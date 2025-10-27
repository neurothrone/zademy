using Zademy.Domain.Courses;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface ICourseService
{
    Task<Result<List<CourseResponse>>> GetAllAsync();
    Task<Result<CourseResponse?>> GetByIdAsync(int id);
    Task<Result<CourseResponse>> CreateAsync(CourseRequest course);
    Task<Result<CourseResponse?>> UpdateAsync(int id, CourseRequest course);
    Task<Result<bool>> DeleteAsync(int id);
}