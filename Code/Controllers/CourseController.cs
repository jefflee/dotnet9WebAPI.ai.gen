using Microsoft.AspNetCore.Mvc;
using dotnet9WebAPI.ai.gen.Models;
using dotnet9WebAPI.ai.gen.DTOs;

namespace dotnet9WebAPI.ai.gen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ContosoUniversityContext context, ILogger<CourseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /Course
        [HttpGet(Name = "GetCourses")]
        [ProducesResponseType(typeof(IEnumerable<CourseReadDto>), 200)]
        public ActionResult<IEnumerable<CourseReadDto>> GetCourses()
        {
            var courses = _context.Courses.Select(c => new CourseReadDto
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Credits = c.Credits
            }).ToList();
            return Ok(courses);
        }

        // GET: /Course/{id}
        [HttpGet("{id}", Name = "GetCourseById")]
        [ProducesResponseType(typeof(CourseReadDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<CourseReadDto> GetCourseById(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();
            return Ok(new CourseReadDto
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Credits = course.Credits
            });
        }

        // POST: /Course
        [HttpPost(Name = "CreateCourse")]
        [ProducesResponseType(typeof(CourseReadDto), 201)]
        [ProducesResponseType(400)]
        public ActionResult<CourseReadDto> CreateCourse([FromBody] CourseCreateDto dto)
        {
            var course = new Course { Title = dto.Title, Credits = dto.Credits };
            _context.Courses.Add(course);
            _context.SaveChanges();
            var result = new CourseReadDto
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Credits = course.Credits
            };
            return CreatedAtRoute("GetCourseById", new { id = course.CourseId }, result);
        }

        // PUT: /Course/{id}
        [HttpPut("{id}", Name = "UpdateCourse")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCourse(int id, [FromBody] CourseUpdateDto dto)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();
            course.Title = dto.Title;
            course.Credits = dto.Credits;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: /Course/{id}
        [HttpDelete("{id}", Name = "DeleteCourse")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
