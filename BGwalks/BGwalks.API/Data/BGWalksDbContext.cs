using BGwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BGwalks.API.Data
{
    public class BGWalksDbContext:DbContext
    {

        public BGWalksDbContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions)
        {

            
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }

    }
}
