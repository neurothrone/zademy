namespace Zademy.Domain.Grades;

public record StudentGradesDto
{
    public int StudentId { get; set; }
    public required string StudentName { get; set; }
    public List<CourseGradeDto> Grades { get; set; } = [];
}