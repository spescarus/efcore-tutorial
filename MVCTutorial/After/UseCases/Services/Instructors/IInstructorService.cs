using Application.Contexts.InstructorContexts.Responses;
using Domain.Base;

namespace Application.Services.Instructors;

public interface IInstructorService
{
    Task<IReadOnlyCollection<InstructorDetailsResponse>> GetAllInstructorsAsync();
    Task<Result<InstructorDetailsResponse>> GetInstructorDetailsAsync(long                            instructorId);
    Task<Result<IReadOnlyCollection<CourseAssignmentResponse>>> GetCourseAssignmentsAsync(long instructorId);
}
