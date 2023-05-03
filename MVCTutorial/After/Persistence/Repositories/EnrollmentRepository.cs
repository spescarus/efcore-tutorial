using System.Linq.Expressions;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

internal class EnrollmentRepository : IEnrollmentRepository
{
    private readonly EfCoreContext _context;

    public EnrollmentRepository(EfCoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Enrollment>> GetEnrollmentsAsync()
    {
        var enrollmentDbSet = _context.Set<Enrollment>();

        return await enrollmentDbSet.Include(p => p.Course)
                                    .Include(p => p.Student)
                                    .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Enrollment>> GetEnrollmentsForCourseAsync(long courseId)
    {
        Expression<Func<Enrollment, bool>> predicate = p => p.Course.Id == courseId;

        var enrollmentDbSet = _context.Set<Enrollment>();
        return await enrollmentDbSet.Where(predicate)
                                    .Include(p => p.Student)
                                    .Include(p => p.Course)
                                    .ToListAsync();
    }

    public async Task<Enrollment?> GetEnrollmentAsync(long enrollmentId)
    {
        var enrollmentDbSet = _context.Set<Enrollment>();

        Expression<Func<Enrollment, object>>[] includes =
        {
            p => p.Course,
            p => p.Student
        };


        return null;
    }
}