using System.ComponentModel.DataAnnotations;

namespace Zademy.Persistence.Entities;

public class GradeEntity
{
    public int Id { get; set; }

    [MinLength(1), MaxLength(1)]
    public required string Value { get; set; }

    public int StudentId { get; set; }
    public required StudentEntity Student { get; set; }

    public int CourseInstanceId { get; set; }
    public required CourseInstanceEntity CourseInstance { get; set; }
}