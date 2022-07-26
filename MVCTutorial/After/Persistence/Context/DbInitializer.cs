using Domain.Entities;

namespace Persistence.Context;

public sealed class DbInitializer
{
    public static void Initialize(EfCoreContext context)
    {
        context.Database.EnsureCreated();

        var studentsDbSet = context.Set<Student>();
        // Look for any students.
        if (studentsDbSet.Any())
        {
            return; // DB has been seeded
        }

        Student[] students =
        {
            Student.Create("Carson", "Alexander", "carson.alexander@email.com", DateTime.Parse("2010-09-01"))
                   .Value,
            Student.Create("Meredith", "Alonso", "meredith.alonso@email.com", DateTime.Parse("2012-09-01"))
                   .Value,
            Student.Create("Arturo", "Anand", "arturo.anand@email.com", DateTime.Parse("2013-09-01"))
                   .Value,
            Student.Create("Gytis", "Barzdukas", "gytis.barzdukas@yahoo.com", DateTime.Parse("2012-09-01"))
                   .Value,
            Student.Create("Yan", "Li", "yan.li@email.com", DateTime.Parse("2012-09-01"))
                   .Value,
            Student.Create("Peggy", "Justice", "peggy.justice@email.com", DateTime.Parse("2011-09-01"))
                   .Value,
            Student.Create("Laura", "Norman", "laura.norman@email.com", DateTime.Parse("2013-09-01"))
                   .Value,
            Student.Create("Nino", "Olivetto", "nino.olivetto@email.com", DateTime.Parse("2005-09-01"))
                   .Value
        };

        studentsDbSet.AddRange(students);
        context.SaveChanges();

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
        Course[] courses =
        {
            Course.Create(1050, "Chemistry", 3)
                  .Value,
            Course.Create(4022, "Microeconomics", 3)
                  .Value,
            Course.Create(4041, "Macroeconomics", 3)
                  .Value,
            Course.Create(1045, "Calculus", 4)
                  .Value,
            Course.Create(3141, "Trigonometry", 4)
                  .Value,
            Course.Create(2021, "Composition", 3)
                  .Value,
            Course.Create(2042, "Literature", 4)
                  .Value
        };

        var coursesDbSet = context.Set<Course>();

        foreach (Course c in courses)
        {
            coursesDbSet.Add(c);
        }

        context.SaveChanges();

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
        Enrollment[] enrollments =
        {
            Enrollment.Create(courses.Single(c => c.Title == "Chemistry")
                                     .CourseId, students.Single(s => s.Name.LastName == "Alexander")
                                                        .Id, Grade.A.ToString())
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Microeconomics")
                                     .CourseId, students.Single(s => s.Name.LastName == "Alexander")
                                                        .Id, Grade.C.ToString())
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Macroeconomics")
                                     .CourseId, students.Single(s => s.Name.LastName == "Alexander")
                                                        .Id, Grade.B.ToString())
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Calculus")
                                     .CourseId, students.Single(s => s.Name.LastName == "Alonso")
                                                        .Id, Grade.B.ToString())
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Trigonometry")
                                     .CourseId, students.Single(s => s.Name.LastName == "Alonso")
                                                        .Id, Grade.B.ToString())
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Composition")
                                     .CourseId, students.Single(s => s.Name.LastName == "Alonso")
                                                        .Id, Grade.B.ToString())
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Chemistry")
                                     .CourseId, students.Single(s => s.Name.LastName == "Anand")
                                                        .Id, "")
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Microeconomics")
                                     .CourseId, students.Single(s => s.Name.LastName == "Anand")
                                                        .Id, Grade.B.ToString())
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Chemistry")
                                     .CourseId, students.Single(s => s.Name.LastName == "Barzdukas")
                                                        .Id, Grade.B.ToString())
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Composition")
                                     .CourseId, students.Single(s => s.Name.LastName == "Li")
                                                        .Id, Grade.B.ToString())
                      .Value,
            Enrollment.Create(courses.Single(c => c.Title == "Literature")
                                     .CourseId, students.Single(s => s.Name.LastName == "Justice")
                                                        .Id, Grade.B.ToString())
                      .Value
        };

        var enrollmentDbSet = context.Set<Enrollment>();

        foreach (Enrollment e in enrollments)
        {
            var enrollmentInDataBase = enrollmentDbSet.SingleOrDefault(s => s.Student.Id      == e.StudentId &&
                                                                            s.Course.CourseId == e.CourseId);

            if (enrollmentInDataBase == null)
            {
                enrollmentDbSet.Add(e);
            }
        }

        context.SaveChanges();
    }
}