using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contexts.InstructorContexts;
using Application.Models;
using Application.Services.Courses;
using Application.Services.Instructors.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Instructors;

[ValidateAntiForgeryToken]
public class CreateModel : PageModel
{
    private readonly CreateInstructorContext _createInstructorContext;
    private readonly ICourseService          _courseService;

    public CreateModel(CreateInstructorContext createInstructorContext,
                       ICourseService          courseService)
    {
        _createInstructorContext = createInstructorContext;
        _courseService           = courseService;
    }

    [BindProperty]
    public CreateInstructorRequest InstructorToCreate { get; set; }

    [BindProperty(SupportsGet = true)]
    public IReadOnlyCollection<CourseModel> Courses { get; set; }

    public async Task OnGet()
    {
        var allCourses = await _courseService.GetCoursesAsync();
        Courses = allCourses.Select(course => new CourseModel
                             {
                                 CourseId = course.CourseId,
                                 Title    = course.Title,
                                 Assigned = false
                             })
                            .ToList();
    }

    public async Task<IActionResult> OnPost(long[] selectedCourses)
    {
        InstructorToCreate.Courses = selectedCourses;

        if (ModelState.IsValid)
        {
            var instructorOrError = await _createInstructorContext.Execute(InstructorToCreate);

            if (instructorOrError.IsSuccess)
                return RedirectToPage("./Index");

            ModelState.AddModelError("", instructorOrError.Error);
        }

        var allCourses = await _courseService.GetCoursesAsync();

        Courses = allCourses.Select(course => new CourseModel
                             {
                                 CourseId = course.CourseId,
                                 Title    = course.Title,
                                 Assigned = selectedCourses.Contains(course.CourseId)
                             })
                            .ToList();

        return Page();
    }
}