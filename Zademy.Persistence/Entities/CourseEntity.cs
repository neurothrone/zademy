namespace Zademy.Persistence.Entities;

public class CourseEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}