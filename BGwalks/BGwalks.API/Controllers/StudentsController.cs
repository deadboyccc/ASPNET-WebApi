using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BGwalks.API.Controllers
// TESTING - NOT AN APP RESOURCE
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult getAllStudents()
        {
            var studentNames = new string[] { "Ahmed", "Joe" };
            return Ok(studentNames);

        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return NoContent();
        }

    }
}
