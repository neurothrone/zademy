using Zademy.Domain.Grades;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface IGradeService
{
    Task<Result<List<GradeDto>>> GetAllAsync();
    Task<Result<List<CourseGradeDto>>> GetGradesByStudentIdAsync(int studentId);
}