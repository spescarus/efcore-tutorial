using Domain.Base;

namespace Domain.Entities;

public enum Grade
{
    A, B, C, D, F
}

public class Enrollment : Entity
{
    public Grade? Grade { get; }
    public Student Student   { get; }
    public Course  Course    { get; }
    


    protected Enrollment()
    {
    }

    public Enrollment(Student   student,
                       Course   course,
                       Grade? grade) : this()
    {
        Student = student;
        Course       = course;
        Grade        = grade;
    }
    //
    // public static Result<Enrollment> Create(Student? student,
    //                                         Course?  course,
    //                                         string   grade)
    // {
    //     if (course == null)
    //         return Result.Failure<Enrollment>("Course number is required");
    //
    //     if (student == null)
    //         return Result.Failure<Enrollment>("Student is required");
    //
    //     Grade? gradeResult;
    //
    //     if (string.IsNullOrWhiteSpace(grade))
    //         gradeResult = null;
    //     else
    //     {
    //         var isValidGrade = Enum.TryParse<Grade>(grade, out var enrollmentGrade);
    //
    //         if (isValidGrade == false)
    //             return Result.Failure<Enrollment>("Grade must be one of the following letters: A, B, C, D, F ");
    //
    //         gradeResult = enrollmentGrade;
    //     }
    //
    //     var enrollment = new Enrollment(student, course, gradeResult);
    //
    //     return Result.Success(enrollment);
    // }
}