using Domain.Base;

namespace Domain.Entities;

public enum Grade
{
    A, B, C, D, F
}

public class Enrollment : Entity
{
    public long   CourseId  { get; set; }
    public long   StudentId { get; set; }
    public Grade? Grade     { get; set; }

    public Course  Course  { get; set; }
    public Student Student { get; set; }
}