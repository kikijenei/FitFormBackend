using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FitFormServer.Models
{
    public class User
    {
        //public Guid ID { get; set; }
        //public string FirstName { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;
        //public string Email { get; set; } = string.Empty;
        //public string PasswordHash { get; set; } = string.Empty;
        //public string Role { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty; //hash, nu parola

        [Required]
        public string Role { get; set; } = "User"; // sau admin, trainer, user simplu

        //date personale progres
        public float Height { get; set; } //cm
        public float Weight { get; set; } //kg
        public DateTime BirthDate { get; set; } //pentru varsta

        //relatii
        public Progress? ProgressRecord { get; set; }
        public List<Workout>? Workouts { get; set; }

        //trainer si client
        public int? TrainerId { get; set; }
        [ForeignKey("TrainerId")]
        public User? Trainer { get; set; } //daca e user simplu - client, poate avea antrenor
        public ICollection<User> Clients { get; set; } = new List<User>();//daca e antrenor, mai multi clienti
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
 