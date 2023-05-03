using System.Threading.Tasks;
using Application.Contexts.InstructorContexts;
using Application.Contexts.InstructorContexts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Instructors;

public class DetailsModel : PageModel
{
    private readonly GetInstructorDetailsContext _instructorDetailsContext;

    public DetailsModel(GetInstructorDetailsContext instructorDetailsContext)
    {
        _instructorDetailsContext = instructorDetailsContext;
    }

    public InstructorDetailsResponse Instructor { get; set; }

    public async Task<IActionResult> OnGet(long id)
    {
        var instructor = await _instructorDetailsContext.Execute(id);

        if (instructor.IsSuccess)
        {
            Instructor = instructor.Value;

            return Page();
        }

        ModelState.AddModelError("", instructor.Error);

        return NotFound();
    }
}