using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public string Grade1 { get; set; } = null!;

    public DateOnly GradeDate { get; set; }

    public int Student { get; set; }

    public int Course { get; set; }

    public int Teacher { get; set; }

    public virtual Course CourseNavigation { get; set; } = null!;

    public virtual Student StudentNavigation { get; set; } = null!;

    public virtual Staff TeacherNavigation { get; set; } = null!;

    public static void PrintGradesSetWithinXDays(SchoolDbContext context)
    {
        Console.WriteLine("How long would you like the grades to go back? (input a number)");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int days))
        {
            var gradingList = context.Grades.Where(grade => grade.GradeDate >= DateOnly.FromDateTime(DateTime.Now.AddDays(-days))).ToList();

            foreach (var grade in gradingList)
            {
                Console.WriteLine($"Grade ID: {grade.GradeId}, " +
                    $"Grade: {grade.Grade1}, " +
                    $"Date: {grade.GradeDate}, " +
                    $"Student: {grade.StudentNavigation.FirstName} {grade.StudentNavigation.LastName}, " +
                    $"Course: {grade.CourseNavigation.CourseName}");
            }
        }
        else
        {
            Console.WriteLine("Invalid number entered!");
        }
    }
}
