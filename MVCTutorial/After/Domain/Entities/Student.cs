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


    public static Result<Student> Create(string   firstMidName,
                                         string   lastName,
                                         string   studentEmail,
                                         DateTime enrollmentDate)
    {

        var name = PersonName.Create(firstMidName, lastName);

        if (name.IsFailure)
            return Result.Failure<Student>(name.Error);

        var email = Email.Create(studentEmail);

        if (email.IsFailure)
            return Result.Failure<Student>(email.Error);

        var student = new Student(name.Value, email.Value, enrollmentDate);

        return Result.Success(student);
    }

    public Result<Student> Update(string   firstMidName,
                                  string   lastName,
                                  string   studentEmail,
                                  DateTime enrollmentDate)
    {
        var name = PersonName.Create(firstMidName, lastName);

        if (name.IsFailure)
            return Result.Failure<Student>(name.Error);

        var email = Email.Create(studentEmail);

        if (email.IsFailure)
            return Result.Failure<Student>(email.Error);

        Name           = name.Value;
        Email          = email.Value;
        EnrollmentDate = enrollmentDate;

        return Result.Success(this);
    }
}