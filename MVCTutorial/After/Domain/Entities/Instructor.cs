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
    public OfficeAssignment?             OfficeAssignment  { get; private set; }

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


    public static Result<Instructor> Create(string   firstMidName,
                                     string   lastName,
                                     string    instructorEmail,
                                     DateTime hireDate)
    {
        var name = PersonName.Create(firstMidName, lastName);

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
        var name = PersonName.Create(firstMidName, lastName);

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

    public void AddOrUpdateOffice(string officeLocation)
    {
        if (string.IsNullOrWhiteSpace(officeLocation))
        {
            return;
        }

        OfficeAssignment ??= new OfficeAssignment
        {
            Location = officeLocation
        };
    }

    public Result AssignCourses(ICollection<long> courseIds)
    {
        if (!courseIds.Any())
        {
            return Result.Failure("At least one course need to be assigned to an instructor.");
        }

        CourseAssignments ??= new List<CourseAssignment>();

        foreach (var courseId in courseIds)
        {
            var courseAssignment = CourseAssignments.SingleOrDefault(p => p.CourseId == courseId);

            if (courseAssignment == null)
            {
                CourseAssignments.Add(new CourseAssignment
                {
                    CourseId     = courseId,
                    InstructorId = Id
                });
            }
        }

        var coursesToRemove = CourseAssignments.Where(p => courseIds.All(pp => pp != p.CourseId));
        
        foreach (var courseAssignment in coursesToRemove)
        {
            CourseAssignments.Remove(courseAssignment);
        }

        return Result.Success();
    }
}