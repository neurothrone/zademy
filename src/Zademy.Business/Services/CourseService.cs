using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zademy.Business.Mappers;
using Zademy.Business.Services.Contracts;
using Zademy.Domain.Courses;
using Zademy.Domain.Utils;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Business.Services;

public class CourseService(
    ICourseRepository repository,
    ILogger<CourseService> logger
) : ICourseService
{
    public async Task<Result<List<CourseResponse>>> GetAllAsync()
    {
        try
        {
            var entities = await repository.GetAllAsync();
            var courses = entities
                .Select(e => e.ToDto())
                .ToList();
            return Result<List<CourseResponse>>.Success(courses);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Courses: {}", ex.Message);
            return Result<List<CourseResponse>>.Failure("Failed to retrieve courses from the database.");
        }
    }

    public async Task<Result<CourseResponse?>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await repository.GetByIdAsync(id);
            return Result<CourseResponse?>.Success(entity?.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Course by ID {id}: {message}", id, ex.Message);
            return Result<CourseResponse?>.Failure("Failed to retrieve the course from the database.");
        }
    }

    public async Task<Result<CourseResponse>> CreateAsync(CourseRequest course)
    {
        try
        {
            var entity = course.ToEntity();
            var createdEntity = await repository.CreateAsync(entity);
            return Result<CourseResponse>.Success(createdEntity.ToDto());
        }
        catch (DbUpdateException ex)
        {
            logger.LogError("❌ -> Failed to create Course: {message}", ex.Message);
            return Result<CourseResponse>.Failure("Failed to create the course in the database.");
        }
    }

    public async Task<Result<CourseResponse?>> UpdateAsync(int id, CourseRequest course)
    {
        try
        {
            var entity = course.ToEntity(id: id);
            var updatedEntity = await repository.UpdateAsync(id, entity);
            return Result<CourseResponse?>.Success(updatedEntity?.ToDto());
        }
        catch (DbUpdateException ex)
        {
            logger.LogError("❌ -> Failed to update Course ID {id}: {message}", id, ex.Message);
            return Result<CourseResponse?>.Failure("Failed to update the course in the database.");
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
            logger.LogError("❌ -> Failed to delete Course ID {id}: {message}", id, ex.Message);
            return Result<bool>.Failure("Failed to delete the course from the database.");
        }
    }
}