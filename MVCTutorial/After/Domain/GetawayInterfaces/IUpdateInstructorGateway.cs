using Domain.Entities;

namespace Domain.GetawayInterfaces;

public interface IUpdateInstructorGateway
{
    Task<Instructor?> GetInstructorForUpdate(long                  instructorId);
    Task<ICollection<Course>> GetCoursesToAssign(ICollection<long> courseIds);

    Task Update(Instructor entity);
}