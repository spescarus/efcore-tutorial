namespace ApplicationServices.Services.Students.Responses;

public class StudentResponse
{
    public long                                    Id             { get; set; }
    public string                                  LastName       { get; set; }
    public string                                  FirstMidName   { get; set; }
    public string                                  FullName       { get; set; }
    public string                                  Email          { get; set; }
    public DateTime                                EnrollmentDate { get; set; }
    public IReadOnlyCollection<EnrollmentResponse> Enrollments    { get; set; }
}