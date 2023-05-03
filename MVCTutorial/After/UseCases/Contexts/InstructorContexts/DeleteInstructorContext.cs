using Domain.Base;
using Domain.GetawayInterfaces;

namespace Application.Contexts.InstructorContexts;

public sealed class DeleteInstructorContext
{
    private readonly IDeleteInstructorGateway _gateway;

    public DeleteInstructorContext(IDeleteInstructorGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<Result> Execute(long instructorId)
    {
        var instructor = await _gateway.GetInstructorToDelete(instructorId);

        if (instructor == null)
            return Result.Failure($"Cannot find instructor with Id {instructorId}");


        await _gateway.Delete(instructor);

        return Result.Success();
    }
}