using Zademy.Domain.Courses;
using Zademy.Domain.Students;

namespace Zademy.Domain.CourseInstances;

public record CourseInstanceResponse
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required CourseResponse Course { get; set; }
    public List<StudentResponse> Students { get; set; } = [];
}