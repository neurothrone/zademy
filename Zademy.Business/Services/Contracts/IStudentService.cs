using Zademy.Domain.Students;
using Zademy.Domain.Utils;

namespace Zademy.Business.Services.Contracts;

public interface IStudentService
{
    Task<Result<List<StudentDto>>> GetAllAsync();
    Task<Result<StudentDto?>> GetByIdAsync(int id);
    Task<Result<StudentDto>> CreateAsync(StudentInputDto student);
    Task<Result<StudentDto?>> UpdateAsync(int id, StudentInputDto student);
    Task<Result<bool>> DeleteAsync(int id);
}