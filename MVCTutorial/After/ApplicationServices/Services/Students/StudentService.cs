using ApplicationServices.Services.Students.Requests;
using ApplicationServices.Services.Students.Responses;
using Domain.Base;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.RepositoryInterfaces.Generics;
using Domain.Search;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Services.Students;

public sealed class StudentService : Service, IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork        _unitOfWork;

    public StudentService(IStudentRepository      studentRepository,
                          IUnitOfWork             unitOfWork,
                          ILogger<StudentService> logger)
        : base(logger)
    {
        _studentRepository = studentRepository;
        _unitOfWork        = unitOfWork;
    }

    public async Task<IReadOnlyCollection<StudentResponse>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync();

        var studentsResponse = students.Select(p => new StudentResponse
                                        {
                                            Id             = p.Id,
                                            LastName       = p.Name.LastName,
                                            FirstMidName   = p.Name.FirstMidName,
                                            FullName       = p.FullName,
                                            Email          = p.Email,
                                            EnrollmentDate = p.EnrollmentDate
                                        })
                                       .ToList();

        return studentsResponse;
    }

    public async Task<PartialCollectionResponse<StudentResponse>> SearchStudentsAsync(SearchArgs searchArgs)
    {
        var students = await _studentRepository.SearchStudentsAsync(searchArgs);

        var studentsResponse = students.Values.Select(p => new StudentResponse
                                        {
                                            Id             = p.Id,
                                            LastName       = p.Name.LastName,
                                            FirstMidName   = p.Name.FirstMidName,
                                            FullName       = p.FullName,
                                            Email          = p.Email,
                                            EnrollmentDate = p.EnrollmentDate
                                        })
                                       .ToList();

        return new PartialCollectionResponse<StudentResponse>
        {
            Values          = studentsResponse,
            Offset          = students.Offset,
            Limit           = students.Limit,
            RecordsFiltered = students.Count,
            RecordsTotal    = await _studentRepository.CountAsync()
        };
    }

    public async Task<Result<StudentResponse>> GetStudentDetailsAsync(long studentId)
    {
        var student = await _studentRepository.GetStudentByIdAsync(studentId);

        if (student == null)
            return Result.Failure<StudentResponse>($"Cannot find student with Id {studentId}");

        var response = new StudentResponse
        {
            Id             = student.Id,
            LastName       = student.Name.LastName,
            FirstMidName   = student.Name.FirstMidName,
            FullName       = student.FullName,
            Email          = student.Email,
            EnrollmentDate = student.EnrollmentDate,
            Enrollments = student.Enrollments.Select(p => new EnrollmentResponse
                                  {
                                      Grade = p.Grade.HasValue
                                                  ? p.Grade.Value.ToString()
                                                  : "No Grade",
                                      CourseId    = p.CourseId,
                                      CourseTitle = p.Course.Title
                                  })
                                 .ToList()
        };

        return Result.Success(response);
    }

    public async Task<Result> CreateAsync(CreateStudentRequest request)
    {
        var student = Student.Create(request.FirstMidName, request.LastName, request.Email, request.EnrollmentDate);

        if (student.IsFailure)
            return Result.Failure(student.Error);

        var scope = await _unitOfWork.CreateScopeAsync();

        await _studentRepository.AddAsync(student.Value);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(long                 studentId,
                                          UpdateStudentRequest request)
    {
        var scope = await _unitOfWork.CreateScopeAsync();

        var student = await _studentRepository.GetStudentByIdAsync(studentId);

        if (student == null)
            return Result.Failure<StudentResponse>($"Cannot find student with Id {studentId}");

        var studentToUpdate = student.Update(request.FirstMidName, request.LastName, request.Email, request.EnrollmentDate);

        if (studentToUpdate.IsFailure)
            return Result.Failure<StudentResponse>(studentToUpdate.Error);

        await _studentRepository.UpdateAsync(studentToUpdate.Value);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(long studentId)
    {
        var student = await _studentRepository.GetStudentByIdAsync(studentId);

        if (student == null)
            return Result.Failure($"Cannot find student with Id {studentId}");

        var scope = await _unitOfWork.CreateScopeAsync();

        _studentRepository.Delete(student);

        await scope.SaveAsync();
        await scope.CommitAsync();

        return Result.Success();
    }
}