using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitFormServer.Data;
using FitFormServer.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitFormServer.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        // get api/reviews/workout/{workoutId}
        [HttpGet("workout/{workoutId}")]
        public async Task<IActionResult> GetReviewsForWorkout(int workoutId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.WorkoutId == workoutId)
                .Include(r => r.User)
                .ToListAsync();

            return Ok(reviews);
        }

        // post api/reviews
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviewsForWorkout), new { workoutId = review.WorkoutId }, review);
        }

        // delete api/reviews/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
