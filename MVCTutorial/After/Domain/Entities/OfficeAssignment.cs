using Domain.Base;

namespace Domain.Entities;

public class OfficeAssignment : Entity
{
    public long InstructorId { get; set; }
    public string     Location     { get; set; }
}