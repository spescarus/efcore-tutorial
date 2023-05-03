namespace Application.Contexts.InstructorContexts.Requests;

public class UpdateInstructorRequest
{
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public string Email { get; set; }
    public DateTime HireDate { get; set; }
    public string OfficeLocation { get; set; }
}
