using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitFormServer.Data;
using FitFormServer.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitFormServer.Controllers
{
    [Route("api/progress")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProgressController(AppDbContext context)
        {
            _context = context;
        }

        //get api/progress/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProgressForUser(int userId)
        {
            var progressRecords = await _context.ProgressRecords
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.MeasurementDate)
                .ToListAsync();

            return Ok(progressRecords);
        }

        //post api/progress
        [HttpPost]
        public async Task<IActionResult> CreateProgress([FromBody] Progress progress)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.ProgressRecords.Add(progress);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProgressForUser), new { userId = progress.UserId }, progress);
        }

        //put api/progress/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateProgress(int id, [FromBody] Progress updatedProgress)
        {
            if (id != updatedProgress.Id) return BadRequest("Progress ID mismatch.");

            _context.Entry(updatedProgress).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //delete api/progress/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProgress(int id)
        {
            var progress = await _context.ProgressRecords.FindAsync(id);
            if (progress == null) return NotFound();

            _context.ProgressRecords.Remove(progress);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
