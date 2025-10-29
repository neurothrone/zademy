using Microsoft.AspNetCore.Identity;
using Zademy.Persistence.Entities;

namespace Zademy.Api.Middlewares;

public class UserDeletionAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, UserManager<UserEntity> userManager)
    {
        if (context.Request.Path.StartsWithSegments("/api/v1/users") &&
            context.Request.Method == HttpMethods.Delete)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "User must be authenticated." });
                return;
            }

            UserEntity? user = await userManager.GetUserAsync(context.User);
            if (user is null || !user.Title.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "Admin privileges required." });
                return;
            }
        }

        await next(context);
    }
}