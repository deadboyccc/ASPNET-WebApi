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
        [HttpGet]
        public IActionResult GetAll()
        {
            // hard code
            var regions = new List<Region>()
            {
                new Region(){Id=Guid.NewGuid(),Name = "Mansoor"},
                new Region(){Id=Guid.NewGuid(),Name = "Karada"},
                new Region(){Id=Guid.NewGuid(),Name = "New York"}
                
                
            };
            // 200
            return Ok(regions);
        }
        
    }
}
