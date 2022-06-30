using ApplicationServices.Services.Students.Requests;
using ApplicationServices.Services.Students.Responses;
using Domain.Base;

namespace ApplicationServices.Services.Students;

public interface IEnrollmentService
{
    Task<IReadOnlyCollection<EnrollmentResponse>> GetEnrollmentsAsync();
    Task<Result<EnrollmentResponse>> GetEnrollmentDetailsAsync(long enrollmentId);
    Task<Result> CreateAsync(CreateEnrollmentRequest                request);

    Task<Result> UpdateAsync(long                    enrollmentId,
                             UpdateEnrollmentRequest request);

    Task<Result> DeleteAsync(long enrollmentId);
}