using Domain.Base;
using Domain.Entities.ValueObjects;

namespace Domain.Entities;

public class Instructor : Entity
{
    public Name                Name             { get; private set; }
    public string              FullName         => Name.LastName + " " + Name.FirstMidName;
    public Email               Email            { get; private set; }
    public DateTime            HireDate         { get; private set; }
    public ICollection<Course> Courses          { get; private set; } = new List<Course>();
    public OfficeAssignment?   OfficeAssignment { get; private set; }

    private Instructor()
    {
    }

    private Instructor(Name     name,
                       Email    email,
                       DateTime hireDate)
    {
        Name     = name;
        Email    = email;
        HireDate = hireDate;
    }


    public static Result<Instructor> Create(string   firstMidName,
                                            string   lastName,
                                            string   instructorEmail,
                                            DateTime hireDate)
    {
        var name = Name.Create(firstMidName, lastName);

        if (name.IsFailure)
            return Result.Failure<Instructor>(name.Error);

        var email = Email.Create(instructorEmail);

        if (email.IsFailure)
            return Result.Failure<Instructor>(email.Error);

        var student = new Instructor(name.Value, email.Value, hireDate);

        return Result.Success(student);
    }

    public Result<Instructor> Update(string   firstMidName,
                                     string   lastName,
                                     string   instructorEmail,
                                     DateTime hireDate)
    {
        var name = Name.Create(firstMidName, lastName);

        if (name.IsFailure)
            return Result.Failure<Instructor>(name.Error);

        var email = Email.Create(instructorEmail);

        if (email.IsFailure)
            return Result.Failure<Instructor>(email.Error);

        Name     = name.Value;
        Email    = email.Value;
        HireDate = hireDate;

        return Result.Success(this);
    }

    public void AddOrUpdateOffice(string? officeLocation)
    {
        if (string.IsNullOrWhiteSpace(officeLocation))
        {
            return;
        }

        OfficeAssignment          ??= new OfficeAssignment();
        OfficeAssignment.Location =   officeLocation;
    }

    public Result AddOrUpdateCourses(ICollection<Course> courses)
    {
        if (!courses.Any())
        {
            return Result.Failure("At least one course need to be assigned to an instructor.");
        }

        foreach (var course in courses)
        {
            var existingCourse = Courses.SingleOrDefault(p => p.Id == course.Id);

            if (existingCourse == null)
            {
                Courses.Add(course);
            }
        }

        var coursesToRemove = Courses.Where(p => courses.All(pp => pp.Id != p.Id));

        foreach (var courseAssignment in coursesToRemove)
        {
            Courses.Remove(courseAssignment);
        }

        return Result.Success();
    }
}