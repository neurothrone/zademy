using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zademy.Domain.Users;

public record UpdateUserRequest
{
    [Required]
    [MaxLength(50)]
    [DefaultValue("Captain")]
    public required string Title { get; init; }
}