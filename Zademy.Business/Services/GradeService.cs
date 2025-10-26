using Microsoft.Extensions.Logging;
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
            var grades = await repository.GetAllAsync();
            var dtos = grades
                .Select(g => new GradeDto
                {
                    Id = g.Id,
                    Value = g.Value,
                })
                .ToList();
            return Result<List<GradeDto>>.Success(dtos);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get all grades: {message}", ex.Message);
            return Result<List<GradeDto>>.Failure("Failed to retrieve grades.");
        }
    }

    public async Task<Result<List<StudentGradeWithCourseDto>>> GetGradesByStudentIdAsync(int studentId)
    {
        try
        {
            var grades = await repository.GetGradesByStudentIdAsync(studentId);
            var dtos = grades
                .Select(g => new StudentGradeWithCourseDto
                {
                    GradeId = g.Id,
                    GradeValue = g.Value,
                    CourseTitle = g.CourseInstance.Course.Title,
                    CourseStartDate = g.CourseInstance.StartDate,
                    CourseEndDate = g.CourseInstance.EndDate
                })
                .ToList();
            return Result<List<StudentGradeWithCourseDto>>.Success(dtos);
        }
        catch (Exception ex)
        {
            logger.LogError("❌ -> Failed to get grades for student {studentId}: {message}", studentId, ex.Message);
            return Result<List<StudentGradeWithCourseDto>>.Failure("Failed to retrieve grades.");
        }
    }
}