using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Persistence.Repositories;

public class CourseInstanceRepository(ZademyDbContext context) :
    BaseRepository<CourseInstanceEntity>(context),
    ICourseInstanceRepository
{
    public override async Task<List<CourseInstanceEntity>> GetAllAsync()
    {
        return await Context.CourseInstances
            .AsNoTracking()
            .Include(ci => ci.Course)
            .Include(ci => ci.Students)
            .ToListAsync();
    }

    public async Task<List<CourseEntity>> GetCoursesByStudentIdAsync(int studentId)
    {
        return await Context.CourseInstances
            .AsNoTracking()
            .Where(ci => ci.Students.Any(s => s.Id == studentId))
            .Select(ci => ci.Course)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<CourseEntity>> GetCoursesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await Context.CourseInstances
            .AsNoTracking()
            .Where(ci => ci.StartDate >= startDate && ci.EndDate <= endDate)
            .Select(ci => ci.Course)
            .Distinct()
            .ToListAsync();
    }
}