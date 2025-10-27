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
    public async Task<Result<List<GradeResponse>>> GetAllAsync()
    {
        try
        {
            var entities = await repository.GetAllAsync();
            var grades = entities
                .Select(g => g.ToResponse())
                .ToList();
            return Result<List<GradeResponse>>.Success(grades);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get all grades: {message}", ex.Message);
            return Result<List<GradeResponse>>.Failure("Failed to retrieve grades.");
        }
    }

    public async Task<Result<List<StudentGradeWithCourseResponse>>> GetGradesByStudentIdAsync(int studentId)
    {
        try
        {
            var entities = await repository.GetGradesByStudentIdAsync(studentId);
            var gradesWithCourses = entities
                .Select(g => g.ToSummaryResponse())
                .ToList();
            return Result<List<StudentGradeWithCourseResponse>>.Success(gradesWithCourses);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get grades for student {studentId}: {message}", studentId, ex.Message);
            return Result<List<StudentGradeWithCourseResponse>>.Failure("Failed to retrieve grades.");
        }
    }
}