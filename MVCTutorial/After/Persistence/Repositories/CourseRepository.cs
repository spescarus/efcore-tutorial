using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public sealed class CourseRepository : ICourseRepository
{
    private readonly EfCoreContext _context;

    public CourseRepository(EfCoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Course>> GetCoursesAsync()
    {
        var courseDbSet = _context.Set<Course>();

        return await courseDbSet.ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(long courseId)
    {
        var courseDbSet = _context.Set<Course>();

        return await courseDbSet.Where(p => p.Id == courseId)
                                .FirstOrDefaultAsync();
    }

    public Task<bool> ExistAsync(long id)
    {
        var courseDbSet = _context.Set<Course>();

        return courseDbSet.AnyAsync(p => p.Id == id);
    }

    public async Task<Course> AddAsync(Course entity)
    {
        var courseDbSet = _context.Set<Course>();

        var result = await courseDbSet.AddAsync(entity);

        return result.Entity;
    }

    public Task<Course> UpdateAsync(Course entity)
    {
        var courseDbSet = _context.Set<Course>();

        var updatedEntity = courseDbSet.Update(entity)
                                       .Entity;
        return Task.FromResult(updatedEntity);
    }

    public void Delete(Course entity)
    {
        var courseDbSet = _context.Set<Course>();

        courseDbSet.Remove(entity);
    }
}