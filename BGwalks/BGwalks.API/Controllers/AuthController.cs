using BGwalks.API.Models.DTO;
using BGwalks.API.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BGwalks.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly UserManager<IdentityUser> userManager;

    public ITokenRespository TokenRespository { get; }

    public AuthController(UserManager<IdentityUser> userManager, ITokenRespository tokenRespository)
    {
      this.userManager = userManager;
      TokenRespository = tokenRespository;
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

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
      // Validate request input
      if (string.IsNullOrWhiteSpace(loginRequestDto.Email) ||
          string.IsNullOrWhiteSpace(loginRequestDto.Password))
      {
        return BadRequest("Email and password are required.");
      }

      // Find user by email
      var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

      // If user is found and password is correct, generate JWT token
      if (user != null && await userManager.CheckPasswordAsync(user, loginRequestDto.Password) && (await userManager.GetRolesAsync(user)).Count > 0)
      {
        var roles = (await userManager.GetRolesAsync(user)).ToArray();

        // var token = await GenerateJwtToken(user);
        var token = await TokenRespository.CreateJWTToken(user, roles);
        return Ok(new
        {
          Status = true,
          Message = "User logged in successfully.",
          Token = token  // Commented out for security reasons, use JWT token generation logic instead.
        });
      }

      // Return error if user is not found or password is incorrect
      return Unauthorized("Invalid email or password.");
    }



  }

}