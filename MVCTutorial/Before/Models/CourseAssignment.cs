namespace EfCoreMvcTutorial.Models;

public class CourseAssignment
{
    public long        InstructorId { get; set; }
    public long        CourseId     { get; set; }
    public Instructor Instructor   { get; set; }
    public Course     Course       { get; set; }
}