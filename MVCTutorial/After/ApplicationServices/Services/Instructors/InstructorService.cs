using ApplicationServices.Services.Instructors.Requests;
using ApplicationServices.Services.Instructors.Responses;
using Domain.Base;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.RepositoryInterfaces.Generics;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Services.Instructors;

public sealed class InstructorService : Service, IInstructorService
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly IUnitOfWork           _unitOfWork;

    public InstructorService(IInstructorRepository      instructorRepository,
                             IUnitOfWork                unitOfWork,
                             ILogger<InstructorService> logger)
        : base(logger)
    {
        _instructorRepository = instructorRepository;
        _unitOfWork           = unitOfWork;
    }

    public async Task<IReadOnlyCollection<InstructorResponse>> GetAllInstructorsAsync()
    {
        var students = await _instructorRepository.GetAllAsync();

        var studentsResponse = students.Select(p => new InstructorResponse
                                        {
                                            Id           = p.Id,
                                            LastName     = p.Name.LastName,
                                            FirstMidName = p.Name.FirstMidName,
                                            FullName     = p.FullName,
                                            Email        = p.Email,
                                            HireDate     = p.HireDate,
                                            OfficeAssignment = new OfficeAssignmentResponse
                                            {
                                                Location = p.OfficeAssignment?.Location
                                            },
                                            CourseAssignments = p.CourseAssignments.Select(c => new CourseAssignmentResponse
                                            {
                                                InstructorId   = c.InstructorId,
                                                CourseId       = c.CourseId,
                                                CourseName     = c.Course.Title
                                            }).ToList()
                                        })
                                       .ToList();

        return studentsResponse;
    }

    public async Task<Result<InstructorResponse>> GetInstructorDetailsAsync(long instructorId)
    {
        var instructor = await _instructorRepository.GetInstructorByIdAsync(instructorId);

        if (instructor == null)
            return Result.Failure<InstructorResponse>($"Cannot find instructor with Id {instructorId}");

        var response = new InstructorResponse
        {
            Id           = instructor.Id,
            LastName     = instructor.Name.LastName,
            FirstMidName = instructor.Name.FirstMidName,
            FullName     = instructor.FullName,
            Email        = instructor.Email,
            HireDate     = instructor.HireDate,
            OfficeAssignment = new OfficeAssignmentResponse
            {
                Location = instructor.OfficeAssignment?.Location
            },
            CourseAssignments = instructor.CourseAssignments.Select(p => new CourseAssignmentResponse
                                           {
                                               InstructorId   = p.Instructor.Id,
                                               CourseId       = p.CourseId,
                                               CourseName     = p.Course.Title
                                           })
                                          .ToList()
        };

        return Result.Success(response);
    }

    public async Task<Result<IReadOnlyCollection<CourseAssignmentResponse>>> GetCourseAssignmentsAsync(long instructorId)
    {
        var instructor = await _instructorRepository.GetInstructorByIdAsync(instructorId);

        if (instructor == null)
            return Result.Failure<IReadOnlyCollection<CourseAssignmentResponse>>($"Cannot find instructor with Id {instructorId}");

        IReadOnlyCollection<CourseAssignmentResponse> courseAssignments = instructor.CourseAssignments.Select(p => new CourseAssignmentResponse
                                                                                     {
                                                                                         InstructorId   = p.Instructor.Id,
                                                                                         CourseId       = p.CourseId,
                                                                                         CourseName     = p.Course.Title
                                                                                     })
                                                                                    .ToList();

        return Result.Success(courseAssignments);
    }

    public async Task<Result> CreateAsync(CreateInstructorRequest request)
    {
        var instructor = Instructor.Create(request.FirstMidName, request.LastName, request.Email, request.HireDate);

        if (instructor.IsFailure)
            return Result.Failure(instructor.Error);

        instructor.Value.AddOrUpdateOffice(request.OfficeLocation);

        var scope = await _unitOfWork.CreateScopeAsync();

        var assignedCourseResult = instructor.Value.AssignCourses(request.Courses);

        if (assignedCourseResult.IsFailure)
            return Result.Failure(assignedCourseResult.Error);

        await _instructorRepository.AddAsync(instructor.Value);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(long                    instructorId,
                                          UpdateInstructorRequest request)
    {
        var scope = await _unitOfWork.CreateScopeAsync();

        var instructor = await _instructorRepository.GetInstructorByIdAsync(instructorId);

        if (instructor == null)
            return Result.Failure<InstructorResponse>($"Cannot find instructor with Id {instructorId}");

        var instructorToUpdate = instructor.Update(request.FirstMidName, request.LastName, request.Email, request.HireDate);

        if (instructorToUpdate.IsFailure)
            return Result.Failure(instructorToUpdate.Error);

        instructorToUpdate.Value.AddOrUpdateOffice(request.OfficeLocation);

        var assignedCourseResult = instructorToUpdate.Value.AssignCourses(request.Courses);

        if (assignedCourseResult.IsFailure)
            return Result.Failure(assignedCourseResult.Error);

        await _instructorRepository.UpdateAsync(instructorToUpdate.Value);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(long instructorId)
    {
        var scope = await _unitOfWork.CreateScopeAsync();

        var instructor = await _instructorRepository.GetInstructorByIdAsync(instructorId);

        if (instructor == null)
            return Result.Failure<InstructorResponse>($"Cannot find instructor with Id {instructorId}");


        _instructorRepository.Delete(instructor);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }
}