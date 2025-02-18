using AutoMapper;
using BGwalks.API.Models.Domain;
using BGwalks.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BGwalks.API.Controllers;
// /api/walks
[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
  private readonly IMapper mapper;

  WalksController(IMapper mapper)
  {
    this.mapper = mapper;
  }
  // Create Walk POST: /api/walks
  [HttpPost]
  public async Task<IActionResult> CreateWalk([FromBody] WalkCreateDto walkDto)
  {
    // map DTO to domain model
    var createdWalkDomain = mapper.Map<WalkDomain>(walkDto);

    // repo

    return Ok(walk);
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
