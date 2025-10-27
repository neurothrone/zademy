using Zademy.Domain.CourseInstances;
using Zademy.Domain.Courses;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface ICourseInstanceService
{
    Task<Result<List<CourseInstanceResponse>>> GetAllAsync();
    Task<Result<CourseInstanceResponse?>> GetByIdAsync(int id);
    Task<Result<CourseInstanceResponse>> CreateAsync(CourseInstanceData data);
    Task<Result<CourseInstanceResponse?>> UpdateAsync(int id, CourseInstanceData data);
    Task<Result<bool>> DeleteAsync(int id);

    Task<Result<List<CourseResponse>>> GetCoursesByStudentIdAsync(int studentId);
    Task<Result<List<CourseResponse>>> GetCoursesByDateRangeAsync(DateTime startDate, DateTime endDate);
}