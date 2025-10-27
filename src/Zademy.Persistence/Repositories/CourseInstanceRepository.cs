using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Persistence.Repositories;

public class CourseInstanceRepository(ZademyDbContext context) :
    BaseRepository<CourseInstanceEntity>(context),
    ICourseInstanceRepository
{
    public override Task<List<CourseInstanceEntity>> GetAllAsync()
    {
        return Context.CourseInstances
            .AsNoTracking()
            .Include(ci => ci.Course)
            .Include(ci => ci.Students)
            .ToListAsync();
    }

    public override Task<CourseInstanceEntity?> GetByIdAsync(int id)
    {
        return Context.CourseInstances
            .AsNoTracking()
            .Include(ci => ci.Course)
            .Include(ci => ci.Students)
            .FirstOrDefaultAsync(ci => ci.Id == id);
    }

    public Task<List<CourseEntity>> GetCoursesByStudentIdAsync(int studentId)
    {
        return Context.CourseInstances
            .AsNoTracking()
            .Where(ci => ci.Students.Any(s => s.Id == studentId))
            .Select(ci => ci.Course)
            .Distinct()
            .ToListAsync();
    }

    public Task<List<CourseEntity>> GetCoursesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return Context.CourseInstances
            .AsNoTracking()
            .Where(ci => ci.StartDate >= startDate && ci.EndDate <= endDate)
            .Select(ci => ci.Course)
            .Distinct()
            .ToListAsync();
    }

    public override Task<CourseInstanceEntity> CreateAsync(CourseInstanceEntity entity)
    {
        throw new NotSupportedException("Use CreateAsync with IDs instead");
    }

    public async Task<CourseInstanceEntity> CreateAsync(
        int courseId,
        List<int> studentIds,
        DateTime startDate,
        DateTime endDate
    )
    {
        var course = await Context.Courses.FindAsync(courseId);
        if (course is null)
            throw new InvalidOperationException($"Course with ID {courseId} not found");

        var students = await Context.Students
            .Where(s => studentIds.Contains(s.Id))
            .ToListAsync();

        if (students.Count != studentIds.Count)
            throw new InvalidOperationException("One or more student IDs not found");

        var entity = new CourseInstanceEntity
        {
            StartDate = startDate,
            EndDate = endDate,
            Course = course,
            Students = students
        };

        await Context.CourseInstances.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public override Task<CourseInstanceEntity?> UpdateAsync(int id, CourseInstanceEntity entity)
    {
        throw new NotSupportedException("Use UpdateAsync with IDs instead");
    }

    public async Task<CourseInstanceEntity?> UpdateAsync(
        int courseInstanceId,
        int courseId,
        List<int> studentIds,
        DateTime startDate,
        DateTime endDate
    )
    {
        var entity = await Context.CourseInstances
            .Include(ci => ci.Students)
            .FirstOrDefaultAsync(ci => ci.Id == courseInstanceId);

        if (entity is null)
            return null;

        var course = await Context.Courses.FindAsync(courseId);
        if (course is null)
            throw new InvalidOperationException($"Course with ID {courseId} not found");

        var students = await Context.Students
            .Where(s => studentIds.Contains(s.Id))
            .ToListAsync();

        if (students.Count != studentIds.Count)
            throw new InvalidOperationException("One or more student IDs not found");

        entity.StartDate = startDate;
        entity.EndDate = endDate;
        entity.Course = course;
        entity.Students = students;

        await Context.SaveChangesAsync();
        return entity;
    }
}