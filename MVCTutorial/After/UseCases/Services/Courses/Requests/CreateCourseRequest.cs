namespace Application.Services.Courses.Requests;

public sealed class CreateCourseRequest
{
    public long CourseId { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
}
