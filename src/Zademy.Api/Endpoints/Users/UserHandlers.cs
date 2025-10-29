using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Zademy.Domain.Users;
using Zademy.Persistence.Entities;

namespace Zademy.Api.Endpoints.Users;

public static class UserHandlers
{
    public static Task<IResult> GetUsersAsync(UserManager<UserEntity> userManager)
    {
        return Task.FromResult<IResult>(TypedResults.Ok(userManager.Users));
    }

    public static async Task<IResult> UpdateUserAsync(
        UpdateUserRequest request,
        ClaimsPrincipal userPrincipal,
        UserManager<UserEntity> userManager)
    {
        UserEntity? currentUser = await userManager.GetUserAsync(userPrincipal);
        if (currentUser is null)
            return TypedResults.Unauthorized();

        currentUser.Title = request.Title;

        var result = await userManager.UpdateAsync(currentUser);
        if (result.Succeeded)
            return TypedResults.Ok();

        return TypedResults.BadRequest(result.Errors);
    }
}