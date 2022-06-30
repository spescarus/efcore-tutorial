using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class EnrollmentTypeConfiguration : BasicEntityTypeConfiguration<Enrollment>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.Property(p => p.CourseId)
               .HasColumnName("CourseId")
               .IsRequired();

        builder.HasOne(p => p.Course)
               .WithMany(p => p.Enrollments)
               .HasForeignKey(p => p.CourseId)
               .IsRequired();

        builder.Property(p => p.StudentId)
               .HasColumnName("StudentId")
               .IsRequired();

        builder.HasOne(p => p.Student)
               .WithMany(p => p.Enrollments)
               .HasForeignKey(fk => fk.StudentId)
               .IsRequired();

        builder.Property(p => p.Grade)
               .HasColumnName("Grade");


    }
}