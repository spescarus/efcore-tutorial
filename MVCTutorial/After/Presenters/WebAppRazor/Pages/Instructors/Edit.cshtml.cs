using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contexts.InstructorContexts;
using Application.Contexts.InstructorContexts.Requests;
using Application.Contexts.InstructorContexts.Responses;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Instructors;

[ValidateAntiForgeryToken]
public class EditModel : PageModel
{
    private readonly GetInstructorForUpdateContext _getInstructorForUpdateContext;
    private readonly UpdateInstructorContext       _updateInstructorContext;

    public EditModel(GetInstructorForUpdateContext getInstructorForUpdateContext,
                     UpdateInstructorContext       updateInstructorContext)
    {
        _getInstructorForUpdateContext = getInstructorForUpdateContext;
        _updateInstructorContext       = updateInstructorContext;
    }

    [BindProperty]
    public GetInstructorForUpdateResponse InstructorForUpdate { get; set; }

    public async Task<IActionResult> OnGet(long id)
    {
        var instructorForUpdate = await _getInstructorForUpdateContext.Execute(id);

        if (instructorForUpdate.IsSuccess)
        {
            InstructorForUpdate = instructorForUpdate.Value;
            return Page();
        }

        ModelState.AddModelError("", instructorForUpdate.Error);

        return BadRequest(ModelState);
    }

    public async Task<IActionResult> OnPost(long   id,
                                            long[] selectedCourses)
    {
        var updateInstructorRequest = new UpdateInstructorRequest();

        var tryUpdateModel = await TryUpdateModelAsync(
                                 updateInstructorRequest,
                                 "InstructorForUpdate.InstructorDataToModify",
                                 i => i.FirstMidName,
                                 i => i.LastName,
                                 i => i.Email,
                                 i => i.HireDate,
                                 i => i.OfficeLocation);
        if (tryUpdateModel)
        {
            var result = await _updateInstructorContext.Execute(id, updateInstructorRequest, selectedCourses);

            if (result.IsSuccess)
                return RedirectToPage("./Index");

            ModelState.AddModelError("", $"Unable to save changes: {result.Error}");
        }

        var instructorForUpdate = await _getInstructorForUpdateContext.Execute(id);

        if (instructorForUpdate.IsFailure)
        {
            ModelState.AddModelError("", instructorForUpdate.Error);

            return Page();
        }

        ApplyUserChangesOnCoursesList(selectedCourses, instructorForUpdate.Value.AllClasses);

        return Page();
    }

    private void ApplyUserChangesOnCoursesList(long[]                selectedCourses,
                                               IReadOnlyCollection<CourseModel> courses)
    {
        var allClasses = new List<CourseModel>();

        foreach (var course in courses)
        {
            course.Assigned = selectedCourses.ToList()
                                                  .Contains(course.CourseId);
            allClasses.Add(course);
        }

        InstructorForUpdate.AllClasses = allClasses;
    }
}