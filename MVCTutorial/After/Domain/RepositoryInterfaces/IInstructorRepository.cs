using Domain.Entities;
using Domain.RepositoryInterfaces.Generics;

namespace Domain.RepositoryInterfaces;

public interface IInstructorRepository : IRepository<Instructor>
{
    Task<Instructor?> GetInstructorByIdAsync(long instructorId);
}