using Zademy.Domain.Students;

namespace Zademy.Api.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/api/v1/students")
            .WithTags("Students");

        group.MapGet("", StudentHandlers.GetStudentsAsync)
            .WithSummary("Get all students")
            .WithDescription("Get all students")
            .Produces<List<StudentResponse>>();

        group.MapGet("{id:int:min(0)}", StudentHandlers.GetStudentByIdAsync)
            .WithSummary("Get student by id")
            .WithDescription("Get student by id")
            .Produces<StudentResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("", StudentHandlers.CreateStudentAsync)
            .WithSummary("Create a new student")
            .WithDescription("Create a new student")
            .Produces<StudentResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("{id:int:min(0)}", StudentHandlers.UpdateStudentAsync)
            .WithSummary("Update a student by id")
            .WithDescription("Update a student by id")
            .Produces<StudentResponse>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("{id:int:min(0)}", StudentHandlers.DeleteStudentAsync)
            .WithSummary("Delete a student by id")
            .WithDescription("Delete a student by id")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}