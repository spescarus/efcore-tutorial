namespace Application.Services.Students.Requests;

public class UpdateEnrollmentRequest
{
    public long CourseId { get; set; }
    public long StudentId { get; set; }
    public string Grade { get; set; }
}