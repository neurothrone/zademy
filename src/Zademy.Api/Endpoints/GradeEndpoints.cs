using Zademy.Api.Utils;
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

        group.MapGet("/{id:int:min(0)}", GradeHandlers.GetGradeById)
            .WithSummary("Get grade by id")
            .WithDescription("Get grade by id")
            .Produces<GradeDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/students/{id:int:min(0)}", GradeHandlers.GetGradesByStudentIdAsync)
            .WithSummary("Get grades by student ID")
            .WithDescription("Get grades by student ID")
            .Produces<List<CourseGradeDto>>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("", GradeHandlers.CreateGradeAsync)
            .AddEndpointFilter<ValidationFilter>()
            .WithSummary("Create a new grade")
            .WithDescription("Create a new grade")
            .Produces<GradeDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict);
    }
}