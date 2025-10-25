namespace Zademy.Persistence.Entities;

public class StudentEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}