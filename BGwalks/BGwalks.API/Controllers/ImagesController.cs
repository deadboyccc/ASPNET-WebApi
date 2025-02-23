using BGwalks.API.Models.Domain.Models;
using BGwalks.API.Models.DTO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BGwalks.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
  private readonly IImageRepository _imageRepository;

  public ImagesController(IImageRepository imageRepository)
  {
    _imageRepository = imageRepository;
  }
  // POST: /api/Images/upload/
  [HttpPost]
  [Route("upload")]
  public async Task<IActionResult> UploadImageAsync([FromForm] ImageUploadRequestDto req)
  {
    ValidateImageUpload(req);
    if (ModelState.IsValid)
    {
      // convert dto to image domain model
      var
      ImageDomainModel = new ImageDomain
      {
        Id = Guid.NewGuid(),
        ImageFile = req.ImageFile,
        ImageExtention = Path.GetExtension(req.ImageFile?.FileName),
        FileSizeInBytes = req.ImageFile!.Length,
        ImageName = req.ImageName,
        ImageDescription = req.ImageDescription,
      };
      // using repo to save the image to the db + file system
      await _imageRepository.AddAsync(ImageDomainModel);


      return Ok(ImageDomainModel);


    }
    return BadRequest(ModelState);

  }

  private void ValidateImageUpload(ImageUploadRequestDto req)
  {
    // extension validation
    var allowedExtentions = new List<string> {
      ".jpg", ".jpeg", ".png"
    };
    Console.WriteLine(req.ImageName);
    Console.WriteLine(Path.GetExtension(req.ImageName));
    if (req.ImageName == null || !allowedExtentions.Contains(Path.GetExtension(req.ImageFile!.FileName)))
    {
      ModelState.AddModelError("ImageFile", "Invalid file extension. Only JPG, JPEG, and PNG are allowed.");
      return;

    }
    // size validation
    if (req.ImageFile?.Length > 10 * 1024 * 1024)
    {
      ModelState.AddModelError("ImageFile", "File size exceeds 10MB.");
      return;
    }
  }

}
