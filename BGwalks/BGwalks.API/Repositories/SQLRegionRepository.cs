using BGwalks.API.Data;
using BGwalks.API.Models.Domain;
using BGwalks.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BGwalks.API.Repositories;
public class SQLRegionRepository : IRegionRepository
{
  // Dependency Injection 
  private readonly BGWalksDbContext dbContext;
  public SQLRegionRepository(BGWalksDbContext dbContext)
  {
    this.dbContext = dbContext;
  }


  // Create a region
  public async Task<Region> AddAsync(Region region)
  {
    await dbContext.Regions.AddAsync(region);
    await dbContext.SaveChangesAsync();
    return region;
  }

  // Delete a region
  public async Task DeleteAsync(Guid id)
  {
    var regionToDelete = await dbContext.Regions.FindAsync(id);
    if (regionToDelete != null)
    {
      dbContext.Regions.Remove(regionToDelete);
      await dbContext.SaveChangesAsync();
    }
  }

  // check if A region Exists
  public async Task<bool> ExistsAsync(Guid id)
  {
    return await dbContext.Regions.AnyAsync(r => r.Id == id);
  }

  // Get all regions
  public async Task<List<Region>> GetAllAsync()
  {
    return await dbContext.Regions.ToListAsync();

  }

  // Get a region by id
  public async Task<Region?> GetByIdAsync(Guid id)
  {
    var region = await dbContext.Regions.FindAsync(id);
    if (region == null)
    {
      // throw new KeyNotFoundException($"Region with Id {id} not found.");
      return null;
    }
    return region;

  }

  // Update a region
  public async Task<Region?> UpdateAsync(Region region)
  {
    // 1. Get existing region.
    var regionToBeUpdated = await dbContext.Regions.FindAsync(region.Id);

    // 2. Handle missing region.
    if (regionToBeUpdated == null)
    {
      // returning Null for now until we create a global error handling middlware 
      return null;
      // throw new KeyNotFoundException($"Region with Id {region.Id} not found.");
    }

    // 3. Update properties.
    regionToBeUpdated.Name = region.Name;
    regionToBeUpdated.RegionImageUrl = region.RegionImageUrl;

    // 4. Save changes.
    await dbContext.SaveChangesAsync();

    // 5. Return updated region.
    return regionToBeUpdated;
  }

}