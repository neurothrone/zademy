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
    ICourseRepository courseRepository,
    ICourseInstanceRepository courseInstanceRepository,
    ILogger<CourseInstanceService> logger
) : ICourseInstanceService
{
    public async Task<Result<List<CourseInstanceDto>>> GetAllAsync()
    {
        try
        {
            var entities = await courseInstanceRepository.GetAllAsync();
            var courseInstances = entities
                .Select(e => e.ToDto())
                .ToList();
            return Result<List<CourseInstanceDto>>.Success(courseInstances);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Course Instances: {message}", ex.Message);
            return Result<List<CourseInstanceDto>>.Failure(
                "Failed to retrieve course instances from the database.");
        }
    }

    public async Task<Result<CourseInstanceDto?>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await courseInstanceRepository.GetByIdAsync(id);
            return Result<CourseInstanceDto?>.Success(entity?.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Course Instance by ID {id}: {message}", id, ex.Message);
            return Result<CourseInstanceDto?>.Failure(
                "Failed to retrieve the course instance from the database.");
        }
    }

    public async Task<Result<List<CourseDto>>> GetCoursesByStudentIdAsync(int studentId)
    {
        try
        {
            var entities = await courseInstanceRepository.GetCoursesByStudentIdAsync(studentId);
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
            var entities = await courseInstanceRepository.GetCoursesByDateRangeAsync(startDate, endDate);
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

    public async Task<Result<CourseInstanceDto>> CreateAsync(CourseInstanceData data)
    {
        try
        {
            var courseExists = await courseRepository.ExistsAsync(data.CourseId);
            if (!courseExists)
                return Result<CourseInstanceDto>.Failure(
                    error: CourseInstanceError.CourseNotFound.ToMessage(),
                    errorCode: nameof(CourseInstanceError.CourseNotFound)
                );

            var createdEntity = await courseInstanceRepository.CreateAsync(
                data.CourseId,
                data.StudentIds,
                data.StartDate,
                data.EndDate
            );
            return Result<CourseInstanceDto>.Success(createdEntity.ToDto());
        }
        catch (Exception ex) when (ex is InvalidOperationException or DbUpdateException)
        {
            logger.LogError("❌ -> Failed to create Course Instance: {message}", ex.Message);
            return Result<CourseInstanceDto>.Failure("Failed to create the course instance in the database.");
        }
    }

    public async Task<Result<CourseInstanceDto?>> UpdateAsync(int id, CourseInstanceData data)
    {
        try
        {
            var courseExists = await courseRepository.ExistsAsync(data.CourseId);
            if (!courseExists)
                return Result<CourseInstanceDto?>.Failure(
                    error: CourseInstanceError.CourseNotFound.ToMessage(),
                    errorCode: nameof(CourseInstanceError.CourseNotFound)
                );

            var updatedEntity = await courseInstanceRepository.UpdateAsync(
                id,
                data.CourseId,
                data.StudentIds,
                data.StartDate,
                data.EndDate
            );
            return Result<CourseInstanceDto?>.Success(updatedEntity?.ToDto());
        }
        catch (Exception ex) when (ex is InvalidOperationException or DbUpdateException)
        {
            logger.LogError("❌ -> Failed to update Course Instance ID {id}: {message}", id, ex.Message);
            return Result<CourseInstanceDto?>.Failure("Failed to update the course instance in the database.");
        }
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        try
        {
            var deleted = await courseInstanceRepository.DeleteAsync(id);
            return Result<bool>.Success(deleted);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError("❌ -> Failed to delete Course Instance ID {id}: {message}", id, ex.Message);
            return Result<bool>.Failure("Failed to delete the course instance from the database.");
        }
    }
}