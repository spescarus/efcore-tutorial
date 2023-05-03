using Domain.Entities;
using Domain.GetawayInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Getaways.Instructors;

public sealed class InstructorDetailsGetaway : IInstructorDetailsGetaway
{
    private readonly EfCoreContext _context;

    public InstructorDetailsGetaway(EfCoreContext context)
    {
        _context = context;
    }

    public async Task<Instructor?> GetInstructorDetails(long instructorId)
    {
        var instructorDbSet = _context.Set<Instructor>();

        var instructor = await instructorDbSet.Where(p => p.Id == instructorId)
                                              .Include(p => p.OfficeAssignment)
                                              .Include(p => p.Courses)
                                              .AsNoTracking()
                                              .SingleOrDefaultAsync();

        return instructor;
    }
}