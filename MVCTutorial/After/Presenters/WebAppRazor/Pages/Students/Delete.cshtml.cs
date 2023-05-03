using System.Threading.Tasks;
using Application.Services.Students;
using Application.Services.Students.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Students;

[ValidateAntiForgeryToken]
public class DeleteModel : PageModel
{
    private readonly IStudentService _studentService;

    public DeleteModel(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public StudentResponse Student { get; set; }

    public async Task<IActionResult> OnGet(long? id, bool? saveChangesError = false)
    {
        if (id == null)
            return NotFound();

        var student = await _studentService.GetStudentDetailsAsync(id.Value);

        if (student.IsFailure) ModelState.AddModelError("", $"Delete failed. {student.Error}");

        if (saveChangesError.GetValueOrDefault())
        {
            ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists see your system administrator.";
        }

        Student = student.Value;

        return Page();
    }

    public async Task<IActionResult> OnPost(long id)
    {
        var result = await _studentService.DeleteAsync(id);

        return result.IsSuccess
                   ? RedirectToPage("./Index")
                   : RedirectToPage("./Delete", new { id, saveChangesError = true });
    }
}