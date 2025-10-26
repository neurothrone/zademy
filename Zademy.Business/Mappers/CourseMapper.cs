using Zademy.Domain.Courses;
using Zademy.Persistence.Entities;

namespace Zademy.Business.Mappers;

public static class CourseMapper
{
    public static CourseDto ToDto(this CourseEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description
    };

    public static CourseEntity ToEntity(this CourseInputDto dto, int id = 0) => new()
    {
        Id = id,
        Title = dto.Title,
        Description = dto.Description
    };
}