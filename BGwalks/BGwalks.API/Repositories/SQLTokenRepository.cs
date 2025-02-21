using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using BGwalks.API.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BGwalks.API.Repositories
{
  public class SQLTokenRepository : ITokenRespository
  {
    private readonly IConfiguration _config;

    // Dependency Injection via constructor | adding the configration to access appsettings.json
    public SQLTokenRepository(IConfiguration configuration)
    {
      _config = configuration;
    }


    // Create JWT token for a user with their email and roles
    public Task<string> CreateJWTToken(IdentityUser user, string[] roles)
    {
      // Create claims, including the user's email and roles
      var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!)
            };
      claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

      // Generate the symmetric security key using the JWT key from configuration
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

      // Create the token descriptor with claims, expiry, and signing credentials
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Issuer = _config["Jwt:Issuer"],
        Audience = _config["Jwt:Audience"],
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddHours(2),
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
      };

      // Create and write the token
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // Since there is no asynchronous operation, wrap the result in Task.FromResult
      return Task.FromResult(tokenString);
    }
  }
}
