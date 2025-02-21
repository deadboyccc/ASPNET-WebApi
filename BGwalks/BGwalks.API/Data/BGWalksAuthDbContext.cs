using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BGwalks.API.Data;
public class BGWalksAuthDbContext : IdentityDbContext
{
  public BGWalksAuthDbContext(DbContextOptions<BGWalksAuthDbContext> options)
      : base(options)
  {

  }

}