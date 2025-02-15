using BGwalks.API.Data;
using BGwalks.API.Models.Domain;
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
    throw new NotImplementedException();
  }

  // Delete a region
  public async Task DeleteAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  // check if A region Exists
  public Task<bool> ExistsAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  // Get all regions
  public async Task<List<Region>> GetAllAsync()
  {
    return await dbContext.Regions.ToListAsync();

  }

  // Get a region by id
  public Task<Region> GetByIdAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  // Update a region
  public Task UpdateAsync(Region region)
  {
    throw new NotImplementedException();
  }
}