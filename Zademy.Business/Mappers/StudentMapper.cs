using Zademy.Domain.Students;
using Zademy.Persistence.Entities;

namespace Zademy.Business.Mappers;

public static class StudentMapper
{
    public static StudentDto ToDto(this StudentEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Email = entity.Email
    };

    public static StudentEntity ToEntity(this StudentInputDto dto, int id = 0) => new()
    {
        Id = id,
        Name = dto.Name,
        Email = dto.Email
    };
}