namespace Zademy.Domain.Grades;

public record StudentGradeWithCourseDto
{
    public int GradeId { get; set; }
    public required string GradeValue { get; set; }
    public required string CourseTitle { get; set; }
    public DateTime CourseStartDate { get; set; }
    public DateTime CourseEndDate { get; set; }
}