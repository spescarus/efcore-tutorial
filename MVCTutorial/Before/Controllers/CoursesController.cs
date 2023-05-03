using System.Linq;
using System.Threading.Tasks;
using EfCoreMvcTutorial.Data;
using EfCoreMvcTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMvcTutorial.Controllers;

public class CoursesController : Controller
{
    private readonly SchoolContext _context;

    public CoursesController(SchoolContext context)
    {
        _context = context;
    }

    // GET: Courses
    public async Task<IActionResult> Index()
    {
        var courses = _context.Courses
                              .AsNoTracking();
        return View(await courses.ToListAsync());
    }

    // GET: Courses/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(m => m.Id == id);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
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
    public async Task<IActionResult> Create([Bind("Id,Credits,DepartmentId,Title")] Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    // GET: Courses/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(m => m.Id == id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
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

        var courseToUpdate = await _context.Courses
                                           .FirstOrDefaultAsync(c => c.Id == id);

        if (await TryUpdateModelAsync<Course>(courseToUpdate,
                                              "",
                                              c => c.Credits, c => c.Title))
        {
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. "                 +
                                             "Try again, and if the problem persists, " +
                                             "see your system administrator.");
            }
        }
        return View(courseToUpdate);
    }


    // GET: Courses/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(m => m.Id == id);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    // POST: Courses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var course = await _context.Courses.FindAsync(id);
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult UpdateCourseCredits()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCourseCredits(long? multiplier)
    {
        if (multiplier != null)
        {
            ViewData["RowsAffected"] =
                await _context.Database.ExecuteSqlRawAsync(
                    "UPDATE Course SET Credits = Credits * {0}",
                    parameters: multiplier);
        }
        return View();
    }

    private bool CourseExists(long id)
    {
        return _context.Courses.Any(e => e.Id == id);
    }
}