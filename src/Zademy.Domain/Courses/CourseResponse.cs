namespace Zademy.Domain.Courses;

public record CourseResponse
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}