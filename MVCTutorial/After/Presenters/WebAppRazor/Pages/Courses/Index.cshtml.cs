using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.Courses;
using Application.Services.Courses.Responses;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Courses;

public class IndexModel : PageModel
{
    private readonly ICourseService                      _courseService;
    public           IReadOnlyCollection<CourseResponse> Courses;

    public IndexModel(ICourseService courseService)
    {
        _courseService = courseService;
    }


    public async Task OnGet()
    {
        Courses = await _courseService.GetCoursesAsync();
    }
}