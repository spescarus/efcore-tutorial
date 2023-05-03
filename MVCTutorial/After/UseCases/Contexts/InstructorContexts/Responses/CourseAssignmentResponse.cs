namespace Application.Contexts.InstructorContexts.Responses;

public sealed class CourseAssignmentResponse
{
    public long InstructorId { get; set; }
    public long CourseId { get; set; }
    public string CourseName { get; set; }
}
