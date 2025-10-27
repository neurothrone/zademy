using System.ComponentModel.DataAnnotations;

namespace Zademy.Domain.Validation;

[AttributeUsage(AttributeTargets.Property)]
public class DateStringAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        if (value is not string dateString)
            return new ValidationResult(
                "Value must be a string",
                [validationContext.MemberName ?? "Unknown"]
            );

        if (string.IsNullOrWhiteSpace(dateString))
            return new ValidationResult(
                "Date cannot be empty",
                [validationContext.MemberName ?? "Unknown"]
            );

        return !DateTime.TryParse(dateString, out _)
            ? new ValidationResult(
                "Must be a valid date",
                [validationContext.MemberName ?? "Unknown"]
            )
            : ValidationResult.Success;
    }
}