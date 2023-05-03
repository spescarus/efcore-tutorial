using Application.Contexts.InstructorContexts.Responses;
using Application.Models;
using Application.Services.Courses;
using Application.Services.Instructors;
using Domain.Base;

namespace Application.Contexts.InstructorContexts;

public class GetInstructorForUpdateContext
{
    private readonly IInstructorService _instructorService;
    private readonly ICourseService _courseService;

    public GetInstructorForUpdateContext(IInstructorService instructorService, ICourseService courseService)
    {
        _instructorService = instructorService;
        _courseService = courseService;
    }

    public async Task<Result<GetInstructorForUpdateResponse>> Execute(long id)
    {
        
        var instructor = await _instructorService.GetInstructorDetailsAsync(id);

        if (instructor.IsFailure)
            return Result.Failure<GetInstructorForUpdateResponse>(instructor.Error);

        var updateRequest = new InstructorDataToModify
        {
            FirstMidName   = instructor.Value.FirstMidName,
            LastName       = instructor.Value.LastName,
            Email          = instructor.Value.Email,
            OfficeLocation = instructor.Value.OfficeAssignment?.Location,
            HireDate       = instructor.Value.HireDate
        };

        var instructorCourses = new HashSet<long>(instructor.Value.CourseAssignments.Select(c => c.CourseId));

        var allClasses = await PopulateAssignedCourseData(instructorCourses);

        var result = new GetInstructorForUpdateResponse
        {
            InstructorDataToModify = updateRequest,
            AllClasses = allClasses,

        };

        return Result.Success(result);
    }

    private async Task<IReadOnlyCollection<CourseModel>> PopulateAssignedCourseData(ICollection<long> instructorCourses)
    {
        var allCourses = await _courseService.GetCoursesAsync();

        return allCourses.Select(course => new CourseModel
        {
            CourseId = course.CourseId,
            Title = course.Title,
            Assigned = instructorCourses.Contains(course.CourseId)
        })
                         .ToList();
    }
}
