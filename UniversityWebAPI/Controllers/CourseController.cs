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

        [HttpPut("edit")]
        public async Task<ActionResult<Course>> EditStudent(string courseId, [FromBody] Course editCourse)
        {
            if(courseId == null)
            {
                return BadRequest();
            }
            _context.Courses.Update(editCourse);
            await _context.SaveChangesAsync();
            return Ok(editCourse);
        }
        [HttpDelete("delete")]

        public async Task<ActionResult<Course>> DeleteCourse(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId); 
            _context.Courses.Remove(course);
            return Ok(course);
        }
    }
}
