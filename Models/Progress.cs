using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitFormServer.Models
{
    public class Progress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public float Weight { get; set; }
        public int WorkoutsPerWeek { get; set; }//de cate ori a fost antrenament pe o saptamana

        public DateTime MeasurementDate { get; set; } = DateTime.UtcNow;
    }
}
