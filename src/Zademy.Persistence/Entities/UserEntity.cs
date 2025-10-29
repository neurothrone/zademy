using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Zademy.Persistence.Entities;

public class UserEntity : IdentityUser
{
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
}