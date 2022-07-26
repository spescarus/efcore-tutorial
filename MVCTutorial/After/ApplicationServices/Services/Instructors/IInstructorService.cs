using ApplicationServices.Services.Instructors.Requests;
using ApplicationServices.Services.Instructors.Responses;
using Domain.Base;

namespace ApplicationServices.Services.Instructors;

public interface IInstructorService
{
    Task<IReadOnlyCollection<InstructorResponse>> GetAllInstructorsAsync();
    Task<Result<InstructorResponse>> GetInstructorDetailsAsync(long                            instructorId);
    Task<Result<IReadOnlyCollection<CourseAssignmentResponse>>> GetCourseAssignmentsAsync(long instructorId);
    Task<Result> CreateAsync(CreateInstructorRequest                                           request);

    Task<Result> UpdateAsync(long                    instructorId,
                             UpdateInstructorRequest request);

    Task<Result> DeleteAsync(long instructorId);
}
