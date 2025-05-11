using System.ComponentModel.DataAnnotations;

namespace FitFormServer.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; } = null!;

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        [MaxLength(500, ErrorMessage = "Comment must not exceed 500 characters.")]
        public string Comment { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; } //nr. de stele 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
