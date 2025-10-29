using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zademy.Domain.Students;

public record StudentRequest
{
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
    [DefaultValue("John Doe")]
    public required string Name { get; init; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    [DefaultValue("john.doe@example.com")]
    public  string Email { get; init; }
}