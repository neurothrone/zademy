using Zademy.Api.Utils;
using Zademy.Domain.CourseInstances;
using Zademy.Domain.Courses;

namespace Zademy.Api.Endpoints;

public static class CourseInstanceEndpoints
{
    public static void MapCourseInstanceEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/api/v1/course-instances")
            .WithTags("Course Instances");

        group.MapGet("", CourseInstanceHandlers.GetCourseInstancesAsync)
            .WithSummary("Get all course instances")
            .WithDescription("Get all course instances")
            .Produces<List<CourseInstanceDto>>();

        group.MapGet("/{id:int:min(0)}", CourseInstanceHandlers.GetCourseInstanceByIdAsync)
            .WithSummary("Get a course instance by id")
            .WithDescription("Get a course instance by id")
            .Produces<CourseInstanceDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/students/{id:int:min(0)}/courses", CourseInstanceHandlers.GetCoursesByStudentIdAsync)
            .WithSummary("Get courses by student ID")
            .WithDescription("Get courses assigned to a specific student")
            .Produces<List<CourseDto>>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/courses", CourseInstanceHandlers.GetCoursesByDateRangeAsync)
            .AddEndpointFilter<ValidateModelFilter>()
            .WithSummary("Get courses by date range")
            .WithDescription("Get courses by date range")
            .Produces<List<CourseDto>>()
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("", CourseInstanceHandlers.CreateCourseInstanceAsync)
            .AddEndpointFilter<ValidateModelFilter>()
            .WithSummary("Create a new course instance")
            .WithDescription("Create a new course instance")
            .Produces<CourseInstanceDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("{id:int:min(0)}", CourseInstanceHandlers.UpdateCourseInstanceAsync)
            .AddEndpointFilter<ValidateModelFilter>()
            .WithSummary("Update a course instance by id")
            .WithDescription("Update a course instance by id")
            .Produces<CourseInstanceDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("{id:int:min(0)}", CourseInstanceHandlers.DeleteCourseInstanceAsync)
            .WithSummary("Delete a course instance by id")
            .WithDescription("Delete a course instance by id")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}