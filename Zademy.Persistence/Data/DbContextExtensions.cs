using Microsoft.Extensions.DependencyInjection;
using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Data;

public static class DbContextExtensions
{
    public static void SeedInMemoryDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ZademyDbContext>();

        var course1 = new CourseEntity { Id = 1, Title = "Course 1", Description = "Course 1 description" };
        var course2 = new CourseEntity { Id = 2, Title = "Course 2", Description = "Course 2 description" };

        var student1 = new StudentEntity { Id = 1, Name = "John", Email = "john.doe@example.com" };
        var student2 = new StudentEntity { Id = 2, Name = "Jane", Email = "jane.doe@example.com" };
        var student3 = new StudentEntity { Id = 3, Name = "Frodo", Email = "frodo.baggins@shire.com" };

        if (!context.Courses.Any())
            context.Courses.AddRange(course1, course2);

        if (!context.Students.Any())
            context.Students.AddRange(student1, student2, student3);

        if (!context.CourseInstances.Any())
        {
            context.CourseInstances.AddRange(
                new CourseInstanceEntity
                {
                    Id = 1,
                    StartDate = DateTime.UtcNow.AddDays(-10),
                    EndDate = DateTime.UtcNow.AddDays(20),
                    Course = course1,
                    Students = [student1, student2]
                },
                new CourseInstanceEntity
                {
                    Id = 2,
                    StartDate = DateTime.UtcNow.AddDays(-5),
                    EndDate = DateTime.UtcNow.AddDays(25),
                    Course = course2,
                    Students = [student2]
                },
                new CourseInstanceEntity
                {
                    Id = 3,
                    StartDate = DateTime.UtcNow.AddDays(1),
                    EndDate = DateTime.UtcNow.AddDays(30),
                    Course = course2,
                    Students = [student3]
                }
            );
        }

        context.SaveChanges();
    }
}