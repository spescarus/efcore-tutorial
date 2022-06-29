using Domain.Entities;

namespace Persistence.Context;

public class DbInitializer
{
    public static void Initialize(EfCoreContext context)
    {
        // context.Database.EnsureCreated();
        //
        // var studentsDbSet = context.Set<Student>();
        // // Look for any students.
        // if (studentsDbSet.Any())
        // {
        //     return; // DB has been seeded
        // }
        //
        // Student[] students = {
        //     new()
        //     {
        //         FirstMidName   = "Carson", LastName = "Alexander",
        //         EnrollmentDate = DateTime.Parse("2010-09-01"),
        //         Email          = "carson.alexander@email.com"
        //     },
        //     new()
        //     {
        //         FirstMidName   = "Meredith", LastName = "Alonso",
        //         EnrollmentDate = DateTime.Parse("2012-09-01"),
        //         Email          = "meredith.alonso@email.com"
        //     },
        //     new()
        //     {
        //         FirstMidName   = "Arturo", LastName = "Anand",
        //         EnrollmentDate = DateTime.Parse("2013-09-01"),
        //         Email          = "arturo.anand@email.com"
        //     },
        //     new()
        //     {
        //         FirstMidName   = "Gytis", LastName = "Barzdukas",
        //         EnrollmentDate = DateTime.Parse("2012-09-01"),
        //         Email          = "gytis.barzdukas@yahoo.com"
        //     },
        //     new()
        //     {
        //         FirstMidName   = "Yan", LastName = "Li",
        //         EnrollmentDate = DateTime.Parse("2012-09-01"),
        //         Email          = "yan.li@email.com"
        //     },
        //     new()
        //     {
        //         FirstMidName   = "Peggy", LastName = "Justice",
        //         EnrollmentDate = DateTime.Parse("2011-09-01"),
        //         Email          = "peggy.justice@email.com"
        //     },
        //     new()
        //     {
        //         FirstMidName   = "Laura", LastName = "Norman",
        //         EnrollmentDate = DateTime.Parse("2013-09-01"),
        //         Email          = "laura.norman@email.com"
        //     },
        //     new()
        //     {
        //         FirstMidName   = "Nino", LastName = "Olivetto",
        //         EnrollmentDate = DateTime.Parse("2005-09-01"),
        //         Email          = "nino.olivetto@email.com"
        //     }
        // };
        //
        // studentsDbSet.AddRange(students);
        // context.SaveChanges();
        //
        // Instructor[] instructors = {
        //     new()
        //     {
        //         FirstMidName = "Kim", LastName = "Abercrombie",
        //         HireDate     = DateTime.Parse("1995-03-11"),
        //         Email        = "kim.abercrombie@rmail.com"
        //     },
        //     new()
        //     {
        //         FirstMidName = "Fadi", LastName = "Fakhouri",
        //         HireDate     = DateTime.Parse("2002-07-06"),
        //         Email        = "fadi.fakhouri@email.com"
        //     },
        //     new()
        //     {
        //         FirstMidName = "Roger", LastName = "Harui",
        //         HireDate     = DateTime.Parse("1998-07-01"),
        //         Email        = "roger.harui@email.com"
        //     },
        //     new()
        //     {
        //         FirstMidName = "Candace", LastName = "Kapoor",
        //         HireDate     = DateTime.Parse("2001-01-15"),
        //         Email        = "candace.kapoor@email.com"
        //     },
        //     new()
        //     {
        //         FirstMidName = "Roger", LastName = "Zheng",
        //         HireDate     = DateTime.Parse("2004-02-12"),
        //         Email        = "roger.zheng@email.com"
        //     }
        // };
        //
        // var instructorsDbSet = context.Set<Instructor>();
        // instructorsDbSet.AddRange(instructors);
        // context.SaveChanges();
        //
        // Department[] departments = {
        //     new()
        //     {
        //         Name      = "English", Budget = 350000,
        //         StartDate = DateTime.Parse("2007-09-01"),
        //         InstructorId = instructors.Single(i => i.LastName == "Abercrombie")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         Name      = "Mathematics", Budget = 100000,
        //         StartDate = DateTime.Parse("2007-09-01"),
        //         InstructorId = instructors.Single(i => i.LastName == "Fakhouri")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         Name      = "Engineering", Budget = 350000,
        //         StartDate = DateTime.Parse("2007-09-01"),
        //         InstructorId = instructors.Single(i => i.LastName == "Harui")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         Name      = "Economics", Budget = 100000,
        //         StartDate = DateTime.Parse("2007-09-01"),
        //         InstructorId = instructors.Single(i => i.LastName == "Kapoor")
        //                                   .Id
        //     }
        // };
        //
        // var departmentsDbSet = context.Set<Department>();
        //
        // departmentsDbSet.AddRange(departments);
        // context.SaveChanges();
        //
        // Course[] courses =
        // {
        //     new()
        //     {
        //         CourseId = 1050, Title = "Chemistry", Credits = 3,
        //         DepartmentId = departments.Single(s => s.Name == "Engineering")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = 4022, Title = "Microeconomics", Credits = 3,
        //         DepartmentId = departments.Single(s => s.Name == "Economics")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = 4041, Title = "Macroeconomics", Credits = 3,
        //         DepartmentId = departments.Single(s => s.Name == "Economics")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = 1045, Title = "Calculus", Credits = 4,
        //         DepartmentId = departments.Single(s => s.Name == "Mathematics")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = 3141, Title = "Trigonometry", Credits = 4,
        //         DepartmentId = departments.Single(s => s.Name == "Mathematics")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = 2021, Title = "Composition", Credits = 3,
        //         DepartmentId = departments.Single(s => s.Name == "English")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = 2042, Title = "Literature", Credits = 4,
        //         DepartmentId = departments.Single(s => s.Name == "English")
        //                                   .Id
        //     },
        // };
        //
        // var coursesDbSet = context.Set<Course>();
        //
        // foreach (Course c in courses)
        // {
        //     coursesDbSet.Add(c);
        // }
        //
        // context.SaveChanges();
        //
        // OfficeAssignment[] officeAssignments = {
        //     new()
        //     {
        //         InstructorId = instructors.Single(i => i.LastName == "Fakhouri")
        //                                   .Id,
        //         Location = "Smith 17"
        //     },
        //     new()
        //     {
        //         InstructorId = instructors.Single(i => i.LastName == "Harui")
        //                                   .Id,
        //         Location = "Gowan 27"
        //     },
        //     new()
        //     {
        //         InstructorId = instructors.Single(i => i.LastName == "Kapoor")
        //                                   .Id,
        //         Location = "Thompson 304"
        //     },
        // };
        //
        // var officeAssignmentsDbSet = context.Set<OfficeAssignment>();
        //
        // officeAssignmentsDbSet.AddRange(officeAssignments);
        // context.SaveChanges();
        //
        // CourseAssignment[] courseInstructors = {
        //     new()
        //     {
        //         CourseId = courses.Single(c => c.Title == "Chemistry")
        //                           .CourseId,
        //         InstructorId = instructors.Single(i => i.LastName == "Kapoor")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = courses.Single(c => c.Title == "Chemistry")
        //                           .CourseId,
        //         InstructorId = instructors.Single(i => i.LastName == "Harui")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = courses.Single(c => c.Title == "Microeconomics")
        //                           .CourseId,
        //         InstructorId = instructors.Single(i => i.LastName == "Zheng")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = courses.Single(c => c.Title == "Macroeconomics")
        //                           .CourseId,
        //         InstructorId = instructors.Single(i => i.LastName == "Zheng")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = courses.Single(c => c.Title == "Calculus")
        //                           .CourseId,
        //         InstructorId = instructors.Single(i => i.LastName == "Fakhouri")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = courses.Single(c => c.Title == "Trigonometry")
        //                           .CourseId,
        //         InstructorId = instructors.Single(i => i.LastName == "Harui")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = courses.Single(c => c.Title == "Composition")
        //                           .CourseId,
        //         InstructorId = instructors.Single(i => i.LastName == "Abercrombie")
        //                                   .Id
        //     },
        //     new()
        //     {
        //         CourseId = courses.Single(c => c.Title == "Literature")
        //                           .CourseId,
        //         InstructorId = instructors.Single(i => i.LastName == "Abercrombie")
        //                                   .Id
        //     },
        // };
        //
        // var courseAssignmentDbSet = context.Set<CourseAssignment>();
        //
        // courseAssignmentDbSet.AddRange(courseInstructors);
        // context.SaveChanges();
        //
        // Enrollment[] enrollments = {
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Alexander")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Chemistry")
        //                           .CourseId,
        //         Grade = Grade.A
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Alexander")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Microeconomics")
        //                           .CourseId,
        //         Grade = Grade.C
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Alexander")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Macroeconomics")
        //                           .CourseId,
        //         Grade = Grade.B
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Alonso")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Calculus")
        //                           .CourseId,
        //         Grade = Grade.B
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Alonso")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Trigonometry")
        //                           .CourseId,
        //         Grade = Grade.B
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Alonso")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Composition")
        //                           .CourseId,
        //         Grade = Grade.B
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Anand")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Chemistry")
        //                           .CourseId
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Anand")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Microeconomics")
        //                           .CourseId,
        //         Grade = Grade.B
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Barzdukas")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Chemistry")
        //                           .CourseId,
        //         Grade = Grade.B
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Li")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Composition")
        //                           .CourseId,
        //         Grade = Grade.B
        //     },
        //     new()
        //     {
        //         StudentId = students.Single(s => s.LastName == "Justice")
        //                             .Id,
        //         CourseId = courses.Single(c => c.Title == "Literature")
        //                           .CourseId,
        //         Grade = Grade.B
        //     }
        // };
        //
        // var enrollmentDbSet = context.Set<Enrollment>();
        //
        // foreach (Enrollment e in enrollments)
        // {
        //     var enrollmentInDataBase = enrollmentDbSet.SingleOrDefault(s => s.Student.Id      == e.StudentId &&
        //                                                                     s.Course.CourseId == e.CourseId);
        //
        //     if (enrollmentInDataBase == null)
        //     {
        //         enrollmentDbSet.Add(e);
        //     }
        // }
        //
        // context.SaveChanges();
    }
}