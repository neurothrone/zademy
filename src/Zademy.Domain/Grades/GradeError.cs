namespace Zademy.Domain.Grades;

public enum GradeError
{
    StudentNotFound,
    CourseInstanceNotFound,
    GradeAlreadyExists,
    Unknown
}

public static class GradeErrorExtensions
{
    public static string ToMessage(this GradeError error) => error switch
    {
        GradeError.StudentNotFound => "Student not found",
        GradeError.CourseInstanceNotFound => "Course instance not found",
        GradeError.GradeAlreadyExists => "Grade already exists for this student and course",
        _ => "An unexpected error occurred"
    };
}