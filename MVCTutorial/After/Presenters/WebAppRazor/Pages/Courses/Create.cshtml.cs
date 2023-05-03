using System.Threading.Tasks;
using Application.Services.Courses;
using Application.Services.Courses.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Courses;

[ValidateAntiForgeryToken]
public class CreateModel : PageModel
{
    private readonly ICourseService _courseService;

    public CreateModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [BindProperty]
    public CreateCourseRequest CourseToCreate { get; set; }

    public async Task<IActionResult> OnPost()
    {
        var course = await _courseService.CreateAsync(CourseToCreate);

        if (course.IsSuccess)
        {
            return RedirectToPage("./Index");
        }

        ModelState.AddModelError("", course.Error);
        return Page();
    }
}