using Asp.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BGwalks.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class StudentsController : ControllerBase
    {
        /// <summary>
        /// Gets all students for API version 1.0.
        /// </summary>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllStudentsV1()
        {
            // Implement v1 logic here.
            return Ok("v1");
        }

        /// <summary>
        /// Gets all students for API version 2.0.
        /// </summary>
        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllStudentsV2()
        {
            // Implement v2 logic here.
            return Ok("v2");
        }

        // /// <summary>
        // /// Deletes a student.
        // /// </summary>
        // /// <param name="id">The ID of the student to delete.</param>
        // [HttpDelete("{id}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // public IActionResult DeleteStudent(int id)
        // {
        //     // Implement delete logic here.
        //     return NoContent();
        // }
    }
}
