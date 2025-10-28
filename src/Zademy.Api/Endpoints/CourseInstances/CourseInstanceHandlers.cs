using Zademy.Business.Mappers;
using Zademy.Business.Services.Contracts;
using Zademy.Domain.CourseInstances;
using Zademy.Domain.Utils;

namespace Zademy.Api.Endpoints.CourseInstances;

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

    public static async Task<IResult> GetCourseInstanceByIdAsync(int id, ICourseInstanceService service)
    {
        var result = await service.GetByIdAsync(id);
        return result.Match<IResult>(
            onSuccess: courseInstance =>
                courseInstance is not null
                    ? TypedResults.Ok(courseInstance)
                    : TypedResults.NotFound(),
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

    public static async Task<IResult> CreateCourseInstanceAsync(
        CourseInstanceRequest request,
        ICourseInstanceService service
    )
    {
        var result = await service.CreateAsync(request.ToData());
        return result.Match<IResult>(
            onSuccess: created => TypedResults.Created($"/api/v1/course-instances/{created.Id}", created),
            onFailure: error =>
            {
                var errorCode = (result as FailureResult<CourseInstanceDto>)?.ErrorCode;
                return errorCode switch
                {
                    nameof(CourseInstanceError.CourseNotFound)
                        => TypedResults.Problem(
                            detail: error,
                            statusCode: StatusCodes.Status404NotFound,
                            title: "Resource Not Found"
                        ),
                    _ => TypedResults.Problem(
                        detail: error,
                        statusCode: StatusCodes.Status500InternalServerError,
                        title: "An error occurred"
                    )
                };
            }
        );
    }

    public static async Task<IResult> UpdateCourseInstanceAsync(
        int id,
        CourseInstanceRequest request,
        ICourseInstanceService service
    )
    {
        var result = await service.UpdateAsync(id, request.ToData());
        return result.Match<IResult>(
            onSuccess: updated => TypedResults.Ok(updated),
            onFailure: error =>
            {
                var errorCode = (result as FailureResult<CourseInstanceDto>)?.ErrorCode;
                return errorCode switch
                {
                    nameof(CourseInstanceError.CourseNotFound)
                        => TypedResults.Problem(
                            detail: error,
                            statusCode: StatusCodes.Status404NotFound,
                            title: "Resource Not Found"
                        ),
                    _ => TypedResults.Problem(
                        detail: error,
                        statusCode: StatusCodes.Status500InternalServerError,
                        title: "An error occurred"
                    )
                };
            }
        );
    }

    public static async Task<IResult> DeleteCourseInstanceAsync(int id, ICourseInstanceService service)
    {
        var result = await service.DeleteAsync(id);
        return result.Match<IResult>(
            onSuccess: deleted => deleted
                ? TypedResults.NoContent()
                : TypedResults.NotFound(),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }
}