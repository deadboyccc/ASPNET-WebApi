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
}