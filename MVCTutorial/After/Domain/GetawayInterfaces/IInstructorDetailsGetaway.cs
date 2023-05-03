using Domain.Entities;

namespace Domain.GetawayInterfaces;

public interface IInstructorDetailsGetaway
{
    Task<Instructor?> GetInstructorDetails(long instructorId);
}