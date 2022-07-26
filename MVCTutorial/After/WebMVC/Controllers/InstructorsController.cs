using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationServices.Services.Courses;
using ApplicationServices.Services.Instructors;
using ApplicationServices.Services.Instructors.Requests;
using ApplicationServices.Services.Instructors.Responses;
using ApplicationServices.Services.Students;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models.SchoolViewModels;

namespace WebMvc.Controllers;

public class InstructorsController : Controller
{
    private readonly IInstructorService _instructorService;
    private readonly IEnrollmentService _enrollmentService;
    private readonly ICourseService     _courseService;

    public InstructorsController(IInstructorService instructorService, IEnrollmentService enrollmentService, ICourseService courseService)
    {
        _instructorService  = instructorService;
        _enrollmentService  = enrollmentService;
        _courseService = courseService;
    }

    // GET: Instructors
    public async Task<IActionResult> Index(long? id,
                                           long? courseId)
    {

        var instructorIndexData = new InstructorIndexData();

        var instructors = await _instructorService.GetAllInstructorsAsync();

        instructorIndexData.Instructors = instructors;

        if (id != null)
        {
            ViewData["InstructorId"] = id.Value;

            var courseAssignments = await _instructorService.GetCourseAssignmentsAsync(id.Value);

            if (courseAssignments.IsFailure)
            {
                ModelState.AddModelError("", courseAssignments.Error);
                return View(instructorIndexData);
            }


            instructorIndexData.Courses = courseAssignments.Value;
        }
        
        if (courseId != null)
        {
            ViewData["CourseId"] = courseId.Value;

            var enrollments = await _enrollmentService.GetEnrollmentsForCourseAsync(courseId.Value);

            instructorIndexData.Enrollments = enrollments;
        }

        return View(instructorIndexData);
    }


// GET: Instructors/Details/5
        public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructor = await _instructorService.GetInstructorDetailsAsync(id.Value);

        if (instructor.IsFailure)
        {
            ModelState.AddModelError("", instructor.Error);
            return BadRequest();
        }

        return View(instructor.Value);
    }

    // GET: Instructors/Create
    public async Task<IActionResult> Create()
    {
        var allCourses        = await _courseService.GetCoursesAsync();
        var viewModel = allCourses.Select(course => new AssignedCourseData
                                   {
                                       CourseId = course.CourseId, 
                                       Title = course.Title, 
                                       Assigned = false
                                   })
                                  .ToList();

        ViewData["Courses"] = viewModel;

        return View();
    }

    // POST: Instructors/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateInstructorRequest request,
                                            long[]                           selectedCourses)
    {
        if (selectedCourses != null)
        {
            request.Courses = selectedCourses;
        }

        if (ModelState.IsValid)
        {
            var instructorOrError = await _instructorService.CreateAsync(request);

            if (instructorOrError.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", instructorOrError.Error);
        }

        var allCourses = await _courseService.GetCoursesAsync();
        var viewModel = allCourses.Select(course => new AssignedCourseData
                                   {
                                       CourseId = course.CourseId, 
                                       Title = course.Title, 
                                       Assigned = false
                                   })
                                  .ToList();

        ViewData["Courses"] = viewModel; 
        return View(request);
    }

    // GET: Instructors/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var updateRequest = new UpdateInstructorRequest();
        var instructor    = await _instructorService.GetInstructorDetailsAsync(id.Value);
        
        if (instructor.IsFailure)
        {
            ModelState.AddModelError("", instructor.Error);
            return View(updateRequest);
        }

        updateRequest.FirstMidName   = instructor.Value.FirstMidName;
        updateRequest.LastName       = instructor.Value.LastName;
        updateRequest.Email          = instructor.Value.Email;
        updateRequest.OfficeLocation = instructor.Value.OfficeAssignment?.Location;
        updateRequest.Email          = instructor.Value.Email;

        await PopulateAssignedCourseData(instructor.Value);

        return View(updateRequest);
    }

    private async Task PopulateAssignedCourseData(InstructorResponse instructor)
    {
        var allCourses        = await _courseService.GetCoursesAsync();
        var instructorCourses = new HashSet<long>(instructor.CourseAssignments.Select(c => c.CourseId));
        var viewModel         = new List<AssignedCourseData>();
        foreach (var course in allCourses)
        {
            viewModel.Add(new AssignedCourseData
            {
                CourseId = course.CourseId,
                Title    = course.Title,
                Assigned = instructorCourses.Contains(course.CourseId)
            });
        }

        ViewData["Courses"] = viewModel;
    }

    // POST: Instructors/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long?     id,
                                          long[] selectedCourses)
    {
        if (id == null)
        {
            return NotFound();
        }

        var updateInstructorRequest = new UpdateInstructorRequest();
        var instructor              = await _instructorService.GetInstructorDetailsAsync(id.Value);

        if (instructor.IsFailure)
        {
            ModelState.AddModelError("", instructor.Error);
            return View(updateInstructorRequest);
        }

        var tryUpdateModelAsync = await TryUpdateModelAsync(
                                      updateInstructorRequest,
                                      "",
                                      i => i.FirstMidName, 
                                      i => i.LastName,
                                      i => i.Email,
                                      i => i.HireDate, 
                                      i => i.OfficeLocation);
        if (tryUpdateModelAsync)
        {

            updateInstructorRequest.Courses = selectedCourses;

            var result = await _instructorService.UpdateAsync(id.Value, updateInstructorRequest);

            if (result.IsSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", $"Unable to save changes. {result.Error}");
            return View(updateInstructorRequest);

        }

        await PopulateAssignedCourseData(instructor.Value);

        return View(updateInstructorRequest);
    }

    // GET: Instructors/Delete/5
    public async Task<IActionResult> Delete(long? id, bool? saveChangesError = false)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructor = await _instructorService.GetInstructorDetailsAsync(id.Value);

        if (instructor.IsFailure) ModelState.AddModelError("", $"Delete failed. {instructor.Error}");

        if (saveChangesError.GetValueOrDefault())
        {
            ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists see your system administrator.";
        }

        return View(instructor.Value);
    }

    // POST: Instructors/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var result = await _instructorService.DeleteAsync(id);

        return result.IsSuccess
                   ? RedirectToAction(nameof(Index))
                   : RedirectToAction(nameof(Delete), new { id, saveChangesError = true });

    }
}