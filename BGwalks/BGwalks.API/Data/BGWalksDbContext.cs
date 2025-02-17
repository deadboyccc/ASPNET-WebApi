using BGwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BGwalks.API.Data
{
    public class BGWalksDbContext : DbContext
    {

        public BGWalksDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {


        }
        public DbSet<DifficultyDomain> Difficulties { get; set; }
        public DbSet<WalkDomain> Walks { get; set; }
        public DbSet<RegionDomain> Regions { get; set; }

    }
}
