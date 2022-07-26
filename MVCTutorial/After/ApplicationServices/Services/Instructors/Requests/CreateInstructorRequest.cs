namespace ApplicationServices.Services.Instructors.Requests;

public class CreateInstructorRequest
{
    public string            LastName       { get; set; }
    public string            FirstMidName   { get; set; }
    public string            Email          { get; set; }
    public DateTime          HireDate       { get; set; }
    public string            OfficeLocation { get; set; }
    public ICollection<long> Courses        { get; set; } = new List<long>();
}

public class UpdateInstructorRequest
{
    public string            LastName       { get; set; }
    public string            FirstMidName   { get; set; }
    public string            Email          { get; set; }
    public DateTime          HireDate       { get; set; }
    public string            OfficeLocation { get; set; }
    public ICollection<long> Courses        { get; set; } = new List<long>();

}