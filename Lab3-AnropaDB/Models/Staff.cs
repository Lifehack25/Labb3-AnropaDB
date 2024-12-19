using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FullName { get; set; } = null!;

    public string PersonalNumber { get; set; } = null!;

    public string Position { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public static void PrintStaff(SchoolDbContext context)
    {
        Console.WriteLine("1. See all staff\n2. See staff off a particular job title.");

        switch (Console.ReadLine())
        {
            case "1":
                var staff = context.Staff.ToList();
                foreach (var s in staff)
                {
                    Console.WriteLine($"Staff ID: {s.StaffId}, Name: {s.FullName}, Role: {s.Position}");
                }
                break;
            case "2":
                Console.WriteLine("Please enter the job title you would like to see:");
                var jobTitle = Console.ReadLine();
                var staffByJobTitle = context.Staff.Where(s => s.Position == jobTitle).ToList();
                foreach (var s in staffByJobTitle)
                {
                    Console.WriteLine($"Staff ID: {s.StaffId}, Name: {s.FullName}, Role: {s.Position}");
                }
                break;
            default:
                Console.WriteLine("You have entered an invalid option. Please try again.");
                break;
        }
        context.SaveChanges();
    }

    public static void AddNewStaff(SchoolDbContext context)
    {
        Console.WriteLine("Great! You have chosen to add new staff to our database. Please fill in the following information about the new employee.");
        Console.WriteLine("Full name:"); string fullName = Console.ReadLine();
        Console.WriteLine("Personal number:"); string staffPersonalNumber = Console.ReadLine();
        Console.WriteLine("Position:"); string position = Console.ReadLine();

        if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(staffPersonalNumber) || string.IsNullOrEmpty(position))
        {
            Console.WriteLine("You have entered invalid information. Please try again.");
        }
        else if (context.Staff.Any(s => s.PersonalNumber == staffPersonalNumber))
        {
            Console.WriteLine("A staff member with that personal number already exists in the database.");
        }
        else context.Staff.Add(new Staff
        {
            FullName = fullName,
            PersonalNumber = staffPersonalNumber,
            Position = position
        });
        context.SaveChanges();
    }
}
