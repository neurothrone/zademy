using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Persistence.Repositories;

public class StudentRepository(ZademyDbContext context) : IStudentRepository
{
    public async Task<List<StudentEntity>> GetAllAsync()
    {
        return await context.Students
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<StudentEntity?> GetByIdAsync(int id)
    {
        return await context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<StudentEntity> CreateAsync(StudentEntity student)
    {
        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
        return student;
    }

    public async Task<StudentEntity?> UpdateAsync(int id, StudentEntity student)
    {
        var existingStudent = await context.Students.FindAsync(id);
        if (existingStudent is null)
            return null;

        existingStudent.Name = student.Name;
        existingStudent.Email = student.Email;

        await context.SaveChangesAsync();
        return existingStudent;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student is null)
            return false;

        context.Students.Remove(student);
        await context.SaveChangesAsync();
        return true;
    }
}