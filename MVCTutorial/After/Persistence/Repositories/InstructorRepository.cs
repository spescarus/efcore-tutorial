using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public sealed class InstructorRepository : Repository<Instructor>, IInstructorRepository
{
    public InstructorRepository(EfCoreContext context)
        : base(context)
    {
    }

    public async Task<Instructor?> GetInstructorByIdAsync(long instructorId)
    {
        var instructor = await GetByIdAsync(instructorId);

        return instructor;
    }

    protected override IQueryable<Instructor> DefaultIncludes(IQueryable<Instructor> queryable)
    {
        queryable = queryable.Include(p => p.Courses)
                             .Include(p => p.OfficeAssignment);

        return queryable;
    }
}