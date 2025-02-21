using BGwalks.API.Data;
using BGwalks.API.Migrations;
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

  public async Task<WalkDomain?> DeleteAsync(Guid id)
  {
    var walkToDelete = await dbContext.Walks.FindAsync(id);
    if (walkToDelete != null)
    {
      var deletedWalk = dbContext.Walks.Remove(walkToDelete);
      await dbContext.SaveChangesAsync();
      return walkToDelete;
    }
    return null;

  }

  public async Task<List<WalkDomain>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, string? sortOrder = "ASC", int pageNumber = 1, int pageSize = 3)
  {
    var walkQuery = dbContext.Walks
        .Include(w => w.Difficulty)
        .Include(w => w.Region)
        .AsQueryable();

    // filtering
    if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
    {
      if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
      {
        var lowerFilter = filterQuery.ToLower();
        walkQuery = walkQuery.Where(w => w.Name != null && w.Name.ToLower().Contains(lowerFilter));
      }

    }


    // sorting
    if (!string.IsNullOrWhiteSpace(sortBy) && !string.IsNullOrWhiteSpace(sortOrder))
    {
      if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
      {
        walkQuery = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
           ? walkQuery.OrderBy(w => w.Name)
            : walkQuery.OrderByDescending(w => w.Name);
      }
    }


    // pagination | basic formula to skip x items based on page number and page size then take the page size
    walkQuery = walkQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

    return await walkQuery.ToListAsync();
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