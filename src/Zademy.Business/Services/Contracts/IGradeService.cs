using Zademy.Domain.Grades;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface IGradeService
{
    Task<Result<List<GradeDto>>> GetAllAsync();
    Task<Result<GradeDto?>> GetByIdAsync(int id);
    Task<Result<List<CourseGradeDto>>> GetGradesByStudentIdAsync(int studentId);
    Task<Result<GradeDto>> CreateAsync(GradeRequest request);
}