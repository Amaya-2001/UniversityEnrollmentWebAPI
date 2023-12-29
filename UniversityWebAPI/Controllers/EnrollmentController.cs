using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UniversityWebAPI.Context;
using UniversityWebAPI.Models;

namespace UniversityWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet("getAllEnrollments")]
        public async Task<ActionResult<Enrollment>> GetAllEnrollments()
        {
            return Ok(await _context.Enrollments.ToListAsync());
        }
        [HttpPost("create")]
        public async Task<ActionResult<Enrollment>> CreateEnrollment([FromBody] Enrollment enrollment)
        {
            try
            {
                
                var course = await _context.Courses.FindAsync(enrollment.CourseId);
                var student = await _context.Students.FindAsync(enrollment.StudentId);

                if (course == null || student == null)
                {
                    return BadRequest("Invalid course or student details");
                }

                
                enrollment.Course = course;
                enrollment.Student = student;

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();

                return CreatedAtAction("CreateEnrollment", new { id = enrollment.EnrollmentId }, enrollment);
            }
            catch (Exception ex)
            {
                
                Debug.WriteLine($"Error creating enrollment: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("details")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentDetails(int enrollmentId)
        {
            return await _context.Enrollments.FirstOrDefaultAsync(m => m.EnrollmentId == enrollmentId);
        }

        [HttpPut("edit/{enrollmentId}")]
        public async Task<ActionResult<Enrollment>> EditEnrollment(int enrollmentId, [FromBody] Enrollment updatedEnrollment)
        {
            if (enrollmentId <= 0)
            {
                return NotFound();

            }


            var existingEnrollment = await _context.Enrollments.FindAsync(enrollmentId);
            if (existingEnrollment == null)
            {
                return NotFound("Enrollment not found");
            }
            _context.Entry(existingEnrollment).CurrentValues.SetValues(updatedEnrollment);

            await _context.SaveChangesAsync();
            return Ok(updatedEnrollment);


        }
        [HttpDelete("delete/{enrollmentId}")]
        public async Task<ActionResult<Course>> DeleteCourse([FromRoute] int enrollmentId)
        {
            var enrollment = _context.Enrollments.FirstOrDefault(c => c.EnrollmentId == enrollmentId);

            if (enrollment == null)
            {
                return NotFound("Enrollment not found");
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return Ok(enrollment);
        }

    }
}
