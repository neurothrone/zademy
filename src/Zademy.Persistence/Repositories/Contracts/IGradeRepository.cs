using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Repositories.Contracts;

public interface IGradeRepository
{
    Task<List<GradeEntity>> GetAllAsync();
    Task<GradeEntity?> GetByIdAsync(int id);
    Task<List<GradeEntity>> GetGradesByStudentIdAsync(int studentId);
    Task<GradeEntity> CreateAsync(GradeEntity grade);
    Task<GradeEntity> CreateAsync(int studentId, int courseInstanceId, string gradeValue);
    Task<GradeEntity?> UpdateAsync(int id, GradeEntity grade);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> GradeExistsAsync(int studentId, int courseInstanceId);
}