using BGwalks.API.Models.Domain;

namespace BGwalks.API.Repositories;
public interface IRegionRepository
{
  Task<List<RegionDomain>> GetAllAsync();
  Task<RegionDomain?> GetByIdAsync(Guid id);
  Task<RegionDomain> AddAsync(RegionDomain region);
  Task<RegionDomain?> UpdateAsync(Guid id, RegionDomain region);
  Task DeleteAsync(Guid id);
  Task<bool> ExistsAsync(Guid id);


}