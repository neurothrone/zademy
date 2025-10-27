using System.ComponentModel.DataAnnotations;
using Zademy.Domain.Grades;

namespace Zademy.Domain.Validation;

public class ValidGradeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string gradeString)
            return new ValidationResult("Grade must be a string");

        if (string.IsNullOrWhiteSpace(gradeString))
            return new ValidationResult("Grade cannot be empty");

        if (!Enum.TryParse<Grade>(gradeString, ignoreCase: true, out var grade)
            || !Enum.IsDefined(typeof(Grade), grade))
        {
            var validGrades = string.Join(", ", Enum.GetNames(typeof(Grade)));
            return new ValidationResult($"Invalid grade value. Valid grades are: {validGrades}");
        }

        // Normalize to uppercase
        var property = validationContext.ObjectType.GetProperty(validationContext.MemberName!);
        if (property != null && property.CanWrite)
        {
            property.SetValue(validationContext.ObjectInstance, grade.ToString().ToUpperInvariant());
        }

        return ValidationResult.Success;
    }
}