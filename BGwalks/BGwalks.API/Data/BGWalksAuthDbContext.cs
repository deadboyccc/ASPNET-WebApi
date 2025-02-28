using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BGwalks.API.Data;
public class BGWalksAuthDbContext : IdentityDbContext
{
  public BGWalksAuthDbContext(DbContextOptions<BGWalksAuthDbContext> options)
      : base(options)
  {

  }
  protected override void OnModelCreating(ModelBuilder builder)
  {
    // fe8be1be-d960-4b6b-8871-8e9c9efc8208       - user
    // b1474091-fb1c-497c-93a8-100277ae0833         - admin

    var userGuid = "fe8be1be-d960-4b6b-8871-8e9c9efc8208";
    var adminGuid = "b1474091-fb1c-497c-93a8-100277ae0833";

    // seeding roles
    base.OnModelCreating(builder);
    var roles = new List<IdentityRole> {
      new IdentityRole { Id = userGuid,ConcurrencyStamp=userGuid, Name = "Admin", NormalizedName = "ADMIN" },
      new IdentityRole { Id = adminGuid,ConcurrencyStamp=adminGuid, Name = "User", NormalizedName = "USER" }
    };



    builder.Entity<IdentityRole>().HasData(roles);
  }
}