using BGwalks.API.Models.Domain.Models;

public interface IImageRepository
{
  Task<ImageDomain> AddAsync(ImageDomain imageDomain);

}