using Microsoft.Extensions.Logging;
using Zademy.Business.Mappers;
using Zademy.Business.Services.Contracts;
using Zademy.Domain.Grades;
using Zademy.Domain.Utils;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Business.Services;

public class GradeService(
    IGradeRepository repository,
    ILogger<GradeService> logger
) : IGradeService
{
    public async Task<Result<List<GradeDto>>> GetAllAsync()
    {
        try
        {
            var entities = await repository.GetAllAsync();
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

    public async Task<Result<List<CourseGradeDto>>> GetGradesByStudentIdAsync(int studentId)
    {
        try
        {
            var entities = await repository.GetGradesByStudentIdAsync(studentId);
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
}