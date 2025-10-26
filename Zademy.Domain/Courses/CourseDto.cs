namespace Zademy.Domain.Courses;

public class CourseDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}