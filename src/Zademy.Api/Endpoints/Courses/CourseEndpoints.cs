using Zademy.Api.Configuration;
using Zademy.Api.Filters;
using Zademy.Domain.Courses;

namespace Zademy.Api.Endpoints.Courses;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup($"{ApiVersioning.RoutePrefix}/courses")
            .RequireAuthorization()
            .WithTags("Courses");

        group.MapGet("", CourseHandlers.GetCoursesAsync)
            .WithSummary("Get all courses")
            .WithDescription("Get all courses")
            .Produces<List<CourseDto>>();

        group.MapGet("{id:int:min(0)}", CourseHandlers.GetCourseByIdAsync)
            .WithSummary("Get course by id")
            .WithDescription("Get course by id")
            .Produces<CourseDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("", CourseHandlers.CreateCourseAsync)
            .AddEndpointFilter<ValidationFilter>()
            .WithSummary("Create a new course")
            .WithDescription("Create a new course")
            .Produces<CourseDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("{id:int:min(0)}", CourseHandlers.UpdateCourseAsync)
            .AddEndpointFilter<ValidationFilter>()
            .WithSummary("Update a course by id")
            .WithDescription("Update a course by id")
            .Produces<CourseDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("{id:int:min(0)}", CourseHandlers.DeleteCourseAsync)
            .WithSummary("Delete a course by id")
            .WithDescription("Delete a course by id")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}