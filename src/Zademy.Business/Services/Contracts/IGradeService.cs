using Zademy.Domain.Grades;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface IGradeService
{
    Task<Result<List<GradeResponse>>> GetAllAsync();
    Task<Result<List<StudentGradeWithCourseResponse>>> GetGradesByStudentIdAsync(int studentId);
}