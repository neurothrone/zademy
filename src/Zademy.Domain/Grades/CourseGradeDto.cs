namespace Zademy.Domain.Grades;

public record CourseGradeDto
{
    public int GradeId { get; init; }
    public required string GradeValue { get; init; }
    public required string CourseTitle { get; init; }
    public DateTime CourseStartDate { get; init; }
    public DateTime CourseEndDate { get; init; }
}