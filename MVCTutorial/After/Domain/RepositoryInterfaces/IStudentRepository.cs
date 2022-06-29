using Domain.Entities;
using Domain.RepositoryInterfaces.Generics;
using Domain.Search;

namespace Domain.RepositoryInterfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<IPartialCollection<Student>> SearchStudentsAsync(SearchArgs searchArgs);
    Task<Student> GetStudentByIdAsync(long                           studentId);
}