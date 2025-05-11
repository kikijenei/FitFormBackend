using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitFormServer.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public Workout Workout { get; set; } = null!;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Repetitions must be a positive number.")]
        public int Repetitions { get; set; } //reps

        [Range(0, int.MaxValue, ErrorMessage = "Sets must be a positive number.")]
        public int Sets { get; set; } //nr.set

        [Range(0, int.MaxValue, ErrorMessage = "Duration must be a positive number.")]
        public int Duration{ get; set; } //in minute

        public string? Description { get; set; }

    }
}
