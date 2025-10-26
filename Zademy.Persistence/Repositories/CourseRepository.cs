using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Persistence.Repositories;

public class CourseRepository(ZademyDbContext context) : ICourseRepository
{
    public async Task<List<CourseEntity>> GetAllAsync()
    {
        return await context.Courses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CourseEntity?> GetByIdAsync(int id)
    {
        return await context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<CourseEntity> CreateAsync(CourseEntity course)
    {
        await context.Courses.AddAsync(course);
        await context.SaveChangesAsync();
        return course;
    }

    public async Task<CourseEntity?> UpdateAsync(int id, CourseEntity course)
    {
        var existingCourse = await context.Courses.FindAsync(id);
        if (existingCourse is null)
            return null;

        existingCourse.Title = course.Title;
        existingCourse.Description = course.Description;

        await context.SaveChangesAsync();
        return existingCourse;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var course = await context.Courses.FindAsync(id);
        if (course is null)
            return false;

        context.Courses.Remove(course);
        await context.SaveChangesAsync();
        return true;
    }
}