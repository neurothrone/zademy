using Zademy.Domain.Students;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface IStudentService
{
    Task<Result<List<StudentDto>>> GetAllAsync();
    Task<Result<StudentDto?>> GetByIdAsync(int id);
    Task<Result<StudentDto>> CreateAsync(StudentRequest student);
    Task<Result<StudentDto?>> UpdateAsync(int id, StudentRequest student);
    Task<Result<bool>> DeleteAsync(int id);
}