using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zademy.Domain.Users;
using Zademy.Persistence.Entities;

namespace Zademy.Api.Endpoints.Users;

[ApiController]
[Route("[controller]")]
public class UserController(UserManager<UserEntity> userManager) : ControllerBase
{
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        UserEntity? currentUser = await userManager.GetUserAsync(User);

        if (currentUser == null)
            return Unauthorized();

        currentUser.Title = request.Title;
        var result = await userManager.UpdateAsync(currentUser);

        if (result.Succeeded)
            return Ok();

        return BadRequest(result.Errors);
    }
}