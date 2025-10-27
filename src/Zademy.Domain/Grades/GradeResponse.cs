namespace Zademy.Domain.Grades;

public record GradeResponse
{
    public int Id { get; init; }
    public required string Value { get; init; }
}