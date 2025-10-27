using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zademy.Business.Mappers;
using Zademy.Business.Services.Contracts;
using Zademy.Domain.CourseInstances;
using Zademy.Domain.Courses;
using Zademy.Domain.Utils;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Business.Services;

public class CourseInstanceService(
    ICourseInstanceRepository repository,
    ILogger<CourseInstanceService> logger
) : ICourseInstanceService
{
    public async Task<Result<List<CourseInstanceResponse>>> GetAllAsync()
    {
        try
        {
            var entities = await repository.GetAllAsync();
            var courseInstances = entities
                .Select(e => e.ToDto())
                .ToList();
            return Result<List<CourseInstanceResponse>>.Success(courseInstances);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Course Instances: {message}", ex.Message);
            return Result<List<CourseInstanceResponse>>.Failure(
                "Failed to retrieve course instances from the database.");
        }
    }

    public async Task<Result<CourseInstanceResponse?>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await repository.GetByIdAsync(id);
            return Result<CourseInstanceResponse?>.Success(entity?.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Course Instance by ID {id}: {message}", id, ex.Message);
            return Result<CourseInstanceResponse?>.Failure(
                "Failed to retrieve the course instance from the database.");
        }
    }

    public async Task<Result<List<CourseDto>>> GetCoursesByStudentIdAsync(int studentId)
    {
        try
        {
            var entities = await repository.GetCoursesByStudentIdAsync(studentId);
            var courses = entities
                .Select(e => e.ToDto())
                .ToList();
            return Result<List<CourseDto>>.Success(courses);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Courses for Student with ID {studentId}: {message}",
                studentId, ex.Message);
            return Result<List<CourseDto>>.Failure("Failed to retrieve courses from the database.");
        }
    }

    public async Task<Result<List<CourseDto>>> GetCoursesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var entities = await repository.GetCoursesByDateRangeAsync(startDate, endDate);
            var courses = entities
                .Select(e => e.ToDto())
                .ToList();
            return Result<List<CourseDto>>.Success(courses);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Courses between {startDate} and {endDate}: {message}",
                startDate, endDate, ex.Message);
            return Result<List<CourseDto>>.Failure("Failed to retrieve courses from the database.");
        }
    }

    public async Task<Result<CourseInstanceResponse>> CreateAsync(CourseInstanceData data)
    {
        try
        {
            var createdEntity = await repository.CreateAsync(
                data.CourseId,
                data.StudentIds,
                data.StartDate,
                data.EndDate
            );
            return Result<CourseInstanceResponse>.Success(createdEntity.ToDto());
        }
        catch (Exception ex) when (ex is InvalidOperationException or DbUpdateException)
        {
            logger.LogError("❌ -> Failed to create Course Instance: {message}", ex.Message);
            return Result<CourseInstanceResponse>.Failure("Failed to create the course instance in the database.");
        }
    }

    public async Task<Result<CourseInstanceResponse?>> UpdateAsync(int id, CourseInstanceData data)
    {
        try
        {
            var updatedEntity = await repository.UpdateAsync(
                id,
                data.CourseId,
                data.StudentIds,
                data.StartDate,
                data.EndDate
            );
            return Result<CourseInstanceResponse?>.Success(updatedEntity?.ToDto());
        }
        catch (Exception ex) when (ex is InvalidOperationException or DbUpdateException)
        {
            logger.LogError("❌ -> Failed to update Course Instance ID {id}: {message}", id, ex.Message);
            return Result<CourseInstanceResponse?>.Failure("Failed to update the course instance in the database.");
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
            logger.LogError("❌ -> Failed to delete Course Instance ID {id}: {message}", id, ex.Message);
            return Result<bool>.Failure("Failed to delete the course instance from the database.");
        }
    }
}