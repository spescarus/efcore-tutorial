﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCoreMvcTutorial.Data;
using EfCoreMvcTutorial.Models;
using EfCoreMvcTutorial.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMvcTutorial.Controllers;

public class InstructorsController : Controller
{
    private readonly SchoolContext _context;

    public InstructorsController(SchoolContext context)
    {
        _context = context;
    }

    // GET: Instructors
    public async Task<IActionResult> Index(long? id,
                                           long? courseId)
    {
        var viewModel = new InstructorIndexData();
        viewModel.Instructors = await _context.Instructors
                                              .Include(i => i.OfficeAssignment)
                                              .Include(i => i.CourseAssignments)
                                              .ThenInclude(i => i.Course)
                                              .OrderBy(i => i.LastName)
                                              .ToListAsync();

        if (id != null)
        {
            ViewData["InstructorId"] = id.Value;
            Instructor instructor = viewModel.Instructors
                                             .Single(i => i.Id == id.Value);
            viewModel.Courses = instructor.CourseAssignments.Select(s => s.Course);
        }

        if (courseId != null)
        {
            ViewData["Id"] = courseId.Value;
            var selectedCourse = viewModel.Courses
                                          .Single(x => x.Id == courseId);
            await _context.Entry(selectedCourse)
                          .Collection(x => x.Enrollments)
                          .LoadAsync();
            foreach (Enrollment enrollment in selectedCourse.Enrollments)
            {
                await _context.Entry(enrollment)
                              .Reference(x => x.Student)
                              .LoadAsync();
            }

            viewModel.Enrollments = selectedCourse.Enrollments;
        }

        return View(viewModel);
    }

    // GET: Instructors/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructors
                                       .FirstOrDefaultAsync(m => m.Id == id);
        if (instructor == null)
        {
            return NotFound();
        }

        return View(instructor);
    }

    // GET: Instructors/Create
    public IActionResult Create()
    {
        var instructor = new Instructor();
        instructor.CourseAssignments = new List<CourseAssignment>();
        PopulateAssignedCourseData(instructor);
        return View();
    }

    // POST: Instructors/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FirstMidName,HireDate,LastName,Email,OfficeAssignment")] Instructor instructor,
                                            string[]                                                                   selectedCourses)
    {
        if (selectedCourses != null)
        {
            instructor.CourseAssignments = new List<CourseAssignment>();
            foreach (var course in selectedCourses)
            {
                var courseToAdd = new CourseAssignment { InstructorId = instructor.Id, CourseId = int.Parse(course) };
                instructor.CourseAssignments.Add(courseToAdd);
            }
        }

        if (ModelState.IsValid)
        {
            _context.Add(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        PopulateAssignedCourseData(instructor);
        return View(instructor);
    }

    // GET: Instructors/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructors
                                       .Include(i => i.OfficeAssignment)
                                       .Include(i => i.CourseAssignments)
                                       .ThenInclude(i => i.Course)
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(m => m.Id == id);
        if (instructor == null)
        {
            return NotFound();
        }

        PopulateAssignedCourseData(instructor);
        return View(instructor);
    }

    private void PopulateAssignedCourseData(Instructor instructor)
    {
        var allCourses        = _context.Courses;
        var instructorCourses = new HashSet<long>(instructor.CourseAssignments.Select(c => c.CourseId));
        var viewModel         = new List<AssignedCourseData>();
        foreach (var course in allCourses)
        {
            viewModel.Add(new AssignedCourseData
            {
                CourseId = course.Id,
                Title    = course.Title,
                Assigned = instructorCourses.Contains(course.Id)
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
                                          string[] selectedCourses)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructorToUpdate = await _context.Instructors
                                               .Include(i => i.OfficeAssignment)
                                               .Include(i => i.CourseAssignments)
                                               .ThenInclude(i => i.Course)
                                               .FirstOrDefaultAsync(m => m.Id == id);

        if (await TryUpdateModelAsync<Instructor>(
                instructorToUpdate,
                "",
                i => i.FirstMidName, i => i.LastName, i => i.Email, i => i.HireDate, i => i.OfficeAssignment))
        {
            if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment?.Location))
            {
                instructorToUpdate.OfficeAssignment = null;
            }

            UpdateInstructorCourses(selectedCourses, instructorToUpdate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. "                 +
                                             "Try again, and if the problem persists, " +
                                             "see your system administrator.");
            }

            return RedirectToAction(nameof(Index));
        }

        UpdateInstructorCourses(selectedCourses, instructorToUpdate);
        PopulateAssignedCourseData(instructorToUpdate);
        return View(instructorToUpdate);
    }

    private void UpdateInstructorCourses(string[]   selectedCourses,
                                         Instructor instructorToUpdate)
    {
        if (selectedCourses == null)
        {
            instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
            return;
        }

        var selectedCoursesHS = new HashSet<string>(selectedCourses);
        var instructorCourses = new HashSet<long>
            (instructorToUpdate.CourseAssignments.Select(c => c.Course.Id));
        foreach (var course in _context.Courses)
        {
            if (selectedCoursesHS.Contains(course.Id.ToString()))
            {
                if (!instructorCourses.Contains(course.Id))
                {
                    instructorToUpdate.CourseAssignments.Add(new CourseAssignment { InstructorId = instructorToUpdate.Id, CourseId = course.Id });
                }
            }
            else
            {

                if (instructorCourses.Contains(course.Id))
                {
                    CourseAssignment courseToRemove = instructorToUpdate.CourseAssignments.FirstOrDefault(i => i.CourseId == course.Id);
                    _context.Remove(courseToRemove);
                }
            }
        }
    }

    // GET: Instructors/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructors
                                       .FirstOrDefaultAsync(m => m.Id == id);
        if (instructor == null)
        {
            return NotFound();
        }

        return View(instructor);
    }

    // POST: Instructors/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        Instructor instructor = await _context.Instructors
                                              .Include(i => i.CourseAssignments)
                                              .SingleAsync(i => i.Id == id);

        _context.Instructors.Remove(instructor);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InstructorExists(long id)
    {
        return _context.Instructors.Any(e => e.Id == id);
    }
}