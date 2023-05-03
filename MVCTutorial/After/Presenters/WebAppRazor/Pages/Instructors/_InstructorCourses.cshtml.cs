using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contexts.InstructorContexts.Responses;
using Application.Services.Instructors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Instructors;

public class InstructorCoursesModel : PageModel
{
    private readonly IInstructorService _instructorService;

    public InstructorCoursesModel(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }

    public long?                                         InstructorId { get; set; }
    public IReadOnlyCollection<CourseAssignmentResponse> Courses      { get; set; }

    public async Task<IActionResult> OnGet(long? instructorId)
    {
        if (instructorId == null)
        {
            return Page();
        }

        var courseAssignments = await _instructorService.GetCourseAssignmentsAsync(instructorId.Value);

        if (courseAssignments.IsFailure)
        {
            ModelState.AddModelError("", courseAssignments.Error);
            return Page();
        }

        InstructorId = instructorId;
        Courses      = courseAssignments.Value;

        return Page();
    }
}