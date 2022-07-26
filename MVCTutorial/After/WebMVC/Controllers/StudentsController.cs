using System.Threading.Tasks;
using ApplicationServices.Services.Students;
using ApplicationServices.Services.Students.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers;

public class StudentsController : Controller
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _studentService.GetAllStudentsAsync();

        return View(students);
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
            return NotFound();

        var student = await _studentService.GetStudentDetailsAsync(id.Value);

        if (student.IsFailure) ModelState.AddModelError("", student.Error);

        return View(student.Value);
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Students/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateStudentRequest request)
    {
        var student = await _studentService.CreateAsync(request);

        if (student.IsFailure) ModelState.AddModelError("", student.Error);

        return View(request);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
            return NotFound();

        var student = await _studentService.GetStudentDetailsAsync(id.Value);

        var updateRequest = new UpdateStudentRequest();

        if (student.IsFailure)
        {
            ModelState.AddModelError("", student.Error);
            return View(updateRequest);
        }


        updateRequest.LastName       = student.Value.LastName;
        updateRequest.FirstMidName   = student.Value.FirstMidName;
        updateRequest.Email          = student.Value.Email;
        updateRequest.EnrollmentDate = student.Value.EnrollmentDate;

        return View(updateRequest);
    }

    // POST: Students/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost, ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost([FromRoute] long? id)
    {
        if (id == null)
            return NotFound();

        var updateStudentRequest = new UpdateStudentRequest();

        if (await TryUpdateModelAsync(
                updateStudentRequest,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.Email, s => s.EnrollmentDate))
        {
            var result = await _studentService.UpdateAsync(id.Value, updateStudentRequest);

            if (result.IsSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", $"Unable to save changes. {result.Error}");
            return View(updateStudentRequest);
        }

        return View(updateStudentRequest);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(long? id,
                                            bool? saveChangesError = false)
    {
        if (id == null)
            return NotFound();

        var student = await _studentService.GetStudentDetailsAsync(id.Value);

        if (student.IsFailure) ModelState.AddModelError("", $"Delete failed. {student.Error}");

        if (saveChangesError.GetValueOrDefault())
        {
            ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists see your system administrator.";
        }

        return View(student.Value);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {

        var result = await _studentService.DeleteAsync(id);

        return result.IsSuccess
                   ? RedirectToAction(nameof(Index))
                   : RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
    }

}