using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Persistence.Repositories;

public class StudentRepository(ZademyDbContext context) :
    BaseRepository<StudentEntity>(context),
    IStudentRepository
{
}