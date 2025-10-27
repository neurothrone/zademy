using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Repositories.Contracts;

public interface ICourseRepository
{
    Task<List<CourseEntity>> GetAllAsync();
    Task<CourseEntity?> GetByIdAsync(int id);
    Task<CourseEntity> CreateAsync(CourseEntity course);
    Task<CourseEntity?> UpdateAsync(int id, CourseEntity course);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}