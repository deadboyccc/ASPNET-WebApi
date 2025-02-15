using System.Threading.Tasks.Dataflow;
using BGwalks.API.Data;
using BGwalks.API.Models.Domain;
using BGwalks.API.Models.DTO;
using BGwalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BGwalks.API.Controllers
{
    // /api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // Dependency injection when the controller is created inject the db context that is injected/coming from the asp app
        private readonly BGWalksDbContext _dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(BGWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this._dbContext = dbContext;
            this.regionRepository = regionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // db context implementation 
            // var regions = await _dbContext.Regions.ToListAsync();

            // Repo Design Pattern
            var regions = await regionRepository.GetAllAsync();

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
        [HttpGet]
        [Route("{id:guid}")]


        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // get data from the domain models
            // var region = await _dbContext.Regions.FindAsync(id);

            //using the repo pattern
            var region = await regionRepository.GetByIdAsync(id);

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
        public async Task<IActionResult> CreateRegion([FromBody] regionCreateDto regionCreateDto)
        {
            // create a new region entity (domain model)
            var newRegion = new Region
            {
                Id = Guid.NewGuid(),
                Name = regionCreateDto.Name,
                RegionImageUrl = regionCreateDto.RegionImageUrl
            };

            // save to the database - using the db context
            // await _dbContext.Regions.AddAsync(newRegion);
            // await _dbContext.SaveChangesAsync();

            // using the repo pattern
            await regionRepository.AddAsync(newRegion);

            //map domain to DTO
            var regionDto = new RegionDto
            {
                Id = newRegion.Id,
                Name = newRegion.Name,
                RegionImageUrl = newRegion.RegionImageUrl
            };

            // return 201 with the new region id
            // action name = nameof(GetById) - sending the id=newRegion.id to the GetById to create the resource header
            /// regionDto then is sent to the client
            return CreatedAtAction(nameof(GetById), new { id = newRegion.Id }, regionDto);


        }

        // Updating a region 
        [HttpPatch]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] RegionUpdateDto regionUpdateDto)
        {
            // find the region in the database - using the dbContext
            // var region = await _dbContext.Regions.FindAsync(id);

            // using the repo pattern
            var region = await regionRepository.GetByIdAsync(id);

            // if not found, return 404
            if (region == null)
            {
                return NotFound();
            }

            // update the region properties in the domain model with the passed DTO 
            region.Name = regionUpdateDto.Name;
            region.RegionImageUrl = regionUpdateDto.RegionImageUrl;

            // save the updated region to the database
            await _dbContext.SaveChangesAsync();


            // Convert domain model to DTO(refactor DRY)
            RegionDto regionDto = new()
            {
                Id = region.Id,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // return 200 with the updated region
            return Ok(regionDto);

            // return 204 (with/without content)
            // return NoContent();

        }
        // Delete a region by Id
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            // find the region in the database - using the db context
            // var region = await _dbContext.Regions.FindAsync(id);

            // using the repo pattern
            var region = await regionRepository.GetByIdAsync(id);

            // if not found, return 404
            if (region == null)
            {
                return NotFound();
            }

            // delete the region from the database
            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();

            // return 204
            return NoContent();

        }

    }
}
