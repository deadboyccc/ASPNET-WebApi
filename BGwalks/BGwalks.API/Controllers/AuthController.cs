using BGwalks.API.Models.DTO;
using BGwalks.API.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

namespace BGwalks.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    // Two DI UserManager and ITokenRespository are injected here
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRespository _tokenRespository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRespository tokenRespository)
    {
      _userManager = userManager;
      _tokenRespository = tokenRespository;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
      // Input Validation
      if (registerRequestDto == null || string.IsNullOrWhiteSpace(registerRequestDto.Email) || string.IsNullOrWhiteSpace(registerRequestDto.Password))
      {
        return BadRequest("Invalid registration data.");
      }

      // Check if user already exists
      var identityUser = new IdentityUser { UserName = registerRequestDto.Email, Email = registerRequestDto.Email };
      var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

      // If user creation fails, return error message
      if (!identityResult.Succeeded)
      {
        return BadRequest(identityResult.Errors.Select(e => new { e.Code, e.Description }));
      }

      // Generate JWT token
      if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
      {
        // Assign roles to the user
        foreach (var role in registerRequestDto.Roles)
        {
          // Check if role exists
          var roleResult = await _userManager.AddToRoleAsync(identityUser, role);

          // If role assignment fails, delete the user and return error message
          if (!roleResult.Succeeded)
          {
            await _userManager.DeleteAsync(identityUser); // Rollback user creation
            return BadRequest(roleResult.Errors.Select(e => new { e.Code, e.Description }));
          }
        }
      }
      // Assign default role if no roles are provided
      else
      {
        var roleResult = await _userManager.AddToRoleAsync(identityUser, "User");
        if (!roleResult.Succeeded)
        {
          await _userManager.DeleteAsync(identityUser);
          return BadRequest(roleResult.Errors.Select(e => new { e.Code, e.Description }));
        }
      }

      return Ok(new { Status = true, Message = "User created and roles assigned." });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
      // Input Validation
      // Check if email and password are provided
      if (loginRequestDto == null || string.IsNullOrWhiteSpace(loginRequestDto.Email) || string.IsNullOrWhiteSpace(loginRequestDto.Password))

      {
        return BadRequest("Email and password are required.");
      }

      // Validate user credentials
      var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
      if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequestDto.Password) || !(await _userManager.GetRolesAsync(user)).Any())
      {
        return Unauthorized("Invalid email or password.");
      }
      // Generate JWT token
      var roles = (await _userManager.GetRolesAsync(user)).ToArray();
      var token = await _tokenRespository.CreateJWTToken(user, roles);

      return Ok(new LoginResponseDto { JwtToken = token, Roles = roles.ToList() });
    }
  }
}