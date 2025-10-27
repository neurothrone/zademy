using System.ComponentModel.DataAnnotations;

namespace Zademy.Api.Utils;

public class ValidateModelFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        foreach (var argument in context.Arguments)
        {
            if (argument is null) continue;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(argument);

            if (!Validator.TryValidateObject(
                    argument,
                    validationContext,
                    validationResults,
                    validateAllProperties: true)
               )
            {
                return TypedResults.ValidationProblem(
                    validationResults.ToDictionary(
                        vr => vr.MemberNames.FirstOrDefault() ?? "Unknown",
                        vr => new[] { vr.ErrorMessage ?? "Validation failed" }
                    )
                );
            }
        }

        return await next(context);
    }
}