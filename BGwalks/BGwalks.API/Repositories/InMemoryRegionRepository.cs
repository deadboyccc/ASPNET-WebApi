// FOR TESTING PURPOSES, THERE IS NO IN MEMORY REPO
using BGwalks.API.Models.Domain;

namespace BGwalks.API.Repositories;
class InMemoryRegionRepository : IRegionRepository
{
  public Task<Region> AddAsync(Region region)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<bool> ExistsAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<List<Region>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<Region?> GetByIdAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<Region?> UpdateAsync(Guid id, Region region)
  {
    throw new NotImplementedException();
  }
}