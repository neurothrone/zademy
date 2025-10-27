using System.ComponentModel.DataAnnotations;
using Zademy.Business.Services.Contracts;
using Zademy.Domain.Courses;

namespace Zademy.Api.Endpoints;

public static class CourseHandlers
{
    public static async Task<IResult> GetCoursesAsync(ICourseService service)
    {
        var result = await service.GetAllAsync();
        return result.Match<IResult>(
            onSuccess: courses => TypedResults.Ok(courses),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> GetCourseByIdAsync(int id, ICourseService service)
    {
        var result = await service.GetByIdAsync(id);
        return result.Match<IResult>(
            onSuccess: course => course is not null
                ? TypedResults.Ok(course)
                : TypedResults.NotFound(),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> CreateCourseAsync(CourseRequest course, ICourseService service)
    {
        if (!Validator.TryValidateObject(course, new ValidationContext(course), null, true))
            return TypedResults.BadRequest("Invalid course data.");

        var result = await service.CreateAsync(course);
        return result.Match<IResult>(
            onSuccess: createdCourse => TypedResults.Created($"/api/v1/courses/{createdCourse.Id}", createdCourse),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> UpdateCourseAsync(int id, CourseRequest course, ICourseService service)
    {
        if (!Validator.TryValidateObject(course, new ValidationContext(course), null, true))
            return TypedResults.BadRequest("Invalid course data.");

        var result = await service.UpdateAsync(id, course);
        return result.Match<IResult>(
            onSuccess: updatedCourse => updatedCourse is not null
                ? TypedResults.Ok(updatedCourse)
                : TypedResults.NotFound(),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> DeleteCourseAsync(int id, ICourseService service)
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