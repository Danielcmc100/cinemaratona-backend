using Microsoft.EntityFrameworkCore;
using cinemaratona.Models;

namespace cinemaratona.Data
{
    public class CinemaratonaContext(DbContextOptions<CinemaratonaContext> options) : DbContext(options)
    {
        public DbSet<Event> Event { get; set; }
        public DbSet<Friendship> Friendship { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Review> Review { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Friendship>().ToTable("Friendship");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Review>().ToTable("Review");
        }
    }
}