using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfCoreMvcTutorial.Data;
using EfCoreMvcTutorial.Models;

namespace EfCoreMvcTutorial.Controllers;

public class EnrollmentsController : Controller
{
    private readonly SchoolContext _context;

    public EnrollmentsController(SchoolContext context)
    {
        _context = context;
    }

    // GET: Enrollments
    public async Task<IActionResult> Index()
    {
        var schoolContext = _context.Enrollments.Include(e => e.Course)
                                    .Include(e => e.Student);
        return View(await schoolContext.ToListAsync());
    }

    // GET: Enrollments/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id                   == null ||
            _context.Enrollments == null)
        {
            return NotFound();
        }

        var enrollment = await _context.Enrollments
                                       .Include(e => e.Course)
                                       .Include(e => e.Student)
                                       .FirstOrDefaultAsync(m => m.Id == id);
        if (enrollment == null)
        {
            return NotFound();
        }

        return View(enrollment);
    }

    // GET: Enrollments/Create
    public IActionResult Create()
    {
        ViewData["Id"]  = new SelectList(_context.Courses,  "Id", "Title");
        ViewData["StudentId"] = new SelectList(_context.Students, "Id",       "FullName");
        return View();
    }

    // POST: Enrollments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Id,StudentId,Grade")] Enrollment enrollment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["Id"]  = new SelectList(_context.Courses,  "Id", "Title",    enrollment.CourseId);
        ViewData["StudentId"] = new SelectList(_context.Students, "Id",       "FullName", enrollment.StudentId);
        return View(enrollment);
    }

    // GET: Enrollments/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id                   == null ||
            _context.Enrollments == null)
        {
            return NotFound();
        }

        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null)
        {
            return NotFound();
        }

        ViewData["Id"]  = new SelectList(_context.Courses,  "Id", "Id", enrollment.CourseId);
        ViewData["StudentId"] = new SelectList(_context.Students, "Id",       "Email",    enrollment.StudentId);
        return View(enrollment);
    }

    // POST: Enrollments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long                                             id,
                                          [Bind("Id,Id,StudentId,Grade")] Enrollment enrollment)
    {
        if (id != enrollment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(enrollment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(enrollment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["Id"]  = new SelectList(_context.Courses,  "Id", "Id", enrollment.CourseId);
        ViewData["StudentId"] = new SelectList(_context.Students, "Id",       "Email",    enrollment.StudentId);
        return View(enrollment);
    }

    // GET: Enrollments/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id                   == null ||
            _context.Enrollments == null)
        {
            return NotFound();
        }

        var enrollment = await _context.Enrollments
                                       .Include(e => e.Course)
                                       .Include(e => e.Student)
                                       .FirstOrDefaultAsync(m => m.Id == id);
        if (enrollment == null)
        {
            return NotFound();
        }

        return View(enrollment);
    }

    // POST: Enrollments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        if (_context.Enrollments == null)
        {
            return Problem("Entity set 'SchoolContext.Enrollments'  is null.");
        }

        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment != null)
        {
            _context.Enrollments.Remove(enrollment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EnrollmentExists(long id)
    {
        return _context.Enrollments.Any(e => e.Id == id);
    }
}