namespace Zademy.Domain.Grades;

public record GradeDto
{
    public int Id { get; set; }
    public required string Value { get; set; }
}