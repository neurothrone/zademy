using System.ComponentModel.DataAnnotations;

namespace Zademy.Persistence.Entities;

public class CourseEntity
{
    public int Id { get; set; }

    [MaxLength(255)]
    public required string Title { get; set; }

    [MaxLength(255)]
    public required string Description { get; set; }
}