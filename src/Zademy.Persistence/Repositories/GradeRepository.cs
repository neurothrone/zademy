using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Persistence.Repositories;

public class GradeRepository(ZademyDbContext context) :
    BaseRepository<GradeEntity>(context),
    IGradeRepository
{
    public Task<List<GradeEntity>> GetGradesByStudentIdAsync(int studentId)
    {
        return Context.Grades
            .AsNoTracking()
            .Where(g => g.Student.Id == studentId)
            .Include(g => g.Student)
            .Include(g => g.CourseInstance)
            .ThenInclude(ci => ci.Course)
            .ToListAsync();
    }
}