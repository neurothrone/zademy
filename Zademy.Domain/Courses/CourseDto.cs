namespace Zademy.Domain.Courses;

public record CourseDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}