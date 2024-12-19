using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public static void PrintCouresesWithGradeInformation(SchoolDbContext context)
    {
        Console.WriteLine("Courses with their respective grade information: ");
        Console.WriteLine();
        var gradeMapping = new Dictionary<string, int>
                        {
                            { "A", 5 },
                            { "B", 4 },
                            { "C", 3 },
                            { "D", 2 },
                            { "E", 1 },
                            { "F", 0 }
                        };

        // Function to map numeric average back to a letter grade
        string MapToGrade(double avgGrade)
        {
            if (avgGrade >= 4.5) return "A";
            if (avgGrade >= 3.5) return "B";
            if (avgGrade >= 2.5) return "C";
            if (avgGrade >= 1.5) return "D";
            if (avgGrade >= 0.5) return "E";
            return "F";
        }

        var courses = context.Courses.ToList();
        var grades = context.Grades.ToList();

        var query = from course in courses
                    join grade in grades on course.CourseId equals grade.Course into courseGrades
                    from grade in courseGrades.DefaultIfEmpty()
                    group grade by course.CourseName into courseGroup
                    let numericGrades = courseGroup
                        .Where(g => g != null && gradeMapping.ContainsKey(g.Grade1))
                        .Select(g => gradeMapping[g.Grade1])
                    select new
                    {
                        CourseName = courseGroup.Key,
                        AverageGrade = numericGrades.Any()
                            ? MapToGrade(numericGrades.Average())
                            : "No Grades",
                        HighestGrade = numericGrades.Any()
                            ? MapToGrade(numericGrades.Max())
                            : "No Grades",
                        LowestGrade = numericGrades.Any()
                            ? MapToGrade(numericGrades.Min())
                            : "No Grades"
                    };
        var result = query.ToList();

        foreach (var course in result)
        {
            Console.WriteLine($"Course: {course.CourseName} - " +
                $"Average grade: {course.AverageGrade}, " +
                $"Highest grade: {course.HighestGrade}, " +
                $"Lowest grade: {course.LowestGrade}");
        }
    }
}
