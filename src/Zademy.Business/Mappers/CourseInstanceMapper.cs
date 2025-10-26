using Zademy.Domain.CourseInstances;
using Zademy.Persistence.Entities;

namespace Zademy.Business.Mappers;

public static class CourseInstanceMapper
{
    public static CourseInstanceDto ToDto(this CourseInstanceEntity entity) => new()
    {
        Id = entity.Id,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        Course = entity.Course.ToDto(),
        Students = entity.Students.Select(s => s.ToDto()).ToList()
    };
}