using System.Threading.Tasks;
using Application.Services.Courses;
using Application.Services.Courses.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Courses;

public class DeleteModel : PageModel
{
    private readonly ICourseService _courseService;
    public           CourseResponse Course { get; set; }

    public DeleteModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> OnGet(long id)
    {
        var course = await _courseService.GetCourseDetailsAsync(id);

        if (course.IsSuccess)
        {
            Course = course.Value;

            return RedirectToPage("./Index");
        }

        ModelState.AddModelError("", course.Error);

        return Page();
    }
}