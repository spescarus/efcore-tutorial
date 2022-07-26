using Domain.Base;

namespace Domain.Entities;

public class CourseAssignment : Entity
{
    public long       InstructorId { get; set; }
    public long       CourseId     { get; set; }
    public Instructor Instructor   { get; set; }
    public Course     Course       { get; set; }
}