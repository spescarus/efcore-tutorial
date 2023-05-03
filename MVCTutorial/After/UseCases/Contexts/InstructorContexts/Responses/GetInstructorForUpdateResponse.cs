using Application.Models;

namespace Application.Contexts.InstructorContexts.Responses;

public class GetInstructorForUpdateResponse
{
    public InstructorDataToModify InstructorDataToModify { get; set; }

    public IReadOnlyCollection<CourseModel> AllClasses { get; set; } = new List<CourseModel>();
}

public class InstructorDataToModify
{
    public string   LastName       { get; set; }
    public string   FirstMidName   { get; set; }
    public string   Email          { get; set; }
    public DateTime HireDate       { get; set; }
    public string  OfficeLocation { get; set; }
}