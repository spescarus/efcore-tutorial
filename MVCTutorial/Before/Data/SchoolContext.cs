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
        modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignments")
                    .HasKey(p => new {p.CourseId, p.InstructorId});

        modelBuilder.Entity<CourseAssignment>()
                    .HasOne(p => p.Course)
                    .WithMany(p => p.CourseAssignments)
                    .HasForeignKey(fk => fk.CourseId);

        modelBuilder.Entity<CourseAssignment>()
                    .HasOne(p => p.Instructor)
                    .WithMany(p => p.CourseAssignments)
                    .HasForeignKey(fk => fk.InstructorId);
    }
}