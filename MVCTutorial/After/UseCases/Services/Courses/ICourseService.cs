using Application.Services.Courses.Requests;
using Application.Services.Courses.Responses;
using Domain.Base;

namespace Application.Services.Courses;

public interface ICourseService
{
    Task<IReadOnlyCollection<CourseResponse>> GetCoursesAsync();
    Task<Result<CourseResponse>> GetCourseDetailsAsync(long      courseId);
    Task<Result<CourseResponse>> CreateAsync(CreateCourseRequest request);
    Task<Result> UpdateAsync(long                courseId,
                             UpdateCourseRequest request);
    Task<Result> DeleteAsync(long                                courseId);
}
