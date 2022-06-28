using System;
using System.Linq;
using EfCoreMvcTutorial.Models;

namespace EfCoreMvcTutorial.Data;

public static class DbInitializer
{
    public static void Initialize(SchoolContext context)
    {
        context.Database.EnsureCreated();

        // Look for any students.
        if (context.Students.Any())
        {
            return; // DB has been seeded
        }

        var students = new Student[]
        {
            new Student
            {
                FirstMidName   = "Carson", LastName = "Alexander",
                EnrollmentDate = DateTime.Parse("2010-09-01"),
                Email = "carson.alexander@email.com"
            },
            new Student
            {
                FirstMidName   = "Meredith", LastName = "Alonso",
                EnrollmentDate = DateTime.Parse("2012-09-01"),
                Email = "meredith.alonso@email.com"
            },
            new Student
            {
                FirstMidName   = "Arturo", LastName = "Anand",
                EnrollmentDate = DateTime.Parse("2013-09-01"),
                Email = "arturo.anand@email.com"
            },
            new Student
            {
                FirstMidName   = "Gytis", LastName = "Barzdukas",
                EnrollmentDate = DateTime.Parse("2012-09-01"),
                Email = "gytis.barzdukas@yahoo.com"
            },
            new Student
            {
                FirstMidName   = "Yan", LastName = "Li",
                EnrollmentDate = DateTime.Parse("2012-09-01"),
                Email = "yan.li@email.com"
            },
            new Student
            {
                FirstMidName   = "Peggy", LastName = "Justice",
                EnrollmentDate = DateTime.Parse("2011-09-01"),
                Email = "peggy.justice@email.com"
            },
            new Student
            {
                FirstMidName   = "Laura", LastName = "Norman",
                EnrollmentDate = DateTime.Parse("2013-09-01"),
                Email = "laura.norman@email.com"
            },
            new Student
            {
                FirstMidName   = "Nino", LastName = "Olivetto",
                EnrollmentDate = DateTime.Parse("2005-09-01"),
                Email = "nino.olivetto@email.com"
            }
        };

        context.Students.AddRange(students);
        context.SaveChanges();

        var instructors = new Instructor[]
        {
            new Instructor
            {
                FirstMidName = "Kim", LastName = "Abercrombie",
                HireDate     = DateTime.Parse("1995-03-11"),
                Email = "kim.abercrombie@rmail.com"
            },
            new Instructor
            {
                FirstMidName = "Fadi", LastName = "Fakhouri",
                HireDate     = DateTime.Parse("2002-07-06"),
                Email = "fadi.fakhouri@email.com"
            },
            new Instructor
            {
                FirstMidName = "Roger", LastName = "Harui",
                HireDate     = DateTime.Parse("1998-07-01"),
                Email = "roger.harui@email.com"
            },
            new Instructor
            {
                FirstMidName = "Candace", LastName = "Kapoor",
                HireDate     = DateTime.Parse("2001-01-15"),
                Email = "candace.kapoor@email.com"
            },
            new Instructor
            {
                FirstMidName = "Roger", LastName = "Zheng",
                HireDate     = DateTime.Parse("2004-02-12"),
                Email = "roger.zheng@email.com"
            }
        };

        context.Instructors.AddRange(instructors);
        context.SaveChanges();

        var departments = new Department[]
        {
            new Department
            {
                Name      = "English", Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorId = instructors.Single(i => i.LastName == "Abercrombie")
                                          .Id
            },
            new Department
            {
                Name      = "Mathematics", Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorId = instructors.Single(i => i.LastName == "Fakhouri")
                                          .Id
            },
            new Department
            {
                Name      = "Engineering", Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorId = instructors.Single(i => i.LastName == "Harui")
                                          .Id
            },
            new Department
            {
                Name      = "Economics", Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorId = instructors.Single(i => i.LastName == "Kapoor")
                                          .Id
            }
        };

        context.Departments.AddRange(departments);
        context.SaveChanges();

        var courses = new Course[]
        {
            new Course
            {
                CourseId = 1050, Title = "Chemistry", Credits = 3,
                DepartmentId = departments.Single(s => s.Name == "Engineering")
                                          .Id
            },
            new Course
            {
                CourseId = 4022, Title = "Microeconomics", Credits = 3,
                DepartmentId = departments.Single(s => s.Name == "Economics")
                                          .Id
            },
            new Course
            {
                CourseId = 4041, Title = "Macroeconomics", Credits = 3,
                DepartmentId = departments.Single(s => s.Name == "Economics")
                                          .Id
            },
            new Course
            {
                CourseId = 1045, Title = "Calculus", Credits = 4,
                DepartmentId = departments.Single(s => s.Name == "Mathematics")
                                          .Id
            },
            new Course
            {
                CourseId = 3141, Title = "Trigonometry", Credits = 4,
                DepartmentId = departments.Single(s => s.Name == "Mathematics")
                                          .Id
            },
            new Course
            {
                CourseId = 2021, Title = "Composition", Credits = 3,
                DepartmentId = departments.Single(s => s.Name == "English")
                                          .Id
            },
            new Course
            {
                CourseId = 2042, Title = "Literature", Credits = 4,
                DepartmentId = departments.Single(s => s.Name == "English")
                                          .Id
            },
        };

        foreach (Course c in courses)
        {
            context.Courses.Add(c);
        }

        context.SaveChanges();

        var officeAssignments = new OfficeAssignment[]
        {
            new OfficeAssignment
            {
                InstructorId = instructors.Single(i => i.LastName == "Fakhouri")
                                          .Id,
                Location = "Smith 17"
            },
            new OfficeAssignment
            {
                InstructorId = instructors.Single(i => i.LastName == "Harui")
                                          .Id,
                Location = "Gowan 27"
            },
            new OfficeAssignment
            {
                InstructorId = instructors.Single(i => i.LastName == "Kapoor")
                                          .Id,
                Location = "Thompson 304"
            },
        };

        context.OfficeAssignments.AddRange(officeAssignments);
        context.SaveChanges();

        var courseInstructors = new CourseAssignment[]
        {
            new CourseAssignment
            {
                CourseId = courses.Single(c => c.Title == "Chemistry")
                                  .CourseId,
                InstructorId = instructors.Single(i => i.LastName == "Kapoor")
                                          .Id
            },
            new CourseAssignment
            {
                CourseId = courses.Single(c => c.Title == "Chemistry")
                                  .CourseId,
                InstructorId = instructors.Single(i => i.LastName == "Harui")
                                          .Id
            },
            new CourseAssignment
            {
                CourseId = courses.Single(c => c.Title == "Microeconomics")
                                  .CourseId,
                InstructorId = instructors.Single(i => i.LastName == "Zheng")
                                          .Id
            },
            new CourseAssignment
            {
                CourseId = courses.Single(c => c.Title == "Macroeconomics")
                                  .CourseId,
                InstructorId = instructors.Single(i => i.LastName == "Zheng")
                                          .Id
            },
            new CourseAssignment
            {
                CourseId = courses.Single(c => c.Title == "Calculus")
                                  .CourseId,
                InstructorId = instructors.Single(i => i.LastName == "Fakhouri")
                                          .Id
            },
            new CourseAssignment
            {
                CourseId = courses.Single(c => c.Title == "Trigonometry")
                                  .CourseId,
                InstructorId = instructors.Single(i => i.LastName == "Harui")
                                          .Id
            },
            new CourseAssignment
            {
                CourseId = courses.Single(c => c.Title == "Composition")
                                  .CourseId,
                InstructorId = instructors.Single(i => i.LastName == "Abercrombie")
                                          .Id
            },
            new CourseAssignment
            {
                CourseId = courses.Single(c => c.Title == "Literature")
                                  .CourseId,
                InstructorId = instructors.Single(i => i.LastName == "Abercrombie")
                                          .Id
            },
        };

        context.CourseAssignments.AddRange(courseInstructors);
        context.SaveChanges();

        var enrollments = new Enrollment[]
        {
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Alexander")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Chemistry")
                                  .CourseId,
                Grade = Grade.A
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Alexander")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Microeconomics")
                                  .CourseId,
                Grade = Grade.C
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Alexander")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Macroeconomics")
                                  .CourseId,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Alonso")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Calculus")
                                  .CourseId,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Alonso")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Trigonometry")
                                  .CourseId,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Alonso")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Composition")
                                  .CourseId,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Anand")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Chemistry")
                                  .CourseId
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Anand")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Microeconomics")
                                  .CourseId,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Barzdukas")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Chemistry")
                                  .CourseId,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Li")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Composition")
                                  .CourseId,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentId = students.Single(s => s.LastName == "Justice")
                                    .Id,
                CourseId = courses.Single(c => c.Title == "Literature")
                                  .CourseId,
                Grade = Grade.B
            }
        };

        foreach (Enrollment e in enrollments)
        {
            var enrollmentInDataBase = context.Enrollments
                                              .SingleOrDefault(s => s.Student.Id      == e.StudentId &&
                                                                    s.Course.CourseId == e.CourseId);
            if (enrollmentInDataBase == null)
            {
                context.Enrollments.Add(e);
            }
        }

        context.SaveChanges();
    }
}