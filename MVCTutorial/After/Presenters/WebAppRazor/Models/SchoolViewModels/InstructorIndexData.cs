using System.Collections.Generic;
using Application.Contexts.InstructorContexts.Responses;
using Application.Services.Students.Responses;

namespace WebApp.Models.SchoolViewModels;

public class InstructorIndexData
{
    public IEnumerable<InstructorDetailsResponse> Instructors { get; set; }
    public IEnumerable<CourseAssignmentResponse>     Courses     { get; set; }
    public IEnumerable<EnrollmentResponse> Enrollments { get; set; }
}