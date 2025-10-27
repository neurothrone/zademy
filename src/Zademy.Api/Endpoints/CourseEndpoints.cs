using Zademy.Domain.Courses;

namespace Zademy.Api.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/api/v1/courses")
            .WithTags("Courses");

        group.MapGet("", CourseHandlers.GetCoursesAsync)
            .WithSummary("Get all courses")
            .WithDescription("Get all courses")
            .Produces<List<CourseResponse>>();

        group.MapGet("{id:int:min(0)}", CourseHandlers.GetCourseByIdAsync)
            .WithSummary("Get course by id")
            .WithDescription("Get course by id")
            .Produces<CourseResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("", CourseHandlers.CreateCourseAsync)
            .WithSummary("Create a new course")
            .WithDescription("Create a new course")
            .Produces<CourseResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("{id:int:min(0)}", CourseHandlers.UpdateCourseAsync)
            .WithSummary("Update a course by id")
            .WithDescription("Update a course by id")
            .Produces<CourseResponse>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("{id:int:min(0)}", CourseHandlers.DeleteCourseAsync)
            .WithSummary("Delete a course by id")
            .WithDescription("Delete a course by id")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}