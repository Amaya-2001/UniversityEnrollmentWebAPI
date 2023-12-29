using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityWebAPI.Context;
using UniversityWebAPI.Models;

namespace UniversityWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext appDbContext)
        {
            _context = appDbContext;
            
        }
        [HttpGet("getAllCourse")]
        public async Task<ActionResult<Course>> GetAllCourse()
        {
            return Ok(await _context.Courses.ToListAsync());
        }

        [HttpPost("create")]
        public async Task<ActionResult<Course>> AddCourse([FromBody] Course course )
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddCourse), new { id = course.CourseId }, course);
        }
        [HttpGet("details")]
        public async Task<ActionResult<Course>> GetCourseDetails(int courseId)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
        }

        [HttpPut("edit/{courseId}")]
        public async Task<ActionResult<Course>> EditCourse([FromRoute] int courseId, [FromBody] Course editCourse)
        {
            if (courseId <= 0)
            {
                return BadRequest("Invalid courseId");
            }

            
            var existingCourse = await _context.Courses.FindAsync(courseId);

            if (existingCourse == null)
            {
                return NotFound("Course not found");
            }

          
            _context.Entry(existingCourse).CurrentValues.SetValues(editCourse);

            
            await _context.SaveChangesAsync();

            return Ok(existingCourse);
        }

        [HttpDelete("delete/{courseId}")]
        public async Task<ActionResult<Course>> DeleteCourse([FromRoute] int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);

            if (course == null)
            {
                return NotFound("Course not found");
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

    }
}
