using Zademy.Domain.Courses;
using Zademy.Domain.Students;

namespace Zademy.Domain.CourseInstances;

public record CourseInstanceDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required CourseDto Course { get; set; }
    public List<StudentDto> Students { get; set; } = [];
}