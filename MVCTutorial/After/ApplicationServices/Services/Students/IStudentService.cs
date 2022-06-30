using ApplicationServices.Services.Students.Requests;
using ApplicationServices.Services.Students.Responses;
using Domain.Base;
using Domain.Search;

namespace ApplicationServices.Services.Students;

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
