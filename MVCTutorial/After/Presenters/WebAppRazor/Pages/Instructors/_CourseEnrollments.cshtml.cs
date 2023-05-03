using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.Students;
using Application.Services.Students.Responses;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Instructors;

public class CourseEnrollmentsModel : PageModel
{
    private readonly IEnrollmentService _enrollmentService;

    public IReadOnlyCollection<EnrollmentResponse> Enrollments { get; set; }
    public CourseEnrollmentsModel(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public async Task<PageResult> OnGet(long? courseId)
    {
        if (courseId == null)
        {
            return Page();
        }

        var enrollments = await _enrollmentService.GetEnrollmentsForCourseAsync(courseId.Value);

        Enrollments = enrollments;

        return Page();
    }
}