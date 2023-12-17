using Microsoft.EntityFrameworkCore;
using UniversityWebAPI.Models;

namespace UniversityWebAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        //helps to take the entity
        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Student>().ToTable("student");
            modelBuilder.Entity<Course>().ToTable("course");
            modelBuilder.Entity<Enrollment>().ToTable("enrollment");

        }
    }
}
