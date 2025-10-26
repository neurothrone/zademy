namespace Zademy.Persistence.Entities;

public class CourseInstanceEntity
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required CourseEntity Course { get; set; }
    public List<StudentEntity> Students { get; set; } = [];
}