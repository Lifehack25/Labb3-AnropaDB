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
        var className = Console.ReadLine();
        var classToPrint = context.Classes.Where(c => c.ClassName == className).FirstOrDefault();
        if (classToPrint != null)
        {
            Console.WriteLine($"Class: {classToPrint.ClassName}");
            foreach (var s in classToPrint.Students)
            {
                Console.WriteLine($"Student ID: {s.StudentId}, Name: {s.FirstName} {s.LastName}");
            }
        }
        else
        {
            Console.WriteLine("The class you entered does not exist.");
        }
    }
}
