using Microsoft.EntityFrameworkCore;

namespace EfCoreMvcTutorial.Models;

public class CourseAssignment
{
    public long       CourseId     { get; set; }
    public long       InstructorId { get; set; }
    public Instructor Instructor   { get; set; }
    public Course     Course       { get; set; }
}