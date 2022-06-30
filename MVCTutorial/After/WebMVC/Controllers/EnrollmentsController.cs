using System.Threading.Tasks;
using ApplicationServices.Services.Courses;
using ApplicationServices.Services.Students;
using ApplicationServices.Services.Students.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebMvc.Controllers;

public class EnrollmentsController : Controller
{
    private readonly IEnrollmentService _enrollmentService;
    private readonly ICourseService     _courseService;
    private readonly IStudentService    _studentService;

    public EnrollmentsController(IEnrollmentService enrollmentService, ICourseService courseService, IStudentService studentService)
    {
        _enrollmentService = enrollmentService;
        _courseService     = courseService;
        _studentService    = studentService;
    }

    // GET: Enrollments
    public async Task<IActionResult> Index()
    {
        var enrollments = await _enrollmentService.GetEnrollmentsAsync();
        return View(enrollments);
    }

    // GET: Enrollments/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrollment = await _enrollmentService.GetEnrollmentDetailsAsync(id.Value);
        if (enrollment.IsFailure)
        {
            ModelState.AddModelError("", enrollment.Error);
            ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists see your system administrator.";
            return View();
        }

        return View(enrollment.Value);
    }

    // GET: Enrollments/Create
    public async Task<IActionResult> Create()
    {
        var courses  = await _courseService.GetCoursesAsync();
        var students = await _studentService.GetAllStudentsAsync();

        ViewData["CourseId"]  = new SelectList(courses,  "CourseId", "Title");
        ViewData["StudentId"] = new SelectList(students, "Id",       "FullName");
        return View();
    }

    // POST: Enrollments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateEnrollmentRequest request)
    {

        var enrollment = await _enrollmentService.CreateAsync(request);

        if (enrollment.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", enrollment.Error);

        var courses  = await _courseService.GetCoursesAsync();
        var students = await _studentService.GetAllStudentsAsync();

        ViewData["CourseId"]  = new SelectList(courses,  "CourseId", "Title",    request.CourseId);
        ViewData["StudentId"] = new SelectList(students, "Id",       "FullName", request.StudentId);
            
        return View(request);
    }

    // GET: Enrollments/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrollment = await _enrollmentService.GetEnrollmentDetailsAsync(id.Value);

        var enrollmentUpdate = new UpdateEnrollmentRequest(); 
            
        if (enrollment.IsFailure)
        {
            ModelState.AddModelError("", enrollment.Error);
            return View(enrollmentUpdate);
        }

        var courses  = await _courseService.GetCoursesAsync();
        var students = await _studentService.GetAllStudentsAsync();

        ViewData["CourseId"]  = new SelectList(courses,  "CourseId", "Title", enrollmentUpdate.CourseId);
        ViewData["StudentId"] = new SelectList(students, "Id",       "FullName",    enrollmentUpdate.StudentId);

        enrollmentUpdate.Grade = enrollment.Value.Grade;

        return View(enrollmentUpdate);
    }

    // POST: Enrollments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long? id, [FromForm] UpdateEnrollmentRequest request)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var enrollment = await _enrollmentService.UpdateAsync(id.Value, request);

        if (enrollment.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        var courses  = await _courseService.GetCoursesAsync();
        var students = await _studentService.GetAllStudentsAsync();

        ViewData["CourseId"]  = new SelectList(courses,  "CourseId", "Title", request.CourseId);
        ViewData["StudentId"] = new SelectList(students, "Id",       "FullName",    request.StudentId);
            
        return View(request);
    }

    // GET: Enrollments/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrollment = await _enrollmentService.GetEnrollmentDetailsAsync(id.Value);

        if (enrollment.IsFailure)
        {
            return Problem(enrollment.Error);
        }

        return View(enrollment.Value);
    }

    // POST: Enrollments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var result = await _enrollmentService.DeleteAsync(id);

        if (result.IsFailure)
        {
            return Problem(result.Error);
        }

        return RedirectToAction(nameof(Index));
    }
}