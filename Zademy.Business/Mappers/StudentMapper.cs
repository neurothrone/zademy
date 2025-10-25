using Zademy.Domain.Students;
using Zademy.Persistence.Entities;

namespace Zademy.Business.Mappers;

public static class StudentMapper
{
    public static StudentItem ToDto(this StudentEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Email = entity.Email
    };

    public static StudentEntity ToEntity(this StudentInputDto inputDto) => new()
    {
        Name = inputDto.Name,
        Email = inputDto.Email
    };
}