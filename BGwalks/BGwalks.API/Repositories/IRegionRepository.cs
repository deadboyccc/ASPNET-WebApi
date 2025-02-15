using BGwalks.API.Models.Domain;

namespace BGwalks.API.Repositories;
public interface IRegionRepository
{
  Task<List<Region>> GetAllAsync();
  Task<Region?> GetByIdAsync(Guid id);
  Task<Region> AddAsync(Region region);
  Task<Region?> UpdateAsync(Guid id, Region region);
  Task DeleteAsync(Guid id);
  Task<bool> ExistsAsync(Guid id);


}