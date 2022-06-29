namespace ApplicationServices.Services.Courses.Requests;

public sealed class UpdateCourseRequest
{
    public string Title { get; set; }
    public int Credits { get; set; }
}
