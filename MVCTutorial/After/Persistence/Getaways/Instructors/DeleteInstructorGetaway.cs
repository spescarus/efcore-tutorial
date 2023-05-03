using Domain.Entities;
using Domain.GetawayInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Getaways.Instructors;

public sealed class DeleteInstructorGetaway : IDeleteInstructorGateway
{
    private readonly EfCoreContext _context;

    public DeleteInstructorGetaway(EfCoreContext context)
    {
        _context = context;
    }

    public Task<Instructor?> GetInstructorToDelete(long instructorId)
    {
        var instructorDbSet = _context.Set<Instructor>();

        var instructor = instructorDbSet.Where(p => p.Id.Equals(instructorId))
                                        .SingleOrDefaultAsync();

        return instructor;
    }

    public async Task Delete(Instructor entity)
    {
        _context.Remove(entity);

        await _context.SaveChangesAsync();
    }
}