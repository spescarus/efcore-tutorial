using Domain.Entities;
using Domain.RepositoryInterfaces.Generics;

namespace Domain.RepositoryInterfaces;

public interface IEnrollmentRepository : IRepository<Enrollment>
{
    Task<IReadOnlyCollection<Enrollment>> GetEnrollmentsAsync();
    Task<Enrollment?> GetEnrollmentAsync(long enrollmentId);
}