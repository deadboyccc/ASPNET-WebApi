
using BGwalks.API.Data;
using BGwalks.API.Models.Domain.Models;

namespace BGwalks.API.Repositories;
public class SQLImageRespository : IImageRepository
{
  private readonly IWebHostEnvironment _webHostEnvironment;
  private readonly IHttpContextAccessor _httpctx;
  private readonly BGWalksDbContext _bGWalksDbContext;

  public SQLImageRespository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpctx, BGWalksDbContext bGWalksDbContext)
  {
    _webHostEnvironment = webHostEnvironment;
    _httpctx = httpctx;
    _bGWalksDbContext = bGWalksDbContext;
  }
  public async Task<ImageDomain> AddAsync(ImageDomain imageDomain)
  {
    // local file path to be saved to
    var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{imageDomain.ImageName}{imageDomain.ImageExtention}");


    // open stream then copy from upload to local file system
    using var stream = new FileStream(localFilePath, FileMode.Create);
    await imageDomain.ImageFile!.CopyToAsync(stream);


    //create image url

    imageDomain.ImageUrl = $"{_httpctx.HttpContext?.Request?.Scheme}://{_httpctx.HttpContext?.Request?.Host}{_httpctx.HttpContext?.Request.PathBase}/images/{imageDomain.ImageName}.{imageDomain.ImageExtention}";

    await _bGWalksDbContext.Images.AddAsync(imageDomain);
    await _bGWalksDbContext.SaveChangesAsync();

    return imageDomain;
  }




}