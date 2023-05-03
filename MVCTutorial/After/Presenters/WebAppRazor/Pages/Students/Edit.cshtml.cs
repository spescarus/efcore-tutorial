using System.Threading.Tasks;
using Application.Services.Students;
using Application.Services.Students.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Students;

[ValidateAntiForgeryToken]
public class EditModel : PageModel
{
    private readonly IStudentService _studentService;

    public EditModel(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [BindProperty]
    public UpdateStudentRequest StudentToUpdate { get; set; }

    public async Task<IActionResult> OnGet(long? id)
    {
        if (id == null)
            return NotFound();

        var student = await _studentService.GetStudentDetailsAsync(id.Value);


        if (student.IsFailure)
        {
            ModelState.AddModelError("", student.Error);
            return Page();
        }

        StudentToUpdate = new UpdateStudentRequest
        {
            LastName       = student.Value.LastName,
            FirstMidName   = student.Value.FirstMidName,
            Email          = student.Value.Email,
            EnrollmentDate = student.Value.EnrollmentDate
        };

        return Page();
    }

    public async Task<IActionResult> OnPost(long? id)
    {
        if (id == null)
            return NotFound();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (await TryUpdateModelAsync(
                StudentToUpdate,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.Email, s => s.EnrollmentDate))
        {
            var result = await _studentService.UpdateAsync(id.Value, StudentToUpdate);

            if (result.IsSuccess)
                return RedirectToPage("/Students/Index");

            ModelState.AddModelError("", $"Unable to save changes. {result.Error}");
            return Page();
        }

        return Page();
    }
}