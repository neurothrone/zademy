using Zademy.Business.Services.Contracts;
using Zademy.Domain.CourseInstances;

namespace Zademy.Api.Endpoints;

public static class CourseInstanceHandlers
{
    public static async Task<IResult> GetCourseInstancesAsync(ICourseInstanceService service)
    {
        var result = await service.GetAllAsync();
        return result.Match<IResult>(
            onSuccess: courseInstances => TypedResults.Ok(courseInstances),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> GetCoursesByStudentIdAsync(
        int id,
        ICourseInstanceService service)
    {
        var result = await service.GetCoursesByStudentIdAsync(id);
        return result.Match<IResult>(
            onSuccess: courses => TypedResults.Ok(courses),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> GetCoursesByDateRangeAsync(
        [AsParameters] CourseInstanceDateRangeQuery query,
        ICourseInstanceService service)
    {
        var result = await service.GetCoursesByDateRangeAsync(
            DateTime.Parse(query.StartDate!),
            DateTime.Parse(query.EndDate!)
        );

        return result.Match<IResult>(
            onSuccess: courses => TypedResults.Ok(courses),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }
}