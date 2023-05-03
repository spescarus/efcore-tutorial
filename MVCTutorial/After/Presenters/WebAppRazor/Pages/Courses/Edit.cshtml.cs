using System.Threading.Tasks;
using Application.Services.Courses;
using Application.Services.Courses.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Courses;

public class EditModel : PageModel
{
    private readonly ICourseService _courseService;

    [BindProperty]
    public UpdateCourseRequest CourseToUpdate { get; set; }

    public long CourseNumber { get; set; }

    public EditModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> OnGet(long id)
    {
        var course = await _courseService.GetCourseDetailsAsync(id);


        if (!course.IsSuccess)
        {
            CourseToUpdate = new UpdateCourseRequest
            {
                Title   = course.Value.Title,
                Credits = course.Value.Credits
            };

            CourseNumber = course.Value.CourseId;

            return Page();
        }

        ModelState.AddModelError("", course.Error);
        return RedirectToPage("./Index");
    }

    public async Task<IActionResult> OnPost(long id)
    {
        if (await TryUpdateModelAsync(CourseToUpdate,
                                      "",
                                      c => c.Credits, c => c.Title))
        {
            var result = await _courseService.UpdateAsync(id, CourseToUpdate);

            if (result.IsSuccess)
                return RedirectToPage("./Index");

            ModelState.AddModelError("", $"Unable to save changes. {result.Error}");
            return Page();
        }

        return Page();
    }
}