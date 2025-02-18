using AutoMapper;
using BGwalks.API.Models.Domain;
using BGwalks.API.Models.DTO;
using BGwalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BGwalks.API.Controllers;
// /api/walks
[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
  private readonly IMapper mapper;
  private readonly IWalkRepository walkRepository;

  WalksController(IMapper mapper, IWalkRepository walkRepository)
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
    var walkCreatedDto = mapper.Map<WalkGetDto>(createdWalkDomain);

    return Ok(walkCreatedDto);
  }
  [HttpGet]
  public IActionResult getAllStudents()
  {
    var studentNames = new string[] { "Ahmed", "Joe" };
    return Ok(studentNames);

  }
  [HttpDelete]
  [Route("{id}")]
  public IActionResult DeleteStudent([FromRoute] int id)
  {
    return NoContent();
  }

}
