using System.ComponentModel.DataAnnotations;

namespace Zademy.Domain.Validation;

[AttributeUsage(AttributeTargets.Property)]
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dateString = value?.ToString();

        if (string.IsNullOrWhiteSpace(dateString))
            return ValidationResult.Success;

        if (!DateTime.TryParse(dateString, out var date))
            return ValidationResult.Success;

        if (date.Date < DateTime.UtcNow.Date)
            return new ValidationResult("Date cannot be in the past");

        return ValidationResult.Success;
    }
}