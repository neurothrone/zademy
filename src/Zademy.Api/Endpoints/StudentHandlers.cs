using System.ComponentModel.DataAnnotations;
using Zademy.Business.Services.Contracts;
using Zademy.Domain.Students;

namespace Zademy.Api.Endpoints;

public static class StudentHandlers
{
    public static async Task<IResult> GetStudentsAsync(IStudentService service)
    {
        var result = await service.GetAllAsync();
        return result.Match<IResult>(
            onSuccess: students => TypedResults.Ok(students),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> GetStudentByIdAsync(int id, IStudentService service)
    {
        var result = await service.GetByIdAsync(id);
        return result.Match<IResult>(
            onSuccess: student => student is not null
                ? TypedResults.Ok(student)
                : TypedResults.NotFound(),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> CreateStudentAsync(StudentInputDto student, IStudentService service)
    {
        if (!Validator.TryValidateObject(student, new ValidationContext(student), null, true))
            return TypedResults.BadRequest("Invalid student data.");

        var result = await service.CreateAsync(student);
        return result.Match<IResult>(
            onSuccess: createdStudent => TypedResults.Created($"/api/v1/students/{createdStudent.Id}", createdStudent),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> UpdateStudentAsync(int id, StudentInputDto student, IStudentService service)
    {
        if (!Validator.TryValidateObject(student, new ValidationContext(student), null, true))
            return TypedResults.BadRequest("Invalid student data.");

        var result = await service.UpdateAsync(id, student);
        return result.Match<IResult>(
            onSuccess: updatedStudent => updatedStudent is not null
                ? TypedResults.Ok(updatedStudent)
                : TypedResults.NotFound(),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> DeleteStudentAsync(int id, IStudentService service)
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