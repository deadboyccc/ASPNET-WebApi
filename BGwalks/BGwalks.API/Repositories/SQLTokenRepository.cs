using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Collections.Generic;

namespace BGwalks.API.Repositories
{
  public class SQLTokenRepository : ITokenRespository
  {
    private readonly IConfiguration _config;

    public SQLTokenRepository(IConfiguration configuration)
    {
      _config = configuration;
    }

    public Task<string> CreateJWTToken(IdentityUser user, string[] roles)
    {
      // Validate input parameters
      if (user == null)
        throw new ArgumentNullException(nameof(user));

      if (string.IsNullOrEmpty(user.Email))
        throw new ArgumentException("User email is required to create a token.", nameof(user));

      // Retrieve JWT settings from configuration
      var jwtKey = _config["Jwt:Key"];
      var jwtIssuer = _config["Jwt:Issuer"];
      var jwtAudience = _config["Jwt:Audience"];

      if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
      {
        throw new InvalidOperationException("JWT configuration is missing. Please check your appsettings.json.");
      }

      // Create claims including email and roles
      var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
      if (roles != null && roles.Any())
      {
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
      }

      // Generate symmetric security key from the JWT key
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

      // Define token descriptor with claims, issuer, audience, expiration (using UTC), and signing credentials
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Issuer = jwtIssuer,
        Audience = jwtAudience,
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
      };

      // Create and write the token using JwtSecurityTokenHandler
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      return Task.FromResult(tokenString);
    }
  }
}
