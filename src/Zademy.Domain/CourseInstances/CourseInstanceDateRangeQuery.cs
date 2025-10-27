using System.ComponentModel.DataAnnotations;
using Zademy.Domain.Validation;

namespace Zademy.Domain.CourseInstances;

public record CourseInstanceDateRangeQuery
{
    [Required(ErrorMessage = "Start date is required")]
    [DateString]
    public string? StartDate { get; init; }

    [Required(ErrorMessage = "End date is required")]
    [DateString]
    [DateAfter(comparisonProperty: nameof(StartDate))]
    public string? EndDate { get; init; }
}