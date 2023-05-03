using Domain.Entities;

namespace Domain.GetawayInterfaces;

public interface ICreateInstructorGateway
{
    Task Create(Instructor entity);

    Task<List<Course>> GetCoursesToAssign(ICollection<long> courseIds);
}
