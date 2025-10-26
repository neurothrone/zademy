using System.ComponentModel.DataAnnotations;

namespace Zademy.Persistence.Entities;

public class StudentEntity
{
    public int Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(255)]
    public required string Email { get; set; }
}