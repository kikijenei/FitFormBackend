using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitFormServer.Data;
using FitFormServer.Models;

namespace FitFormServer.Controllers
{
    [Route("api/workouts")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly AppDbContext _context;
        public WorkoutController(AppDbContext context)
        {
            _context = context;
        }

        //get api/workouts
        [HttpGet]
        public async Task<IActionResult> GetWorkouts()
        {
            var workouts = await _context.Workouts.Include(w => w.Exercises).ToListAsync();
            return Ok(workouts);
        }

        // get api/workouts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkout(int id)
        {
            var workout = await _context.Workouts
                .Include(w => w.Exercises)
                .Include(w => w.Reviews)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null) return NotFound();
            return Ok(workout);
        }

        // post api/workouts
        [HttpPost]
        public async Task<IActionResult> CreateWorkout([FromBody] Workout workout)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkout), new { id = workout.Id }, workout);
        }

        // post api/workouts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, [FromBody] Workout updatedWorkout)
        {
            if (id != updatedWorkout.Id) return BadRequest("Workout ID mismatch.");

            _context.Entry(updatedWorkout).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //delete api/workouts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null) return NotFound();

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
