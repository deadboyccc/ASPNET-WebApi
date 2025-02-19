using BGwalks.API.Data;
using BGwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BGwalks.API.Repositories;
class SQLWalksRepository : IWalkRepository
{
  private readonly BGWalksDbContext dbContext;

  public SQLWalksRepository(BGWalksDbContext dbContext)
  {
    this.dbContext = dbContext;
  }

  public async Task<WalkDomain> AddAsync(WalkDomain walk)
  {

    await dbContext.Walks.AddAsync(walk);
    await dbContext.SaveChangesAsync();

    return walk;
  }

  public async Task<List<WalkDomain>> GetAllAsync()
  {
    //action is a void delgate, fun is a return-type delegate
    return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
  }

  public async Task<WalkDomain?> GetByIdAsync(Guid id)
  {
    return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == id);
  }

  public async Task<WalkDomain?> UpdateAsync(Guid id, WalkDomain walk)
  {
    // 1. Get existing walk
    var walkToBeUpdated = await dbContext.Walks.FindAsync(id);
    if (walkToBeUpdated == null)
    {
      // throw new KeyNotFoundException($"Walk with Id {id} not found.") - Implement Later now just simple returns
      return null;
    }
    // 2. Update properties.
    walkToBeUpdated.Name = walk.Name;
    walkToBeUpdated.Description = walk.Description;
    walkToBeUpdated.DifficultyId = walk.DifficultyId;
    walkToBeUpdated.Region = walk.Region;
    // 3. Save changes.
    await dbContext.SaveChangesAsync();
    // 4. Return updated walk.
    return walkToBeUpdated;
  }
}