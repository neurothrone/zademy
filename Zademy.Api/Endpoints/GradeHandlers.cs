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
                new StudentGradesResult
                {
                    StudentId = id,
                    StudentName = student.Name,
                    Grades = grades
                }),
            onFailure: error => TypedResults.Problem(error, statusCode: StatusCodes.Status500InternalServerError)
        );
    }
}