using Domain.Base;

namespace Domain.Entities;

public class Course : Entity
{
    public string                  Title       { get; private set; }
    public int                     Credits     { get; private set; }
    public ICollection<Instructor> Instructors { get; private set; }


    private Course()
    {
    }

    private Course(long   id,
                   string title,
                   int    credits) : base(id)
    {
        Title    = title;
        Credits  = credits;
    }

    public static Result<Course> Create(long   courseId,
                                        string title,
                                        int    credits)
    {
        if (courseId <= 0)
            return Result.Failure<Course>("Course number need to be grater than 0");

        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<Course>("Course Title is required");

        if (title.Length is < 3 or > 50)
            return Result.Failure<Course>("Course Title need to be between 3 and 50 characters");

        if (credits is < 0 or > 5)
            return Result.Failure<Course>("Course credits are between 0 and 5");

        var course = new Course(courseId, title, credits);

        return Result.Success(course);
    }

    public Result<Course> Update(string title,
                                 int    credits)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<Course>("Course Title is required");

        if (title.Length is < 3 or > 50)
            return Result.Failure<Course>("Course Title need to be between 3 and 50 characters");

        if (credits is < 0 or > 5)
            return Result.Failure<Course>("Course credits are between 0 and 5");

        Title   = title;
        Credits = credits;

        return Result.Success(this);
    }
}