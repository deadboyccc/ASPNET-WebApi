using BGwalks.API.Models.Domain;

using Microsoft.EntityFrameworkCore;

namespace BGwalks.API.Data;
public class BGWalksDbContext : DbContext
{

    public BGWalksDbContext(DbContextOptions<BGWalksDbContext> dbContextOptions)
        : base(dbContextOptions)
    {


    }
    public DbSet<DifficultyDomain> Difficulties { get; set; }
    public DbSet<WalkDomain> Walks { get; set; }
    public DbSet<RegionDomain> Regions { get; set; }

    // Data Seeding
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // DI on createHook
        base.OnModelCreating(modelBuilder);

        // seeding difficulty data [easy,medium, hard]
        var Difficulties = new List<DifficultyDomain>(){
                new DifficultyDomain{Id = Guid.Parse("4b37fb9d-c26b-49e4-8e2a-1cf8da41ec9c"), Name = "Easy"},
                new DifficultyDomain{Id = Guid.Parse("30d6a99c-8787-4919-8552-c145a7214a3a"), Name = "Medium"},
                new DifficultyDomain{Id = Guid.Parse("e1709571-0df8-45c9-8a7f-98f2b194a046"), Name = "Hard"}
            };
        // seed to db
        modelBuilder.Entity<DifficultyDomain>().HasData(Difficulties);


        // seeding regions data
        var regions = new List<RegionDomain>(){
                new RegionDomain{Id = Guid.Parse("6d9a5989-5262-4403-8998-a4075088c650"), Name = "North America", RegionImageUrl = "https://example.com/north-america.jpg"},
                new RegionDomain{Id = Guid.Parse("4459f691-b307-4543-8765-1413477a6b84"), Name = "South America", RegionImageUrl = "https://example.com/south-america.jpg"},

        };
        // seed to db
        modelBuilder.Entity<RegionDomain>().HasData(regions);



    }
}