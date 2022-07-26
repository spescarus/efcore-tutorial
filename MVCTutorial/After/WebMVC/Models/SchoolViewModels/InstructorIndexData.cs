using System.Collections.Generic;
using ApplicationServices.Services.Instructors.Responses;
using ApplicationServices.Services.Students.Responses;

namespace WebMvc.Models.SchoolViewModels;

public class InstructorIndexData
{
    public IEnumerable<InstructorResponse> Instructors { get; set; }
    public IEnumerable<CourseAssignmentResponse>     Courses     { get; set; }
    public IEnumerable<EnrollmentResponse> Enrollments { get; set; }
}