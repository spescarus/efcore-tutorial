using Domain.Entities;
using Domain.GetawayInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Getaways.Instructors;

internal class CreateInstructorGateway : ICreateInstructorGateway
{
    private readonly EfCoreContext _context;

    public CreateInstructorGateway(EfCoreContext context)
    {
        _context = context;
    }

    public async Task Create(Instructor entity)
    {
        await _context.AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Course>> GetCoursesToAssign(ICollection<long> courseIds)
    {
        var courseDbSet = _context.Set<Course>();

        return await courseDbSet.Where(p => courseIds.Contains(p.Id)).ToListAsync();
    }
}