using System.Linq.Expressions;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Persistence.Context;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

internal class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(EfCoreContext context)
        : base(context)
    {
    }

    public async Task<IReadOnlyCollection<Enrollment>> GetEnrollmentsAsync()
    {
        Expression<Func<Enrollment, object>>[] includes =
        {
            p => p.Course,
            p => p.Student
        };

        return await GetAllAsync(includes);
    }

    public async Task<IReadOnlyCollection<Enrollment>> GetEnrollmentsForCourseAsync(long courseId)
    {
        Expression<Func<Enrollment, object>>[] includes =
        {
            p => p.Student
        };

        Expression<Func<Enrollment, bool>> predicate = p => p.CourseId == courseId;

        return await GetAllByAsync(predicate, includes);
    }

    public async Task<Enrollment?> GetEnrollmentAsync(long enrollmentId)
    {
        Expression<Func<Enrollment, object>>[] includes =
        {
            p => p.Course,
            p => p.Student
        };

        var enrollment = await GetByIdAsync(enrollmentId, includes);

        return enrollment;
    }
}