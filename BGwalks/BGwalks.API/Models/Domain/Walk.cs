namespace BGwalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string? ImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid regionId { get; set; }
        


        //navigation properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
