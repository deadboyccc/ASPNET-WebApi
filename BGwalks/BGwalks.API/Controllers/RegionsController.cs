using BGwalks.API.Data;
using BGwalks.API.Models.Domain;
using BGwalks.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BGwalks.API.Controllers
{
    // /api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // Dependency injection when the controller is created inject the db context that is injected/coming from the asp app
        private readonly BGWalksDbContext _dbContext;
        public RegionsController(BGWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            // get all regions from the domain models
            var regions = _dbContext.Regions.ToList();

            //DTO 
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            // 200
            return Ok(regionsDto);
        }


        // get by id (:guid) == typesafety
        [HttpGet("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // get data from the domain models
            var region = _dbContext.Regions.Find(id);

            // DTO implementation  - map domain models to DTOs


            // linq
            // var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);
            var regionDto = new List<RegionDto>();
            if (region != null)
            {
                regionDto.Add(new RegionDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            // 200 if found, 404 if not found
            if (region == null)
            {
                // 404 
                return NotFound();
            }
            // return DTO
            return Ok(regionDto);

        }

        // Create Region controller
        // takes the DTO from the client side
        [HttpPost]
        public IActionResult CreateRegion([FromBody] regionCreateDto regionCreateDto)
        {
            // create a new region entity (domain model)
            var newRegion = new Region
            {
                Id = Guid.NewGuid(),
                Name = regionCreateDto.Name,
                RegionImageUrl = regionCreateDto.RegionImageUrl
            };

            // save to the database
            _dbContext.Regions.Add(newRegion);
            _dbContext.SaveChanges();

            //map domain to DTO
            var regionDto = new RegionDto
            {
                Id = newRegion.Id,
                Name = newRegion.Name,
                RegionImageUrl = newRegion.RegionImageUrl
            };

            // return 201 with the new region id
            // action name = nameof(GetById) = we are using the GetById controller to return the newly craeted
            return CreatedAtAction(nameof(GetById), new { id = newRegion.Id }, regionDto);


        }


    }

}
