namespace Zademy.Domain.Grades;

public record StudentGradesResult
{
    public int StudentId { get; set; }
    public required string StudentName { get; set; }
    public List<StudentGradeWithCourseDto> Grades { get; set; } = [];
}