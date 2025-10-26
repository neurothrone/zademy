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
    public async Task<Result<List<CourseInstanceDto>>> GetAllAsync()
    {
        try
        {
            var entities = await repository.GetAllAsync();
            var courseInstances = entities
                .Select(e => e.ToDto())
                .ToList();
            return Result<List<CourseInstanceDto>>.Success(courseInstances);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get Course Instances: {message}", ex.Message);
            return Result<List<CourseInstanceDto>>.Failure("Failed to retrieve course instances from the database.");
        }
    }

    public Task<Result<CourseInstanceDto?>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
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

    public Task<Result<CourseInstanceDto>> CreateAsync(CourseInstanceInputDto courseInstance)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CourseInstanceDto?>> UpdateAsync(int id, CourseInstanceInputDto courseInstance)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}