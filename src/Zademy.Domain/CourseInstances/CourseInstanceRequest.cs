using System.ComponentModel.DataAnnotations;
using Zademy.Domain.Validation;

namespace Zademy.Domain.CourseInstances;

public record CourseInstanceRequest
{
    [Required(ErrorMessage = "Start date is required")]
    [DateString]
    [FutureDate]
    public string? StartDate { get; init; }

    [Required(ErrorMessage = "End date is required")]
    [DateString]
    [DateAfter(comparisonProperty: nameof(StartDate))]
    public string? EndDate { get; init; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Course ID must be greater than 0")]
    public int CourseId { get; init; }

    [Required]
    [MinLength(1, ErrorMessage = "At least one Student ID is required")]
    public required List<int> StudentIds { get; init; }
}