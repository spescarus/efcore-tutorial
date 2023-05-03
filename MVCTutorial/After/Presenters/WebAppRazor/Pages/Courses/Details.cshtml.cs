using System.Threading.Tasks;
using Application.Services.Courses;
using Application.Services.Courses.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Courses;

public class DetailsModel : PageModel
{
    private readonly ICourseService _courseService;
    public           CourseResponse Course { get; set; }

    public DetailsModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> OnGet(long id)
    {
        var course = await _courseService.GetCourseDetailsAsync(id);

        if (course.IsSuccess)
        {
            Course = course.Value;

            return Page();
        }

        ModelState.AddModelError("", course.Error);

        return NotFound();
    }
}