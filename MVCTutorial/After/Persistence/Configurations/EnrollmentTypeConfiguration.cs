using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class EnrollmentTypeConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments")
               .HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.HasOne(p => p.Student)
               .WithMany(p => p.Enrollments);

        builder.HasOne(p => p.Course)
               .WithMany();

        builder.Property(p => p.Grade)
               .HasColumnName("Grade");
    }
}