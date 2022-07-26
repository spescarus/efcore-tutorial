namespace ApplicationServices.Services.Instructors.Responses;

public sealed class InstructorResponse
{
    public long     Id           { get; set; }
    public string   LastName     { get; set; }
    public string   FirstMidName { get; set; }
    public string   FullName     { get; set; }
    public string   Email        { get; set; }
    public DateTime HireDate     { get; set; }
    public OfficeAssignmentResponse OfficeAssignment { get; set; }
    public IReadOnlyCollection<CourseAssignmentResponse> CourseAssignments { get; set; }
}
