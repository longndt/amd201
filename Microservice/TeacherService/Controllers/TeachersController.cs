using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherService.Data;
using TeacherService.Models;

namespace TeacherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherServiceContext _context;

        public TeachersController(TeacherServiceContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeacher()
        {
            return await _context.Teacher.ToListAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            _context.Teacher.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.Id == id);
        }
    }
}
