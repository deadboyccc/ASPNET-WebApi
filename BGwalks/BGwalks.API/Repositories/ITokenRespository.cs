using Microsoft.AspNetCore.Identity;

namespace BGwalks.API.Repositories;
public interface ITokenRespository
{
  Task<string> CreateJWTToken(IdentityUser user, string[] roles);

}