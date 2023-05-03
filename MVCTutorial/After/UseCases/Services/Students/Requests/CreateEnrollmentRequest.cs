namespace Application.Services.Students.Requests;

public class CreateEnrollmentRequest
{
    public long CourseId { get; set; }
    public long StudentId { get; set; }
    public string Grade { get; set; }
}