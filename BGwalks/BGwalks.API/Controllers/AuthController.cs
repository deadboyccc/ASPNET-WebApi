using BGwalks.API.Models.DTO;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BGwalks.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly UserManager<IdentityUser> userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
      this.userManager = userManager;
    }
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
      // create a new user
      var IdentityUser = new IdentityUser
      {
        UserName = registerRequestDto.Email,
        Email = registerRequestDto.Email,
      };
      // create a new user in Identity User Manager
      var identityResult = await userManager.CreateAsync(IdentityUser, registerRequestDto.Password ?? "");


      // if user creation is successful, add roles to user
      if (identityResult.Succeeded && registerRequestDto.Roles!.Length > 0)
      {
        // try assigning a role to the user
        identityResult = await userManager.AddToRoleAsync(IdentityUser, "User");

        // if role assignment is successful, return success message
        if (identityResult.Succeeded)
        {
          return Ok(new { Status = identityResult.Succeeded, Message = "User created and added to role 'User'.", User = IdentityUser });
        }
      }
      // if user creation or role assignment fails, return error message
      return BadRequest(identityResult.Errors.Select(e => e.Description));
    }
  }
}