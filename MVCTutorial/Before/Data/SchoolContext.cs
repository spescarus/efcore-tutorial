using EfCoreMvcTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMvcTutorial.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

    public DbSet<Course>           Courses           { get; set; }
    public DbSet<Enrollment>       Enrollments       { get; set; }
    public DbSet<Student>          Students          { get; set; }
    public DbSet<Instructor>       Instructors       { get; set; }
    public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
    public DbSet<CourseAssignment> CourseAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable("Courses");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollments");
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Instructor>().ToTable("Instructors");
        modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignments");
        modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignments");

        modelBuilder.Entity<CourseAssignment>()
                    .HasKey(c => new { CourseID = c.CourseId, InstructorID = c.InstructorId });
    }
}