using Domain.Entities;

namespace Domain.GetawayInterfaces;

public interface IDeleteInstructorGateway
{
    Task<Instructor?> GetInstructorToDelete(long instructorId);

    Task Delete(Instructor entity);
}
