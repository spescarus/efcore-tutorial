using Application.Contexts.InstructorContexts.Requests;
using Domain.Base;
using Domain.GetawayInterfaces;

namespace Application.Contexts.InstructorContexts;

public sealed class UpdateInstructorContext
{
    private readonly IUpdateInstructorGateway _gateway;

    public UpdateInstructorContext(IUpdateInstructorGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<Result> Execute(long                    instructorId,
                                      UpdateInstructorRequest request,
                                      long[]                  selectedCourses)
    {
       
        var instructor =  await _gateway.GetInstructorForUpdate(instructorId);

        if (instructor == null)
            return Result.Failure($"Cannot find instructor with Id {instructorId}");

        var instructorToUpdate = instructor.Update(request.FirstMidName, request.LastName, request.Email, request.HireDate);

        if (instructorToUpdate.IsFailure)
            return Result.Failure(instructorToUpdate.Error);

        instructorToUpdate.Value.AddOrUpdateOffice(request.OfficeLocation);

        var courses              = await _gateway.GetCoursesToAssign(selectedCourses);
        var assignedCourseResult = instructorToUpdate.Value.AddOrUpdateCourses(courses);

        if (assignedCourseResult.IsFailure)
            return Result.Failure(assignedCourseResult.Error);

        await _gateway.Update(instructorToUpdate.Value);

        return Result.Success();
    }
}