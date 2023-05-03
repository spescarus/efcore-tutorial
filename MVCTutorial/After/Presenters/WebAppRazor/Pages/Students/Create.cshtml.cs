using System.Threading.Tasks;
using Application.Services.Students;
using Application.Services.Students.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Students;

[ValidateAntiForgeryToken]
public class CreateModel : PageModel
{
    private readonly IStudentService _studentService;

    public CreateModel(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [BindProperty]
    public CreateStudentRequest StudentToCreate { get; set; }

 
    public async Task<IActionResult> OnPost()
    {
        var student = await _studentService.CreateAsync(StudentToCreate);

        if (student.IsFailure) ModelState.AddModelError("", student.Error);

        return RedirectToPage("./Index");
    }
}