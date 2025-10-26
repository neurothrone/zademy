using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Repositories.Contracts;

public interface IGradeRepository
{
    Task<List<GradeEntity>> GetAllAsync();
    Task<GradeEntity?> GetByIdAsync(int id);
    Task<GradeEntity> CreateAsync(GradeEntity grade);
    Task<GradeEntity?> UpdateAsync(int id, GradeEntity grade);
    Task<bool> DeleteAsync(int id);

    Task<List<GradeEntity>> GetGradesByStudentIdAsync(int studentId);
}