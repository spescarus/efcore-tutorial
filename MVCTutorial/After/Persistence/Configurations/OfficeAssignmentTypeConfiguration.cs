using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class OfficeAssignmentTypeConfiguration : BasicEntityTypeConfiguration<OfficeAssignment>
{
    protected override void ConfigureEntity(EntityTypeBuilder<OfficeAssignment> builder)
    {
        builder.ToTable("OfficeAssignments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.Property(p => p.InstructorId)
               .HasColumnName("InstructorId")
               .IsRequired();

        builder.HasOne(p => p.Instructor)
               .WithOne(p => p.OfficeAssignment)
               .HasForeignKey<OfficeAssignment>(fk => fk.InstructorId);
    }
}