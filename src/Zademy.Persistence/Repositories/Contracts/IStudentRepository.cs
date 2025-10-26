using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Repositories.Contracts;

public interface IStudentRepository
{
    Task<List<StudentEntity>> GetAllAsync();
    Task<StudentEntity?> GetByIdAsync(int id);
    Task<StudentEntity> CreateAsync(StudentEntity student);
    Task<StudentEntity?> UpdateAsync(int id, StudentEntity student);
    Task<bool> DeleteAsync(int id);
}