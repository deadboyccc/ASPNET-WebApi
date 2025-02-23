
using BGwalks.API.Models.Domain.Models;

namespace BGwalks.API.Repositories;
public class SQLImageRespository : IImageRepository
{
  public Task<ImageDomain> UpdateAsync(ImageDomain imageDomain)
  {
    // Your implementation here
    return Task.FromResult(new ImageDomain());
  }
}