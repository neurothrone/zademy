using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zademy.Domain.Students;

public record StudentRequest
{
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    [DefaultValue("John Doe")]
    public required string Name { get; init; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    [DefaultValue("john.doe@example.com")]
    public required string Email { get; init; }
}