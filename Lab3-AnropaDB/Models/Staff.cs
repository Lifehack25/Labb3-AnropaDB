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
        while (true)
        {
            Console.WriteLine("Interesting, so you want to see a list of our staff. Choose one of the options below: " +
                "\n1. See all staff" +
                "\n2. See staff by a particular job title" +
                "\n3. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var staff = context.Staff.ToList();
                    if (staff.Count != 0)
                    {
                        foreach (var s in staff)
                        {
                            Console.WriteLine($"Staff ID: {s.StaffId}, Name: {s.FullName}, Role: {s.Position}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No staff members found in the database.");
                    }
                    break;

                case "2":
                    Console.WriteLine("Please enter the job title you would like to see:");
                    var jobTitle = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(jobTitle))
                    {
                        Console.WriteLine("Invalid input. Job title cannot be empty.");
                        break;
                    }

                    var staffByJobTitle = context.Staff
                        .Where(s => s.Position.ToLower().Contains(jobTitle.ToLower()))
                        .ToList();

                    if (staffByJobTitle.Count != 0)
                    {
                        foreach (var s in staffByJobTitle)
                        {
                            Console.WriteLine($"Staff ID: {s.StaffId}, Name: {s.FullName}, Role: {s.Position}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No staff members found with the job title: {jobTitle}");
                    }
                    break;

                case "3":
                    Console.WriteLine("Exiting the staff viewer. Goodbye!");
                    return;

                default:
                    Console.WriteLine("You have entered an invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
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
        Console.WriteLine("Staff member added successfully.");
    }
}
