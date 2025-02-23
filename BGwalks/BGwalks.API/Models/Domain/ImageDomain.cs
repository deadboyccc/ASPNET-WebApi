using System.ComponentModel.DataAnnotations.Schema;

namespace BGwalks.API.Models.Domain.Models;
public class ImageDomain
{
  public Guid Id { get; set; }
  [NotMapped]
  public IFormFile? ImageFile { get; set; }
  public string? ImageName { get; set; }
  public string? ImageDescription { get; set; }
  public string? ImageExtention { get; set; }
  public long FileSizeInBytes { get; set; }
  public string? ImageUrl { get; set; }

}