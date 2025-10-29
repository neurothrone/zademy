using Zademy.Api.Configuration;
using Zademy.Persistence.Entities;

namespace Zademy.Api.Endpoints.Users;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup($"{ApiVersioning.RoutePrefix}/users")
            .WithTags("Users");

#if DEBUG
        group.MapGet("", UserHandlers.GetUsersAsync)
            .WithSummary("Get all users")
            .WithDescription("Get all users")
            .Produces<IEnumerable<UserEntity>>();
#endif

        group.MapPut("/update", UserHandlers.UpdateUserAsync)
            .RequireAuthorization()
            .WithSummary("Update user")
            .WithDescription("Update user")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}