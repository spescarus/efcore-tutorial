using Application.Contexts.InstructorContexts.Responses;
using Domain.Base;
using Domain.GetawayInterfaces;

namespace Application.Contexts.InstructorContexts;

public sealed class GetInstructorDetailsContext
{
    private readonly IInstructorDetailsGetaway _getaway;

    public GetInstructorDetailsContext(IInstructorDetailsGetaway getaway)
    {
        _getaway = getaway;
    }

    public async Task<Result<InstructorDetailsResponse>> Execute(long instructorId)
    {
        var instructor = await _getaway.GetInstructorDetails(instructorId);

        if (instructor == null)
            return Result.Failure<InstructorDetailsResponse>($"Cannot find instructor with Id {instructorId}");

        var response = new InstructorDetailsResponse
        {
            Id           = instructor.Id,
            LastName     = instructor.Name.LastName,
            FirstMidName = instructor.Name.FirstMidName,
            FullName     = instructor.FullName,
            Email        = instructor.Email,
            HireDate     = instructor.HireDate,
            OfficeAssignment = new OfficeAssignmentResponse
            {
                Location = instructor.OfficeAssignment?.Location
            },
            CourseAssignments = instructor.Courses.Select(p => new CourseAssignmentResponse
                                           {
                                               //InstructorId = p.Id,
                                               CourseId     = p.Id,
                                               CourseName   = p.Title
                                           })
                                          .ToList()
        };

        return Result.Success(response);
    }
}