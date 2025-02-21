namespace BGwalks.API.Models.DTO
{
    public class WalkCreateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Distance { get; set; }
        public string? ImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid regionId { get; set; }


    }
}