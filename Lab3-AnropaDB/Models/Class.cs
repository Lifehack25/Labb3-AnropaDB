using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Models;

public partial class Class
{
    public string ClassName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public static void PrintStudentsInClass(SchoolDbContext context)
    {
        Console.WriteLine("Please enter the class name you would like to see:");
        var className = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(className))
        {
            Console.WriteLine("Invalid input. Class name cannot be empty.");
            return;
        }

        var studentsInClass = context.Students
            .Where(s => s.Class.ToLower().Contains(className.ToLower()))
            .ToList();

        if (studentsInClass.Count != 0)
        {
            Console.WriteLine($"Class: {className}");
            foreach (var student in studentsInClass)
            {
                Console.WriteLine($"Student ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}");
            }
        }
        else
        {
            Console.WriteLine($"No students found in the class: {className}");
        }
    }

}
