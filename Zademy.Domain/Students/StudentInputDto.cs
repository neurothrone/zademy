using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zademy.Domain.Students;

public record StudentInputDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    [DefaultValue("John Doe")]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    [DefaultValue("john.doe@example.com")]
    public required string Email { get; set; }
}