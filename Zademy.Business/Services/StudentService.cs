using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zademy.Business.Mappers;
using Zademy.Business.Services.Contracts;
using Zademy.Domain.Students;
using Zademy.Domain.Utils;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Business.Services;

public class StudentService(
    IStudentRepository repository,
    ILogger<StudentService> logger
) : IStudentService
{
    public async Task<Result<List<StudentDto>>> GetAllAsync()
    {
        try
        {
            var entities = await repository.GetAllAsync();
            var students = entities
                .Select(e => e.ToDto())
                .ToList();
            return Result<List<StudentDto>>.Success(students);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Students: {}", ex.Message);
            return Result<List<StudentDto>>.Failure("Failed to retrieve students from the database.");
        }
    }

    public async Task<Result<StudentDto?>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await repository.GetByIdAsync(id);
            return Result<StudentDto?>.Success(entity?.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Student by ID {id}: {message}", id, ex.Message);
            return Result<StudentDto?>.Failure("Failed to retrieve the student from the database.");
        }
    }

    public async Task<Result<StudentDto>> CreateAsync(StudentInputDto student)
    {
        try
        {
            var entity = student.ToEntity();
            var createdEntity = await repository.CreateAsync(entity);
            return Result<StudentDto>.Success(createdEntity.ToDto());
        }
        catch (DbUpdateException ex)
        {
            logger.LogError("❌ -> Failed to create Student: {message}", ex.Message);
            return Result<StudentDto>.Failure("Failed to create the student in the database.");
        }
    }

    public async Task<Result<StudentDto?>> UpdateAsync(int id, StudentInputDto student)
    {
        try
        {
            var entity = student.ToEntity();
            var updatedEntity = await repository.UpdateAsync(id, entity);
            return Result<StudentDto?>.Success(updatedEntity?.ToDto());
        }
        catch (DbUpdateException ex)
        {
            logger.LogError("❌ -> Failed to update Student ID {id}: {message}", id, ex.Message);
            return Result<StudentDto?>.Failure("Failed to update the student in the database.");
        }
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        try
        {
            var deleted = await repository.DeleteAsync(id);
            return Result<bool>.Success(deleted);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError("❌ -> Failed to delete Student ID {id}: {message}", id, ex.Message);
            return Result<bool>.Failure("Failed to delete the student from the database.");
        }
    }
}