using Application.Services.Instructors.Requests;
using Domain.Base;
using Domain.Entities;
using Domain.GetawayInterfaces;

namespace Application.Contexts.InstructorContexts;

public sealed class CreateInstructorContext
{
    private readonly ICreateInstructorGateway _gateway;

    public CreateInstructorContext(ICreateInstructorGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<Result> Execute(CreateInstructorRequest request)
    {
        var instructor = Instructor.Create(request.FirstMidName, request.LastName, request.Email, request.HireDate);

        if (instructor.IsFailure)
            return Result.Failure(instructor.Error);

        instructor.Value.AddOrUpdateOffice(request.OfficeLocation);

        var courses = await _gateway.GetCoursesToAssign(request.Courses);

        var assignedCourseResult = instructor.Value.AddOrUpdateCourses(courses);

        if (assignedCourseResult.IsFailure)
            return Result.Failure(assignedCourseResult.Error);

        await _gateway.Create(instructor.Value);

        return Result.Success();
    }
}