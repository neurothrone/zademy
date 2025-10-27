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
        CourseInstanceRequest courseInstance,
        ICourseInstanceService service
    )
    {
        var data = new CourseInstanceData
        {
            StartDate = DateTime.Parse(courseInstance.StartDate!),
            EndDate = DateTime.Parse(courseInstance.EndDate!),
            CourseId = courseInstance.CourseId,
            StudentIds = courseInstance.StudentIds,
        };
        var result = await service.CreateAsync(data);

        return result.Match<IResult>(
            onSuccess: created => TypedResults.Created($"/api/v1/course-instances/{created.Id}", created),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> UpdateCourseInstanceAsync(
        int id,
        CourseInstanceRequest courseInstance,
        ICourseInstanceService service
    )
    {
        var data = new CourseInstanceData
        {
            StartDate = DateTime.Parse(courseInstance.StartDate!),
            EndDate = DateTime.Parse(courseInstance.EndDate!),
            CourseId = courseInstance.CourseId,
            StudentIds = courseInstance.StudentIds,
        };
        var result = await service.UpdateAsync(id, data);

        return result.Match<IResult>(
            onSuccess: updated => TypedResults.Ok(updated),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
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