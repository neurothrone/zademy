using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Persistence.Repositories;

public class CourseRepository(ZademyAppDbContext context) :
    BaseRepository<CourseEntity>(context),
    ICourseRepository
{
}