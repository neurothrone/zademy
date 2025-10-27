using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zademy.Business.Mappers;
using Zademy.Business.Services.Contracts;
using Zademy.Domain.Grades;
using Zademy.Domain.Utils;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Business.Services;

public class GradeService(
    IStudentRepository studentRepository,
    ICourseInstanceRepository courseInstanceRepository,
    IGradeRepository gradeRepository,
    ILogger<GradeService> logger
) : IGradeService
{
    public async Task<Result<List<GradeDto>>> GetAllAsync()
    {
        try
        {
            var entities = await gradeRepository.GetAllAsync();
            var grades = entities
                .Select(e => e.ToGradeDto())
                .ToList();
            return Result<List<GradeDto>>.Success(grades);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get all grades: {message}", ex.Message);
            return Result<List<GradeDto>>.Failure("Failed to retrieve grades.");
        }
    }

    public async Task<Result<GradeDto?>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await gradeRepository.GetByIdAsync(id);
            return entity is null
                ? Result<GradeDto?>.Success(null)
                : Result<GradeDto?>.Success(entity.ToGradeDto());
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get grade by ID {id}: {message}", id, ex.Message);
            return Result<GradeDto?>.Failure("Failed to retrieve grade.");
        }
    }

    public async Task<Result<List<CourseGradeDto>>> GetGradesByStudentIdAsync(int studentId)
    {
        try
        {
            var entities = await gradeRepository.GetGradesByStudentIdAsync(studentId);
            var gradesWithCourses = entities
                .Select(e => e.ToCourseGradeDto())
                .ToList();
            return Result<List<CourseGradeDto>>.Success(gradesWithCourses);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get grades for student {studentId}: {message}", studentId, ex.Message);
            return Result<List<CourseGradeDto>>.Failure("Failed to retrieve grades.");
        }
    }

    public async Task<Result<GradeDto>> CreateAsync(GradeRequest request)
    {
        try
        {
            // Check if Student exists
            var studentExists = await studentRepository.ExistsAsync(request.StudentId);
            if (!studentExists)
                return Result<GradeDto>.Failure(
                    error: GradeError.StudentNotFound.ToMessage(),
                    errorCode: nameof(GradeError.StudentNotFound)
                );

            // Check if CourseInstance exists
            var courseExists = await courseInstanceRepository.ExistsAsync(request.CourseInstanceId);
            if (!courseExists)
                return Result<GradeDto>.Failure(GradeError.CourseInstanceNotFound.ToMessage());

            // Check if Grade already exists
            var gradeExists = await gradeRepository.GradeExistsAsync(request.StudentId, request.CourseInstanceId);
            if (gradeExists)
                return Result<GradeDto>.Failure(GradeError.GradeAlreadyExists.ToMessage());

            // Create Grade
            var created = await gradeRepository.CreateAsync(
                request.StudentId,
                request.CourseInstanceId,
                request.Grade
            );
            return Result<GradeDto>.Success(created.ToGradeDto());
        }
        catch (Exception ex) when (ex is InvalidOperationException or DbUpdateException)
        {
            logger.LogError("❌ -> Failed to create grade: {message}", ex.Message);
            return Result<GradeDto>.Failure(GradeError.Unknown.ToMessage());
        }
    }
}