using Zademy.Domain.Grades;

namespace Zademy.Api.Endpoints;

public static class GradeEndpoints
{
    public static void MapGradeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/api/v1/grades")
            .WithTags("Grades");

        group.MapGet("", GradeHandlers.GetGradesAsync)
            .WithSummary("Get all grades")
            .WithDescription("Get all grades")
            .Produces<List<GradeDto>>();

        group.MapGet("/students/{id:min(0)}", GradeHandlers.GetGradesByStudentIdAsync)
            .WithSummary("Get grades by student ID")
            .WithDescription("Get grades by student ID")
            .Produces<List<StudentGradeWithCourseDto>>()
            .Produces(StatusCodes.Status404NotFound);
    }
}