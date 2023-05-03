using System.Threading.Tasks;
using Application.Contexts.InstructorContexts;
using Application.Contexts.InstructorContexts.Responses;
using Application.Services.Instructors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Instructors;

[ValidateAntiForgeryToken]
public class DeleteModel : PageModel
{
    private readonly DeleteInstructorContext _deleteInstructorContext;
    private readonly IInstructorService      _instructorService;

    public InstructorDetailsResponse InstructorDetails { get; set; }
    public DeleteModel(DeleteInstructorContext deleteInstructorContext, IInstructorService instructorService)
    {
        _deleteInstructorContext = deleteInstructorContext;
        _instructorService            = instructorService;
    }

    public async Task<IActionResult> OnGet(long id, bool? saveChangesError = false)
    {
        var instructor = await _instructorService.GetInstructorDetailsAsync(id);

        if (saveChangesError.GetValueOrDefault()) ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists see your system administrator.";

        if (instructor.IsSuccess)
        {
            InstructorDetails = instructor.Value;
            return Page();
        }

        ModelState.AddModelError("", $"Delete failed. {instructor.Error}");
        RedirectToPage("./Index");

        return Page();
    }

    public async Task<IActionResult> OnPost(long id)
    {
        var result = await _deleteInstructorContext.Execute(id);

        return result.IsSuccess
                   ? RedirectToPage("./Index")
                   : RedirectToPage("./Delete", new { id, saveChangesError = true });
    }
}