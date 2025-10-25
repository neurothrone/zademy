using Zademy.Api.Models;
using Zademy.Core.Shared.Domain;

namespace Zademy.Api.Endpoints;

public static class StudentHandlers
{
    private static readonly List<Student> Students =
    [
        new() { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
        new() { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
    ];

    public static async Task<IResult> GetStudentsAsync()
    {
        return TypedResults.Ok(Students);
    }

    public static async Task<IResult> GetStudentByIdAsync(int id)
    {
        var student = Students.FirstOrDefault(x => x.Id == id);
        return student is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(student);
    }

    public static async Task<IResult> CreateStudentAsync(StudentInputDto student)
    {
        if (student.Name == string.Empty || student.Email == string.Empty)
            return TypedResults.BadRequest();

        var newStudent = new Student
        {
            Id = Students.Max(x => x.Id) + 1,
            Name = student.Name,
            Email = student.Email
        };
        Students.Add(newStudent);
        return TypedResults.Created($"/api/v1/students/{newStudent.Id}", student);
    }

    public static async Task<IResult> UpdateStudentAsync(int id, StudentInputDto student)
    {
        var existingStudent = Students.FirstOrDefault(x => x.Id == id);
        if (existingStudent is null)
            return TypedResults.NotFound();

        existingStudent.Name = student.Name;
        existingStudent.Email = student.Email;

        return TypedResults.Ok(existingStudent);
    }

    public static async Task<IResult> DeleteStudentAsync(int id)
    {
        var student = Students.FirstOrDefault(x => x.Id == id);
        if (student is null)
            return TypedResults.NotFound();

        Students.Remove(student);
        return TypedResults.NoContent();
    }
}