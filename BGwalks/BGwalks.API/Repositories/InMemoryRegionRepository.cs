// FOR TESTING PURPOSES, THERE IS NO IN MEMORY REPO
using BGwalks.API.Models.Domain;

namespace BGwalks.API.Repositories;
class InMemoryRegionRepository : IRegionRepository
{
  public Task<RegionDomain> AddAsync(RegionDomain region)
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

  public Task<List<RegionDomain>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<RegionDomain?> GetByIdAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<RegionDomain?> UpdateAsync(Guid id, RegionDomain region)
  {
    throw new NotImplementedException();
  }
}