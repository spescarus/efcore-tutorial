using System.Threading.Tasks;
using Application.Services.Students;
using Application.Services.Students.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Students;

public class DetailsModel : PageModel
{
    private readonly IStudentService _studentService;

    public DetailsModel(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public StudentResponse Student { get; set; }

    public async Task<IActionResult> OnGet(long? id)
    {
        if(id == null)
            return NotFound();

        var student = await _studentService.GetStudentDetailsAsync(id.Value);

        if (student.IsFailure)
        {
            ModelState.AddModelError("", student.Error);

            return BadRequest(ModelState);
        }

        Student = student.Value;

        return Page();
    }
}