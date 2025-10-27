using System.ComponentModel.DataAnnotations;
using Zademy.Domain.Validation;

namespace Zademy.Domain.Grades;

public record GradeRequest
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Student ID must be greater than 0")]
    public int StudentId { get; init; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Course ID must be greater than 0")]
    public int CourseInstanceId { get; init; }

    [Required]
    [ValidGrade]
    public required string Grade { get; init; }
}