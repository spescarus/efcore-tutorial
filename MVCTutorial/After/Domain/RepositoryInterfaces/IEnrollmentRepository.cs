using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IEnrollmentRepository 
{
    Task<IReadOnlyCollection<Enrollment>> GetEnrollmentsAsync();
    Task<IReadOnlyCollection<Enrollment>> GetEnrollmentsForCourseAsync(long courseId);
    Task<Enrollment?> GetEnrollmentAsync(long                               enrollmentId);
}