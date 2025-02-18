using AutoMapper;
using BGwalks.API.Models.Domain;
using BGwalks.API.Models.DTO;
using BGwalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGwalks.API.Controllers;
// /api/walks
[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
  private readonly IMapper mapper;
  private readonly IWalkRepository walkRepository;

  public WalksController(IMapper mapper, IWalkRepository walkRepository)
  {
    this.mapper = mapper;
    this.walkRepository = walkRepository;
  }
  // Create Walk POST: /api/walks
  [HttpPost]
  public async Task<IActionResult> CreateWalk([FromBody] WalkCreateDto walkDto)
  {
    // map DTO to domain model
    var createdWalkDomain = mapper.Map<WalkDomain>(walkDto);

    // repo
    await walkRepository.AddAsync(createdWalkDomain);

    // back to DTO from the created model to send to the client

    return Ok(mapper.Map<WalkGetDto>(createdWalkDomain));
  }
  [HttpGet]
  public IActionResult getAllStudents()
  {
    return Ok("test");

  }
  [HttpDelete]
  [Route("{id}")]
  public IActionResult DeleteStudent([FromRoute] int id)
  {
    return NoContent();
  }

}
