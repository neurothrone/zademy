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

        group.MapGet("/students/{id:int:min(0)}/courses", CourseInstanceHandlers.GetCoursesByStudentIdAsync)
            .WithSummary("Get courses by student ID")
            .WithDescription("Get courses assigned to a specific student")
            .Produces<List<CourseDto>>();

        group.MapGet("/courses", CourseInstanceHandlers.GetCoursesByDateRangeAsync)
            .WithSummary("Get courses by date range")
            .WithDescription("Get courses by date range")
            .Produces<List<CourseDto>>()
            .Produces(StatusCodes.Status400BadRequest);
    }
}