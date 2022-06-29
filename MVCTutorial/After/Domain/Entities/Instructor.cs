using Domain.Base;
using Domain.Entities.ValueObjects;

namespace Domain.Entities;

public class Instructor : Entity
{
    public PersonName                    Name              { get; private set; }
    public string                        FullName          => Name.LastName + " " + Name.FirstMidName;
    public Email                         Email             { get; private set; }
    public DateTime                      HireDate          { get; private set; }
    public ICollection<CourseAssignment> CourseAssignments { get; private set; }
    public OfficeAssignment              OfficeAssignment  { get; private set; }

    private Instructor()
    {
    }

    private Instructor(PersonName name,
                       Email      email,
                       DateTime   hireDate)
    {
        Name     = name;
        Email    = email;
        HireDate = hireDate;
    }


    public Result<Instructor> Create(PersonName name,
                                     Email      email,
                                     DateTime   enrollmentDate)
    {
        var student = new Instructor(name, email, enrollmentDate);

        return Result.Success(student);
    }

    public Result Update(PersonName name,
                         Email      email,
                         DateTime   hireDate)
    {
        Name     = name;
        Email    = email;
        HireDate = hireDate;

        return Result.Success();
    }
}