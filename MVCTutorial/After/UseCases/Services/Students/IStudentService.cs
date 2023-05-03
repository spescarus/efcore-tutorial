using Application.Services.Students.Requests;
using Application.Services.Students.Responses;
using Domain.Base;
using Domain.Search;

namespace Application.Services.Students;

public interface IStudentService
{
    Task<IReadOnlyCollection<StudentResponse>> GetAllStudentsAsync();
    Task<PartialCollectionResponse<StudentResponse>> SearchStudentsAsync(SearchArgs searchArgs);
    Task<Result<StudentResponse>> GetStudentDetailsAsync(long                       studentId);
    Task<Result> CreateAsync(CreateStudentRequest                                   request);

    Task<Result> UpdateAsync(long                 studentId,
                             UpdateStudentRequest request);

    Task<Result> DeleteAsync(long studentId);

}
