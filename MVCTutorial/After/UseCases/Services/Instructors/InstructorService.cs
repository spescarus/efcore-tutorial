using Application.Contexts.InstructorContexts.Responses;
using Domain.Base;
using Domain.RepositoryInterfaces;
using Domain.RepositoryInterfaces.Generics;
using Microsoft.Extensions.Logging;

namespace Application.Services.Instructors;

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

    public async Task<IReadOnlyCollection<InstructorDetailsResponse>> GetAllInstructorsAsync()
    {
        var students = await _instructorRepository.GetAllAsync();

        var studentsResponse = students.Select(p => new InstructorDetailsResponse
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
                                            CourseAssignments = p.Courses.Select(c => new CourseAssignmentResponse
                                            {
                                                CourseId       = c.Id,
                                                CourseName     = c.Title
                                            }).ToList()
                                        })
                                       .ToList();

        return studentsResponse;
    }

    public async Task<Result<InstructorDetailsResponse>> GetInstructorDetailsAsync(long instructorId)
    {
        var instructor = await _instructorRepository.GetInstructorByIdAsync(instructorId);

        if (instructor == null)
            return Result.Failure<InstructorDetailsResponse>($"Cannot find instructor with Id {instructorId}");

        var response = new InstructorDetailsResponse
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
            CourseAssignments = instructor.Courses.Select(p => new CourseAssignmentResponse
                                           {
                                              // InstructorId   = p.Instructor.Id,
                                               CourseId       = p.Id,
                                               CourseName     = p.Title
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

        IReadOnlyCollection<CourseAssignmentResponse> courseAssignments = instructor.Courses.Select(p => new CourseAssignmentResponse
                                                                                     {
                                                                                         //InstructorId   = p.Instructor.Id,
                                                                                         CourseId       = p.Id,
                                                                                         CourseName     = p.Title
                                                                                     })
                                                                                    .ToList();

        return Result.Success(courseAssignments);
    }
}