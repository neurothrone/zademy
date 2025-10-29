using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Persistence.Repositories;

public class GradeRepository(ZademyAppDbContext context) :
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

    public override Task<GradeEntity> CreateAsync(GradeEntity entity)
    {
        throw new NotSupportedException("Use CreateAsync with IDs instead");
    }

    public async Task<GradeEntity> CreateAsync(int studentId, int courseInstanceId, string gradeValue)
    {
        var student = await Context.Students.FindAsync(studentId);
        if (student is null)
            throw new InvalidOperationException($"Student with ID {studentId} not found");

        var courseInstance = await Context.CourseInstances.FindAsync(courseInstanceId);
        if (courseInstance is null)
            throw new InvalidOperationException($"Course instance with ID {courseInstanceId} not found");

        var entity = new GradeEntity
        {
            Value = gradeValue,
            Student = student,
            CourseInstance = courseInstance
        };

        await Context.Grades.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }


    public Task<bool> GradeExistsAsync(int studentId, int courseInstanceId)
    {
        return Context.Grades.AnyAsync(g =>
            g.Student.Id == studentId && g.CourseInstance.Id == courseInstanceId);
    }
}