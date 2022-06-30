using Domain.Entities;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class StudentTypeConfiguration : BasicEntityTypeConfiguration<Student>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(pp => pp.FirstMidName)
                .HasColumnName("FirstName")
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

        builder.Property(p => p.EnrollmentDate)
               .HasColumnName("EnrollmentDate")
               .IsRequired();
    }
}