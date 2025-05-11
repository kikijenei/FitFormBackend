using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitFormServer.Models
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty;

        //cine a utilizat acest workout

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [Required]
        public DateTime DateAndTime { get; set; } = DateTime.UtcNow;

        [Required]
        public int Duration { get; set; }  // in minute

        public string Description { get; set; } = string.Empty;

        //ce exercitii au fost in workout-ul respectiv
        public List<Exercise>? Exercises { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
