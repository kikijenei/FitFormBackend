using FitFormServer.Models;
using Microsoft.EntityFrameworkCore;

namespace FitFormServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Progress> ProgressRecords { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //user-workout
            modelBuilder.Entity<User>()
                .HasMany(u => u.Workouts)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //workout-exercises
            modelBuilder.Entity<Workout>()
                .HasMany(w => w.Exercises)
                .WithOne(e => e.Workout)
                .HasForeignKey(e => e.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);

            //user-progress (1-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.ProgressRecord)
                .WithOne(p => p.User)
                .HasForeignKey<Progress>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            //user-review
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //workout-reviews

            modelBuilder.Entity<Workout>()
                .HasMany(w => w.Reviews)
                .WithOne(r => r.Workout)
                .HasForeignKey(r => r.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
