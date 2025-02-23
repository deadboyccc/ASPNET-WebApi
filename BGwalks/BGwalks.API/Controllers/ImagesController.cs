using BGwalks.API.Models.Domain.Models;
using BGwalks.API.Models.DTO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BGwalks.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
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
        // ImageUrl =
      };
      await Task.Delay(100);
      return Ok();


    }
    return BadRequest(ModelState);

  }

  private void ValidateImageUpload(ImageUploadRequestDto req)
  {
    // extension validation
    var allowedExtentions = new List<string> {
      ".jpg", ".jpeg", ".png"
    };
    if (req.ImageName == null || !allowedExtentions.Contains(Path.GetExtension(req.ImageName)))
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
