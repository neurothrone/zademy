namespace Zademy.Domain.Grades;

public record GradeDto
{
    public int Id { get; init; }
    public required string Value { get; init; }
}