namespace Zademy.Domain.Students;

public record StudentResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
}