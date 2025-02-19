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
}