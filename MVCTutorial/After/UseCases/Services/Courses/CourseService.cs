using Application.Services.Courses.Requests;
using Application.Services.Courses.Responses;
using Domain.Base;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.RepositoryInterfaces.Generics;
using Microsoft.Extensions.Logging;

namespace Application.Services.Courses;

public sealed class CourseService : Service, ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork       _unitOfWork;

    public CourseService(ICourseRepository courseRepository, IUnitOfWork unitOfWork, ILogger<Service> logger)
        : base(logger)
    {
        _courseRepository = courseRepository;
        _unitOfWork       = unitOfWork;
    }

    public async Task<IReadOnlyCollection<CourseResponse>> GetCoursesAsync()
    {
        var courses = await _courseRepository.GetCoursesAsync();

        return courses.Select(p => new CourseResponse
                       {
                           CourseId       = p.Id,
                           Title          = p.Title,
                           Credits        = p.Credits,
                       })
                      .ToList();
    }

    public async Task<Result<CourseResponse>> GetCourseDetailsAsync(long courseId)
    {
        var course = await _courseRepository.GetCourseByIdAsync(courseId);

        if (course == null)
            return Result.Failure<CourseResponse>($"Cannot find course with number {courseId}");

        var response =  new CourseResponse
        {
            CourseId       = course.Id,
            Title          = course.Title,
            Credits        = course.Credits,
        };

        return Result.Success(response);
    }

    public async Task<Result<CourseResponse>> CreateAsync(CreateCourseRequest request)
    {

        var isAnExistingCourse = await _courseRepository.ExistAsync(request.CourseId);

        if (isAnExistingCourse)
            return Result.Failure<CourseResponse>($"The course {request.CourseId} already exists");

        var courseOrError = Course.Create(request.CourseId, request.Title, request.Credits);

        if (courseOrError.IsFailure)
            return Result.Failure<CourseResponse>(courseOrError.Error);

        var scope = await _unitOfWork.CreateScopeAsync();

        await _courseRepository.AddAsync(courseOrError.Value);
        await scope.SaveAsync();
        await scope.CommitAsync();

        var course = await _courseRepository.GetCourseByIdAsync(courseOrError.Value.Id);

        var response = new CourseResponse
        {
            CourseId       = course.Id,
            Title          = course.Title,
            Credits        = course.Credits,
        };

        return Result.Success(response);
    }

    public async Task<Result> UpdateAsync(long courseId, UpdateCourseRequest request)
    {
        var scope = await _unitOfWork.CreateScopeAsync();

        var course = await _courseRepository.GetCourseByIdAsync(courseId);

        if (course == null)
            return Result.Failure($"Cannot find course with number {courseId}");

        var courseUpdated = course.Update(request.Title, request.Credits);

        if (courseUpdated.IsFailure)
            return Result.Failure(courseUpdated.Error);

        await _courseRepository.UpdateAsync(courseUpdated.Value);
        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(long courseId)
    {
        var scope = await _unitOfWork.CreateScopeAsync();

        var course = await _courseRepository.GetCourseByIdAsync(courseId);

        if (course == null)
            return Result.Failure($"Cannot find course with number {courseId}");

        _courseRepository.Delete(course);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }
}