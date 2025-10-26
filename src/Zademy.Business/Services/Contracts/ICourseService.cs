using Zademy.Domain.Courses;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface ICourseService
{
    Task<Result<List<CourseDto>>> GetAllAsync();
    Task<Result<CourseDto?>> GetByIdAsync(int id);
    Task<Result<CourseDto>> CreateAsync(CourseInputDto course);
    Task<Result<CourseDto?>> UpdateAsync(int id, CourseInputDto course);
    Task<Result<bool>> DeleteAsync(int id);
}