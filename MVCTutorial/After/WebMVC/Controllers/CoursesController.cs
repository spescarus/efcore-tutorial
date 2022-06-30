using System.Threading.Tasks;
using ApplicationServices.Services.Courses;
using ApplicationServices.Services.Courses.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers;

public class CoursesController : Controller
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    // GET: Courses
    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.GetCoursesAsync();

        return View(courses);
    }

    // GET: Courses/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _courseService.GetCourseDetailsAsync(id.Value);

        if (course.IsFailure) 
            ModelState.AddModelError("", course.Error);

        return View(course.Value);
    }

    // GET: Courses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Courses/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateCourseRequest request)
    {
        var course = await _courseService.CreateAsync(request);

        if (course.IsFailure)
        {
            ModelState.AddModelError("", course.Error);
            return View(request);
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Courses/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _courseService.GetCourseDetailsAsync(id.Value);

        var updateRequest = new UpdateCourseRequest();

        if (course.IsFailure)
        {
            ModelState.AddModelError("", course.Error);
            return View(updateRequest);
        }

        ViewData["CourseId"]  = course.Value.CourseId;
        updateRequest.Title   = course.Value.Title;
        updateRequest.Credits = course.Value.Credits;

        return View(updateRequest);
    }

    // POST: Courses/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost, ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }


        var updateCourseRequest = new UpdateCourseRequest();

        if (await TryUpdateModelAsync(updateCourseRequest,
                                                           "",
                                                           c => c.Credits, c => c.Title))
        {
            var result = await _courseService.UpdateAsync(id.Value, updateCourseRequest);

            if (result.IsSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", $"Unable to save changes. {result.Error}");
            return View(updateCourseRequest);
        }

        return View(updateCourseRequest);
    }

    // GET: Courses/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _courseService.GetCourseDetailsAsync(id.Value);
        
        if (course.IsFailure)
            ModelState.AddModelError("", course.Error);

        return View(course.Value);
    }

    // POST: Courses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var result = await _courseService.DeleteAsync(id);

        return result.IsSuccess
                   ? RedirectToAction(nameof(Index))
                   : RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
    }

    public IActionResult UpdateCourseCredits()
    {
        return View();
    }

    // [HttpPost]
    // public async Task<IActionResult> UpdateCourseCredits(long? multiplier)
    // {
    //     if (multiplier != null)
    //     {
    //         ViewData["RowsAffected"] =
    //             await _context.Database.ExecuteSqlRawAsync(
    //                 "UPDATE Course SET Credits = Credits * {0}",
    //                 parameters: multiplier);
    //     }
    //     return View();
    // }
}