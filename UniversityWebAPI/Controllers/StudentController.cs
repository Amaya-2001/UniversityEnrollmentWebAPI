using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityWebAPI.Context;
using UniversityWebAPI.Models;

namespace UniversityWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext appDbContext)
        {
            _context = appDbContext;

        }
        [HttpGet("getAllStudents")]
        public async Task<ActionResult<Student>> GetAllStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPost("create")]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllStudents", new { id = student.StudentId }, student);
        }
    }

}

