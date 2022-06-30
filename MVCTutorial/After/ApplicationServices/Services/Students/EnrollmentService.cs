using ApplicationServices.Services.Students.Requests;
using ApplicationServices.Services.Students.Responses;
using Domain.Base;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.RepositoryInterfaces.Generics;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Services.Students;

public class EnrollmentService : Service, IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository,
                             IUnitOfWork unitOfWork,
                             ILogger<Service> logger)
        : base(logger)
    {
        _enrollmentRepository = enrollmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<EnrollmentResponse>> GetEnrollmentsAsync()
    {
        var enrollments = await _enrollmentRepository.GetEnrollmentsAsync();

        var response = enrollments.Select(p => new EnrollmentResponse
        {
            Id          = p.Id,
            Grade       = p.Grade.HasValue ? p.Grade.Value.ToString( ): "",
            CourseId    = p.CourseId,
            CourseTitle = p.Course.Title,
            StudentFullName = p.Student.FullName
        }).ToList();

        return response;
    }

    public async Task<Result<EnrollmentResponse>> GetEnrollmentDetailsAsync(long enrollmentId)
    {
        var enrollment = await _enrollmentRepository.GetEnrollmentAsync(enrollmentId);

        if (enrollment == null)
        {
            return Result.Failure<EnrollmentResponse>($"Cannot find enrollment with id {enrollmentId}");
        }

        var response = new EnrollmentResponse
        {
            Id              = enrollment.Id,
            Grade           = enrollment.Grade.HasValue ? enrollment.Grade.Value.ToString() : "",
            CourseId        = enrollment.CourseId,
            CourseTitle     = enrollment.Course.Title,
            StudentFullName = enrollment.Student.FullName
        };

        return Result.Success(response);
    }

    public async Task<Result> CreateAsync(CreateEnrollmentRequest request)
    {
        var enrollment = Enrollment.Create(request.CourseId, request.StudentId, request.Grade);

        if (enrollment.IsFailure)
        {
            return Result.Failure<EnrollmentResponse>(enrollment.Error);
        }

        var scope = await _unitOfWork.CreateScopeAsync();

        await _enrollmentRepository.AddAsync(enrollment.Value);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(long enrollmentId, UpdateEnrollmentRequest request)
    {
        var scope = await _unitOfWork.CreateScopeAsync();

        var enrollment = await _enrollmentRepository.GetEnrollmentAsync(enrollmentId);

        if (enrollment == null)
        {
            return Result.Failure<EnrollmentResponse>($"Cannot find enrollment with id {enrollmentId}");
        }

        var enrollmentUpdated = enrollment.Update(request.CourseId, request.StudentId, request.Grade);

        if (enrollmentUpdated.IsFailure)
        {
            return Result.Failure<EnrollmentResponse>(enrollmentUpdated.Error);
        }

        await _enrollmentRepository.UpdateAsync(enrollmentUpdated.Value);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(long enrollmentId)
    {
        var scope      = await _unitOfWork.CreateScopeAsync();

        var enrollment = await _enrollmentRepository.GetEnrollmentAsync(enrollmentId);

        if (enrollment == null)
        {
            return Result.Failure<EnrollmentResponse>($"Cannot find enrollment with id {enrollmentId}");
        }

        _enrollmentRepository.Delete(enrollment);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }
}