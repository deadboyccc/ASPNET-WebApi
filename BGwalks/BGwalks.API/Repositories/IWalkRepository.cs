using BGwalks.API.Models.Domain;

namespace BGwalks.API.Repositories;
public interface IWalkRepository
{
    Task<List<WalkDomain>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, string? sortOrder = null);
    Task<WalkDomain?> GetByIdAsync(Guid id);
    Task<WalkDomain> AddAsync(WalkDomain walk);
    Task<WalkDomain?> UpdateAsync(Guid id, WalkDomain walk);
    Task<WalkDomain?> DeleteAsync(Guid id);
}