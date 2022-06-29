﻿using System.Collections.Generic;
using Domain.Entities;

namespace WebMvc.Models.SchoolViewModels;

public class InstructorIndexData
{
    public IEnumerable<Instructor> Instructors { get; set; }
    public IEnumerable<Course>     Courses     { get; set; }
    public IEnumerable<Enrollment> Enrollments { get; set; }
}