using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zademy.Domain.Courses;

public record CourseRequest
{
    [Required]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
    [MaxLength(255)]
    [DefaultValue("Course Title")]
    public required string Title { get; init; }

    [Required]
    // [MinLength(1)]
    [MaxLength(255)]
    [DefaultValue("Course Description")]
    public required string Description { get; init; }
}