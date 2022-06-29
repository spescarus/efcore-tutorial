namespace ApplicationServices.Services.Students.Responses;

public sealed class EnrollmentResponse
{
    public string Grade       { get; set; }
    public long   CourseId    { get; set; }
    public string CourseTitle { get; set; }
}
