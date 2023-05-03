using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contexts.InstructorContexts.Responses;
using Application.Services.Instructors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Instructors;

public class IndexModel : PageModel
{
    private readonly IInstructorService _instructorService;

    public IReadOnlyCollection<InstructorDetailsResponse> Instructors { get; set; }

    public IndexModel(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }

    public async Task<IActionResult> OnGet()
    {
        var instructors = await _instructorService.GetAllInstructorsAsync();

        Instructors = instructors;

        return Page();
    }
}