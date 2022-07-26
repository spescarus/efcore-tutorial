using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class CourseAssignmentTypeConfiguration : IEntityTypeConfiguration<CourseAssignment>
{
    public void Configure(EntityTypeBuilder<CourseAssignment> builder)
    {
        builder.ToTable("CourseAssignments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.Property(p => p.InstructorId)
               .ValueGeneratedNever()
               .HasColumnName("InstructorId")
               .IsRequired();

        builder.HasOne(p => p.Instructor)
               .WithMany(p => p.CourseAssignments)
               .HasForeignKey(fk => fk.InstructorId)
               .IsRequired();

        builder.Property(p => p.CourseId)
               .HasColumnName("CourseId")
               .IsRequired();

        builder.HasOne(p => p.Course)
               .WithMany(p => p.CourseAssignments)
               .HasForeignKey(fk => fk.CourseId)
               .IsRequired();
    }
}