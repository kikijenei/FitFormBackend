using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitFormServer.Data;
using FitFormServer.Models;

namespace FitFormServer.Controllers
{
    [Route("api/exercises")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExerciseController(AppDbContext context)
        {
            _context = context;
        }

        // get api/exercises
        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await _context.Exercises.ToListAsync();
            return Ok(exercises);
        }

        // get api/exercises/workout/{workoutId}
        [HttpGet("workout/{workoutId}")]
        public async Task<IActionResult> GetExercisesForWorkout(int workoutId)
        {
            var exercises = await _context.Exercises.Where(e => e.WorkoutId == workoutId).ToListAsync();
            return Ok(exercises);
        }

        // post api/exercises
        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] Exercise exercise)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExercisesForWorkout), new { workoutId = exercise.WorkoutId }, exercise);
        }

        // put api/exercises/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] Exercise updatedExercise)
        {
            if (id != updatedExercise.Id) return BadRequest("Exercise ID mismatch.");

            _context.Entry(updatedExercise).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //delete api/exercises/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null) return NotFound();

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
