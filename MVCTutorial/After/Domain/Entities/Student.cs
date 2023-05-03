using Domain.Base;
using Domain.Entities.ValueObjects;

namespace Domain.Entities;

public class Student : Entity
{
    public Name                    Name           { get; private set; }
    public string                  FullName       => $"{Name.LastName} {Name.FirstMidName}";
    public Email                   Email          { get; private set; }
    public DateTime                EnrollmentDate { get; private set; }
    public ICollection<Enrollment> Enrollments    { get; private set; } = new List<Enrollment>();

    private Student()
    {
    }

    private Student(Name     name,
                    Email    email,
                    DateTime enrollmentDate)
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

        var name = Name.Create(firstMidName, lastName);

        if (name.IsFailure)
            return Result.Failure<Student>(name.Error);

        var email = Email.Create(studentEmail);

        if (email.IsFailure)
            return Result.Failure<Student>(email.Error);

        var student = new Student(name.Value, email.Value, enrollmentDate);

        return Result.Success(student);
    }

    public Result<Student> EditPersonalInfo(string firstMidName,
                                            string lastName,
                                            string studentEmail)
    {
        var name = Name.Create(firstMidName, lastName);

        if (name.IsFailure)
            return Result.Failure<Student>(name.Error);

        var email = Email.Create(studentEmail);

        if (email.IsFailure)
            return Result.Failure<Student>(email.Error);

        Name  = name.Value;
        Email = email.Value;

        return Result.Success(this);
    }

    public Result EnrollIn(Course course,
                           Grade?  grade)
    {
        if (Enrollments.Any(p => p.Course == course))
        {
            return Result.Failure($"Already enrolled in course '{course.Title}'");
        }

        var enrollment = new Enrollment(this, course, grade);

        Enrollments.Add(enrollment);

        return Result.Success();
    }

    public void Disenroll(Course course)
    {
        var enrollment = Enrollments.FirstOrDefault(p => p.Course == course);

        if (enrollment == null)
        {
            return;
        }

        Enrollments.Remove(enrollment);
    }
}