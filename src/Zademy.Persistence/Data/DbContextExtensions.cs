using Microsoft.Extensions.DependencyInjection;
using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Data;

public static class DbContextExtensions
{
    public static void SetupDatabase(this IServiceProvider serviceProvider, bool seedData = false)
    {
        using var scope = serviceProvider.CreateScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<ZademyAppDbContext>();
        var identityDbContext = scope.ServiceProvider.GetRequiredService<ZademyIdentityDbContext>();

        try
        {
            appDbContext.Database.EnsureCreated();
            identityDbContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var message = string.Format("Failed to create databases. AppDb: {0}, IdentityDb: {1}. Error: {2}",
                appDbContext.Database.ProviderName, identityDbContext.Database.ProviderName, ex.Message);
            throw new Exception(message, ex);
        }

        if (seedData)
            SeedDatabase(appDbContext);
    }

    private static void SeedDatabase(ZademyAppDbContext context)
    {
        var course1 = new CourseEntity { Id = 1, Title = "Course 1", Description = "Course 1 description" };
        var course2 = new CourseEntity { Id = 2, Title = "Course 2", Description = "Course 2 description" };

        var student1 = new StudentEntity { Id = 1, Name = "John", Email = "john.doe@example.com" };
        var student2 = new StudentEntity { Id = 2, Name = "Jane", Email = "jane.doe@example.com" };
        var student3 = new StudentEntity { Id = 3, Name = "Frodo", Email = "frodo.baggins@shire.com" };

        if (!context.Courses.Any())
            context.Courses.AddRange(course1, course2);

        if (!context.Students.Any())
            context.Students.AddRange(student1, student2, student3);

        var courseInstance1 = new CourseInstanceEntity
        {
            Id = 1,
            StartDate = DateTime.UtcNow.AddDays(-10),
            EndDate = DateTime.UtcNow.AddDays(20),
            Course = course1,
            Students = [student1, student2]
        };
        var courseInstance2 = new CourseInstanceEntity
        {
            Id = 2,
            StartDate = DateTime.UtcNow.AddDays(-5),
            EndDate = DateTime.UtcNow.AddDays(25),
            Course = course2,
            Students = [student2]
        };
        var courseInstance3 = new CourseInstanceEntity
        {
            Id = 3,
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(30),
            Course = course2,
            Students = [student3]
        };

        if (!context.CourseInstances.Any())
            context.CourseInstances.AddRange(courseInstance1, courseInstance2, courseInstance3);

        if (!context.Grades.Any())
        {
            context.Grades.AddRange(
                new GradeEntity
                {
                    Id = 1,
                    Value = "A",
                    Student = student1,
                    CourseInstance = courseInstance1
                }
            );
        }

        context.SaveChanges();
    }
}