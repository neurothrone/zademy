namespace Zademy.Domain.CourseInstances;

public enum CourseInstanceError
{
    StudentsNotFound,
    CourseNotFound,
    Unknown
}

public static class CourseInstanceErrorExtensions
{
    public static string ToMessage(this CourseInstanceError error) => error switch
    {
        CourseInstanceError.StudentsNotFound => "Student/Students not found",
        CourseInstanceError.CourseNotFound => "Course not found",
        _ => "An unexpected error occurred"
    };
}