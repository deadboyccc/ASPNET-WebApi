using System.ComponentModel.DataAnnotations;

namespace BGwalks.API.Models.DTO;
public class ImageUploadRequestDto
{
  [Required]
  public IFormFile? ImageFile { get; set; }
  [Required]
  public string? ImageName { get; set; }

  public string? ImageDescription { get; set; }

}