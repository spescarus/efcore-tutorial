using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class InstructorTypeConfiguration : BasicEntityTypeConfiguration<Instructor>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(pp => pp.FirstMidName)
                .HasColumnName("FirstMidName")
                .HasMaxLength(50)
                .IsRequired();

            name.Property(pp => pp.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.Ignore(p => p.FullName);


        builder.Property(p => p.Email)
               .HasConversion(p => p.Value, email => Email.Create(email)
                                                          .Value)
               .HasColumnName("Email")
               .IsRequired();

        builder.Property(p => p.HireDate)
               .HasColumnName("HireDate")
               .IsRequired();
    }
}