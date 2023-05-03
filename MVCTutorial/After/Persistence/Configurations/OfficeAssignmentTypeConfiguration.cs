using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class OfficeAssignmentTypeConfiguration : BasicEntityTypeConfiguration<OfficeAssignment>
{
    protected override void ConfigureEntity(EntityTypeBuilder<OfficeAssignment> builder)
    {
        builder.ToTable("OfficeAssignments")
               .HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.Property(p => p.InstructorId)
               .HasColumnName("InstructorId")
               .IsRequired();

        builder.Property(p => p.Location)
               .HasColumnName("Location")
               .IsRequired();
    }
}