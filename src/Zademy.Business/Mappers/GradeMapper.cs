using Zademy.Domain.Grades;
using Zademy.Persistence.Entities;

namespace Zademy.Business.Mappers;

public static class GradeMapper
{
    public static GradeDto ToGradeDto(this GradeEntity entity) => new()
    {
        Id = entity.Id,
        Value = entity.Value
    };

    public static CourseGradeDto ToCourseGradeDto(this GradeEntity entity) => new()
    {
        GradeId = entity.Id,
        GradeValue = entity.Value,
        CourseTitle = entity.CourseInstance.Course.Title,
        CourseStartDate = entity.CourseInstance.StartDate,
        CourseEndDate = entity.CourseInstance.EndDate
    };
}