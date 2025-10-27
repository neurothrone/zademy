using Zademy.Business.Services.Contracts;
using Zademy.Domain.Grades;
using Zademy.Domain.Students;
using Zademy.Domain.Utils;

namespace Zademy.Api.Endpoints;

public static class GradeHandlers
{
    public static async Task<IResult> GetGradesAsync(IGradeService service)
    {
        var result = await service.GetAllAsync();
        return result.Match<IResult>(
            onSuccess: grades => TypedResults.Ok(grades),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> GetGradeById(int id, IGradeService service)
    {
        var result = await service.GetByIdAsync(id);
        return result.Match<IResult>(
            onSuccess: grade => grade is not null ? TypedResults.Ok(grade) : TypedResults.NotFound(),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> GetGradesByStudentIdAsync(
        int id,
        IStudentService studentService,
        IGradeService gradeService)
    {
        var studentResult = await studentService.GetByIdAsync(id);
        if (studentResult is not SuccessResult<StudentDto> { Value: not null and var student })
            return TypedResults.NotFound();


        var gradesResult = await gradeService.GetGradesByStudentIdAsync(id);
        return gradesResult.Match<IResult>(
            onSuccess: grades => TypedResults.Ok(
                new StudentGradesDto
                {
                    StudentId = id,
                    StudentName = student.Name,
                    Grades = grades
                }),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }

    public static async Task<IResult> CreateGradeAsync(GradeRequest request, IGradeService service)
    {
        var result = await service.CreateAsync(request);
        return result.Match<IResult>(
            onSuccess: grade => TypedResults.Created($"/grades/{grade.Id}", grade),
            onFailure: error =>
            {
                var errorCode = (result as FailureResult<GradeDto>)?.ErrorCode;
                return errorCode switch
                {
                    nameof(GradeError.StudentNotFound) or nameof(GradeError.CourseInstanceNotFound)
                        => TypedResults.Problem(
                            detail: error,
                            statusCode: StatusCodes.Status404NotFound,
                            title: "Resource Not Found"
                        ),
                    nameof(GradeError.GradeAlreadyExists)
                        => TypedResults.Problem(
                            detail: error,
                            statusCode: StatusCodes.Status409Conflict,
                            title: "Conflict"
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
}