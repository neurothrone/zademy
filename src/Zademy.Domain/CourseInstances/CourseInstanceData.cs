namespace Zademy.Domain.CourseInstances;

public record CourseInstanceData
{
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public required int CourseId { get; init; }
    public required List<int> StudentIds { get; init; }
}