using Microsoft.Extensions.DependencyInjection;
using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Data;

public static class DbContextExtensions
{
    public static void SeedInMemoryDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ZademyDbContext>();

        if (!context.Students.Any())
        {
            context.Students.AddRange(
                new StudentEntity { Id = 1, Name = "John", Email = "john.doe@example.com" },
                new StudentEntity { Id = 2, Name = "Jane", Email = "jane.doe@example.com" }
            );
        }

        if (!context.Courses.Any())
        {
            context.Courses.AddRange(
                new CourseEntity { Id = 1, Title = "Course 1", Description = "Course 1 description" },
                new CourseEntity { Id = 2, Title = "Course 2", Description = "Course 2 description" }
            );
        }

        context.SaveChanges();
    }
}