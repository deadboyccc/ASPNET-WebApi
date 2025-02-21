using System.ComponentModel.DataAnnotations;

namespace BGwalks.API.Models.DTO;

public class regionCreateDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Name has to be a minimum of 3 characters")]
    [MaxLength(20, ErrorMessage = "max length can't exceed 20 characters")]


    public string? Name { get; set; }

    [MinLength(3, ErrorMessage = "Name has to be a minimum of 3 characters")]
    [MaxLength(40, ErrorMessage = "max length can't exceed 40 characters")]
    public string? RegionImageUrl { get; set; }

}
