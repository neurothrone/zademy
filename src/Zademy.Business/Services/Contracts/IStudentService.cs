using Zademy.Domain.Students;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface IStudentService
{
    Task<Result<List<StudentResponse>>> GetAllAsync();
    Task<Result<StudentResponse?>> GetByIdAsync(int id);
    Task<Result<StudentResponse>> CreateAsync(StudentRequest student);
    Task<Result<StudentResponse?>> UpdateAsync(int id, StudentRequest student);
    Task<Result<bool>> DeleteAsync(int id);
}