using Zademy.Business.Services.Contracts;

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
        ICourseInstanceService service,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        if (startDate.HasValue ^ endDate.HasValue)
            return TypedResults.BadRequest("Both startDate and endDate must be provided together.");

        if (!startDate.HasValue || !endDate.HasValue)
            return TypedResults.BadRequest("Both startDate and endDate are required.");

        var result = await service.GetCoursesByDateRangeAsync(startDate.Value, endDate.Value);
        return result.Match<IResult>(
            onSuccess: courses => TypedResults.Ok(courses),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }
}