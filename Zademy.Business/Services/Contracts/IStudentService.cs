using Zademy.Domain.Students;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface IStudentService
{
    Task<Result<List<StudentItem>>> GetAllAsync();
    Task<Result<StudentItem>> GetByIdAsync(int id);
    Task<Result<StudentItem>> CreateAsync(StudentInputDto student);
    Task<Result<StudentItem>> UpdateAsync(int id, StudentInputDto student);
    Task<Result<bool>> DeleteAsync(int id);
}