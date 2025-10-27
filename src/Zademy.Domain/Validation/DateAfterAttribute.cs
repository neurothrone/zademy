using System.ComponentModel.DataAnnotations;

namespace Zademy.Domain.Validation;

[AttributeUsage(AttributeTargets.Property)]
public class DateAfterAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateAfterAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property == null)
            return new ValidationResult($"Unknown property: {_comparisonProperty}");

        var comparisonValue = property.GetValue(validationContext.ObjectInstance)?.ToString();
        var currentValue = value?.ToString();

        if (string.IsNullOrWhiteSpace(currentValue) || string.IsNullOrWhiteSpace(comparisonValue))
            return ValidationResult.Success;

        if (!DateTime.TryParse(currentValue, out var endDate) ||
            !DateTime.TryParse(comparisonValue, out var startDate))
            return ValidationResult.Success;

        if (endDate <= startDate)
            return new ValidationResult("End date must be after start date");

        return ValidationResult.Success;
    }
}