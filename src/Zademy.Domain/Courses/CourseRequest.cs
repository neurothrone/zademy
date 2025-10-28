using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zademy.Domain.Courses;

public record CourseRequest
{
    [Required]
    [StringLength(maximumLength: 255, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 255 characters")]
    [DefaultValue("Course Title")]
    public required string Title { get; init; }

    [Required]
    [MaxLength(255)]
    [DefaultValue("Course Description")]
    public required string Description { get; init; }
}