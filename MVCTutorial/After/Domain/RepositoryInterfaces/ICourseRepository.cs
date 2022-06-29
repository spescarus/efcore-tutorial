using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface ICourseRepository
{
    Task<IReadOnlyCollection<Course>> GetCoursesAsync();
    Task<Course?> GetCourseByIdAsync(long courseId);
    Task<bool> ExistAsync(long            id);
    Task<Course> AddAsync(Course          entity);
    Task<Course> UpdateAsync(Course       entity);
    void Delete(Course                    entity);
}
