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
        public async Task<ActionResult<Student>> GetAllStudents(StudentController studentController)
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

        [HttpGet("details")]
        public async Task<ActionResult<Student>> GetStudentDetails(int studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(m => m.StudentId == studentId);
        }

        [HttpPut("edit/{studentId}")]
        public async Task<ActionResult<Student>> EditStudent(int studentId, [FromBody] Student updatedStudent)
        {
            if(studentId <= 0)
            {
                return NotFound();

            }

            
            var existingStudent = await _context.Students.FindAsync(studentId);
            if (existingStudent == null)
            {
                return NotFound("Student not found");
            }
            _context.Entry(existingStudent).CurrentValues.SetValues(updatedStudent);
            
            await _context.SaveChangesAsync();
            return Ok(updatedStudent);

            
        }
        [HttpDelete("delete")]
        public  async Task<ActionResult<Student>> DeleteStudent(int studentId)
        {
            var student =  _context.Students.Find(studentId);
            var deleteStudent =  _context.Students.Remove(student);
            return  Ok(deleteStudent);
        }
    }

}

