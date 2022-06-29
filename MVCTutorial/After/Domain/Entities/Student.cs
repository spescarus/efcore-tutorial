using Domain.Base;
using Domain.Entities.ValueObjects;

namespace Domain.Entities;

public class Student : Entity
{
    public PersonName              Name           { get; private set; }
    public string                  FullName       => Name.LastName + " " + Name.FirstMidName;
    public Email                   Email          { get; private set; }
    public DateTime                EnrollmentDate { get; private set; }
    public ICollection<Enrollment> Enrollments    { get; private set; }

    private Student()
    {
    }

    private Student(PersonName name,
                    Email      email,
                    DateTime   enrollmentDate)
    {
        Name           = name;
        Email          = email;
        EnrollmentDate = enrollmentDate;
    }


    public static Result<Student> Create(PersonName name,
                                  Email      email,
                                  DateTime   enrollmentDate)
    {
        if (name == null)
            return Result.Failure<Student>("Student name is mandatory");

        if (email == null)
            return Result.Failure<Student>("Email address is mandatory");

        var student = new Student(name, email, enrollmentDate);

        return Result.Success(student);
    }

    public Result<Student> Update(PersonName name,
                                  Email      email,
                                  DateTime   enrollmentDate)
    {
        if (name == null)
            return Result.Failure<Student>("Student name is mandatory");

        if (email == null)
            return Result.Failure<Student>("Email address is mandatory");

        Name = name;
        Email          = email;
        EnrollmentDate = enrollmentDate;

        return Result.Success(this);
    }
}