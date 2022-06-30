using System.Threading.Tasks;
using ApplicationServices.Services.Students;
using ApplicationServices.Services.Students.Requests;
using ApplicationServices.Services.Students.Responses;
using Domain.Search;
using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers;

public class StudentsWithPaginationController : Controller
{
    private readonly IStudentService _studentService;

    public StudentsWithPaginationController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: Students
    public async Task<IActionResult> Index(string sortOrder,
                                                            string currentFilter,
                                                            string searchString,
                                                            int?   pageNumber)
    {
        ViewData["CurrentSort"] = sortOrder;
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder)
                                       ? "name_desc"
                                       : "";
        ViewData["DateSortParm"] = sortOrder == "Date"
                                       ? "date_desc"
                                       : "Date";

        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        ViewData["CurrentFilter"] = searchString;

        const int pageSize  = 3;
        var       pageIndex = pageNumber ?? 1;

        var searchArgs = new SearchArgs
        {
            SearchText = searchString,
            Offset     = (pageIndex - 1) * pageSize,
            Limit      = pageSize,
            SortOption = new SortOptionArgs()
        };

        switch (sortOrder)
        {
            case "name_desc":
                searchArgs.SortOption.SortOrder    = SortOrder.Descending;
                searchArgs.SortOption.PropertyName = "LastName";
                break;
            case "Date":
                searchArgs.SortOption.SortOrder    = SortOrder.Ascending;
                searchArgs.SortOption.PropertyName = "EnrollmentDate";
                break;
            case "date_desc":
                searchArgs.SortOption.SortOrder    = SortOrder.Descending;
                searchArgs.SortOption.PropertyName = "EnrollmentDate";
                break;
            default:
                searchArgs.SortOption.SortOrder    = SortOrder.Ascending;
                searchArgs.SortOption.PropertyName = "LastName";
                break;
        }

        var students = await _studentService.SearchStudentsAsync(searchArgs);

        return View(PaginatedList<StudentResponse>.CreateAsync(students.Values, students.RecordsTotal, pageIndex, pageSize));
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

        var studentToUpdate = new UpdateStudentRequest();

        if (await TryUpdateModelAsync(
                studentToUpdate,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.Email, s => s.EnrollmentDate))
        {
            var result = await _studentService.UpdateAsync(id.Value, studentToUpdate);

            if (result.IsSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", $"Unable to save changes. {result.Error}");
            return View(studentToUpdate);
        }

        return View(studentToUpdate);
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