using BGwalks.API.Data;
using BGwalks.API.Models.Domain;
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
            var regions = _dbContext.Regions.ToList();

            // 200
            return Ok(regions);
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

            // 200 if found, 404 if not found
            if (region == null)
            {
                // 404 
                return NotFound();
            }
            // return DTO
            return Ok(region);

        }

    }

}
