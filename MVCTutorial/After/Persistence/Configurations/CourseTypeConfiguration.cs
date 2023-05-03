using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class CourseTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses")
               .HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(p => p.Title)
               .HasColumnName("Title")
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(p => p.Credits)
               .HasColumnName("Credits")
               .IsRequired();
    }
}