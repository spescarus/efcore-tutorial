using Domain.Entities;
using Domain.GetawayInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Getaways.Instructors;

public class UpdateInstructorGateway : IUpdateInstructorGateway
{
    private readonly EfCoreContext _context;

    public UpdateInstructorGateway(EfCoreContext context)
    {
        _context = context;
    }

    public Task<Instructor?> GetInstructorForUpdate(long instructorId)
    {
        var instructorDbSet = _context.Set<Instructor>();

        var instructor = instructorDbSet.Where(p => p.Id.Equals(instructorId))
                                        .Include(p => p.Courses)
                                        .Include(p => p.OfficeAssignment)
                                        .SingleOrDefaultAsync();

        return instructor;
    }

    public async Task<ICollection<Course>> GetCoursesToAssign(ICollection<long> courseIds)
    {
        var courseDbSet = _context.Set<Course>();

        return await courseDbSet.Where(p => courseIds.Contains(p.Id)).ToListAsync();
    }

    public async Task Update(Instructor entity)
    {
        _context.Update(entity);

        await _context.SaveChangesAsync();
    }
}