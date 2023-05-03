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

        coursesDbSet.AddRange(courses);

        context.SaveChanges();
        
        var instructorKim = Instructor.Create("Kim", "Abercrombie", "kim.abercrombie@rmail.com", DateTime.Parse("1995-03-11")).Value;
        instructorKim.Courses.Add(courses.Single(c => c.Title == "Chemistry"));
        instructorKim.Courses.Add(courses.Single(c => c.Title == "Calculus"));
        instructorKim.Courses.Add(courses.Single(c => c.Title == "Composition"));
        instructorKim.AddOrUpdateOffice("Smith 17");

        var instructorRoget = Instructor.Create("Roget", "Harui", "roger.harui@email.com", DateTime.Parse("1998-07-01")).Value;
        instructorRoget.Courses.Add(courses.Single(c => c.Title == "Microeconomics"));
        instructorRoget.Courses.Add(courses.Single(c => c.Title == "Macroeconomics"));
        instructorRoget.AddOrUpdateOffice("Thompson 27");

        Instructor[] instructors = {
            instructorKim,
            instructorRoget

        };

        var instructorsDbSet = context.Set<Instructor>();
        instructorsDbSet.AddRange(instructors);
        context.SaveChanges();

        var carsonAlexander = Student.Create("Carson", "Alexander", "carson.alexander@email.com", DateTime.Parse("2010-09-01"))
                             .Value;
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Chemistry"), Grade.A);
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Microeconomics"), Grade.C);
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Macroeconomics"), Grade.B);


        var meredithAlonso = Student.Create("Meredith", "Alonso", "meredith.alonso@email.com", DateTime.Parse("2012-09-01"))
                             .Value;
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Calculus"), Grade.B);
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Trigonometry"), Grade.B);
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Composition"), Grade.B);


        var arturoAnand = Student.Create("Arturo", "Anand", "arturo.anand@email.com", DateTime.Parse("2013-09-01"))
                             .Value;
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Chemistry"), null);
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Microeconomics"), Grade.B);


        var gytisBarzdukas = Student.Create("Gytis", "Barzdukas", "gytis.barzdukas@yahoo.com", DateTime.Parse("2012-09-01"))
                             .Value;
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Chemistry"), Grade.B);


        var yanLi = Student.Create("Yan", "Li", "yan.li@email.com", DateTime.Parse("2012-09-01"))
                             .Value;
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Composition"), Grade.B);


        var peggyJustice = Student.Create("Peggy", "Justice", "peggy.justice@email.com", DateTime.Parse("2011-09-01"))
                             .Value;
        carsonAlexander.EnrollIn(courses.Single(c => c.Title == "Literature"), Grade.B);


        var lauraNorman = Student.Create("Laura", "Norman", "laura.norman@email.com", DateTime.Parse("2013-09-01"))
                             .Value;

        var ninoOlivetto = Student.Create("Nino", "Olivetto", "nino.olivetto@email.com", DateTime.Parse("2005-09-01"))
                             .Value;

        Student[] students =
        {
            carsonAlexander,
            meredithAlonso,
            arturoAnand,
            gytisBarzdukas,
            yanLi,
            peggyJustice,
            lauraNorman,
            ninoOlivetto
        };

        studentsDbSet.AddRange(students);
        context.SaveChanges();
    }
}