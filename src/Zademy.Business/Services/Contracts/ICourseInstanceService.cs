using Zademy.Domain.CourseInstances;
using Zademy.Domain.Courses;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface ICourseInstanceService
{
    Task<Result<List<CourseInstanceDto>>> GetAllAsync();
    Task<Result<CourseInstanceDto?>> GetByIdAsync(int id);
    Task<Result<CourseInstanceDto>> CreateAsync(CourseInstanceInputDto courseInstance);
    Task<Result<CourseInstanceDto?>> UpdateAsync(int id, CourseInstanceInputDto courseInstance);
    Task<Result<bool>> DeleteAsync(int id);

    Task<Result<List<CourseDto>>> GetCoursesByStudentIdAsync(int studentId);
    Task<Result<List<CourseDto>>> GetCoursesByDateRangeAsync(DateTime startDate, DateTime endDate);
}