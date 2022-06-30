namespace ApplicationServices.Services.Students.Requests;

public sealed class CreateStudentRequest
{
    public string   LastName       { get; set; }
    public string   FirstMidName   { get; set; }
    public string   Email          { get; set; }
    public DateTime EnrollmentDate { get; set; }
}