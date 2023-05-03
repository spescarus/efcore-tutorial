using Application.Services.Students.Requests;
using Application.Services.Students.Responses;
using Domain.Base;

namespace Application.Services.Students;

public interface IEnrollmentService
{
    Task<IReadOnlyCollection<EnrollmentResponse>> GetEnrollmentsAsync();
    Task<IReadOnlyCollection<EnrollmentResponse>> GetEnrollmentsForCourseAsync(long courseId);
    Task<Result<EnrollmentResponse>> GetEnrollmentDetailsAsync(long                 enrollmentId);
    Task<Result> CreateAsync(CreateEnrollmentRequest                                request);

    Task<Result> UpdateAsync(long                    enrollmentId,
                             UpdateEnrollmentRequest request);

    Task<Result> DeleteAsync(long enrollmentId);
}