using Domain.Entities;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class InstructorTypeConfiguration : BasicEntityTypeConfiguration<Instructor>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors")
               .HasKey(p => p.Id);

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

        builder.Property(p => p.HireDate)
               .HasColumnName("HireDate")
               .IsRequired();

        builder.HasMany(p => p.Courses)
               .WithMany(p => p.Instructors)
               .UsingEntity("CourseAssignments",
                            l => l.HasOne(typeof(Course))
                                  .WithMany()
                                  .HasForeignKey("CourseId")
                                  .HasPrincipalKey(nameof(Course.Id)),
                            r => r.HasOne(typeof(Instructor))
                                  .WithMany()
                                  .HasForeignKey("InstructorId")
                                  .HasPrincipalKey(nameof(Instructor.Id)),
                            j => j.HasKey("CourseId", "InstructorId"));

        builder.HasOne(p => p.OfficeAssignment)
               .WithOne()
               .HasForeignKey<OfficeAssignment>(p => p.InstructorId);
    }
}