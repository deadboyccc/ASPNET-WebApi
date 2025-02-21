using BGwalks.API.Models.Domain;
using BGwalks.API.Models.DTO;

namespace BGwalks.API.Models.DTO;
public class WalkGetDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Distance { get; set; }
    public string? ImageUrl { get; set; }



    //navigation properties for the dto is always a set of 0 or more dtos
    public DifficultyGetDto? Difficulty { get; set; }
    public RegionGetDto? Region { get; set; }

}