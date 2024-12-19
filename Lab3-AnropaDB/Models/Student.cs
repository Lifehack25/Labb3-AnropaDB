using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string PersonalNumber { get; set; } = null!;

    public string? Class { get; set; }

    public virtual Class? ClassNavigation { get; set; }

    public virtual Enrollment? Enrollment { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public static void PrintStudentsSortedByName(SchoolDbContext context)
    {
        Console.WriteLine("How would you like the list to be sorted? " +
        "\n1. First name" +
        "\n2. Last name");

        switch (Console.ReadLine())
        {
            case "1":
                var studentsByFirstName = context.Students.OrderBy(s => s.FirstName).ToList();
                foreach (var s in studentsByFirstName)
                {
                    Console.WriteLine($"Student ID: {s.StudentId}, Name: {s.FirstName} {s.LastName}");
                }
                break;
            case "2":
                var studentsByLastName = context.Students.OrderBy(s => s.LastName).ToList();
                foreach (var s in studentsByLastName)
                {
                    Console.WriteLine($"Student ID: {s.StudentId}, Name: {s.FirstName} {s.LastName}");
                }
                break;
            default:
                Console.WriteLine("You have entered an invalid option. Please try again.");
                break;
        }
        context.SaveChanges();
    }

    public static void AddNewStudent(SchoolDbContext context)
    {
        Console.WriteLine("Great! You have chosen to add new students to our database. Please fill in the following information about the new student.");
        Console.WriteLine("First name:"); string firstName = Console.ReadLine();
        Console.WriteLine("Last name:"); string lastName = Console.ReadLine();
        Console.WriteLine("Personal number:"); string personalNumber = Console.ReadLine();
        Console.WriteLine("Class:"); string studentClass = Console.ReadLine();

        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(personalNumber) || string.IsNullOrEmpty(studentClass))
        {
            Console.WriteLine("You have entered invalid information. Please try again.");
        }
        else if (context.Students.Any(s => s.PersonalNumber == personalNumber))
        {
            Console.WriteLine("A student with that personal number already exists in the database.");
        }
        else context.Students.Add(new Student
        {
            FirstName = firstName,
            LastName = lastName,
            PersonalNumber = personalNumber,
            Class = studentClass
        });
        context.SaveChanges();
        Console.WriteLine("Student added successfully!");
    }
}
