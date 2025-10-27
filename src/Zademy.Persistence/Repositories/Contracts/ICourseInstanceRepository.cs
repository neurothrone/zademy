using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Repositories.Contracts;

public interface ICourseInstanceRepository
{
    Task<List<CourseInstanceEntity>> GetAllAsync();
    Task<CourseInstanceEntity?> GetByIdAsync(int id);

    Task<CourseInstanceEntity> CreateAsync(
        int courseId,
        List<int> studentIds,
        DateTime startDate,
        DateTime endDate
    );

    Task<CourseInstanceEntity?> UpdateAsync(
        int courseInstanceId,
        int courseId,
        List<int> studentIds,
        DateTime startDate,
        DateTime endDate
    );

    Task<bool> DeleteAsync(int id);

    Task<List<CourseEntity>> GetCoursesByStudentIdAsync(int studentId);
    Task<List<CourseEntity>> GetCoursesByDateRangeAsync(DateTime startDate, DateTime endDate);
}