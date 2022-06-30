using Domain.Base;

namespace Domain.Entities;

public enum Grade
{
    A, B, C, D, F
}

public class Enrollment : Entity
{
    public long    CourseId  { get; private set; }
    public long    StudentId { get; private set; }
    public Grade?  Grade     { get; private set; }
    public Course  Course    { get; private set; }
    public Student Student   { get; private set; }

    private Enrollment()
    {
    }

    private Enrollment(long   courseId,
                       long   studentId,
                       Grade? grade)
    {
        CourseId  = courseId;
        StudentId = studentId;
        Grade     = grade;
    }

    public static Result<Enrollment> Create(long   courseId,
                                            long   studentId,
                                            string grade)
    {
        if (courseId <= 0)
            return Result.Failure<Enrollment>("Course number is required");

        if (studentId <= 0)
            return Result.Failure<Enrollment>("Student is required");

        Grade? gradeResult;

        if (string.IsNullOrWhiteSpace(grade))
            gradeResult = null;
        else
        {
            var isValidGrade = Enum.TryParse<Grade>(grade, out var enrollmentGrade);

            if (isValidGrade == false)
                return Result.Failure<Enrollment>("Grade must be one of the following letters: A, B, C, D, F ");

            gradeResult = enrollmentGrade;
        }

        var enrollment = new Enrollment(courseId, studentId, gradeResult);

        return Result.Success(enrollment);
    }

    public Result<Enrollment> Update(long   courseId,
                                     long   studentId,
                                     string grade)
    {
        if (courseId <= 0)
            return Result.Failure<Enrollment>("Course number is required");

        if (studentId <= 0)
            return Result.Failure<Enrollment>("Student is required");

        Grade? gradeResult;

        if (string.IsNullOrWhiteSpace(grade))
            gradeResult = null;
        else
        {
            var isValidGrade = Enum.TryParse<Grade>(grade, out var enrollmentGrade);

            if (isValidGrade == false)
                return Result.Failure<Enrollment>("Grade must be one of the following letters: A, B, C, D, F ");

            gradeResult = enrollmentGrade;
        }

        CourseId  = courseId;
        StudentId = studentId;
        Grade     = gradeResult;

        return Result.Success(this);
    }
}