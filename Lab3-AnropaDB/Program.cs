using Lab3_AnropaDB.Models;

namespace Lab3_AnropaDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Data Source=DORSAY-DESKTOP\SQLEXPRESS;Initial Catalog=SchoolDB;Integrated Security=True;;Trust Server Certificate=True;

            using (var context = new SchoolDbContext())
            {
                // Main menu for the user to interact with the database
                Console.WriteLine("Hey! Welcome to Hogwarts, please choose one of the actions below to continue." +
                    "\n1. Get information about our great staff." +
                    "\n2. Get a list of our students sorted by either first or last name." +
                    "\n3. Get list of our students in a particular class." +
                    "\n4. Get a list of all the grades being set within the last 30 days." +
                    "\n5. Get a list of all the courses with their respective grading information." +
                    "\n6. Add a new student." +
                    "\n7. Add a new employee to our staff.");

                var menuAnswer = Console.ReadLine(); Console.WriteLine();
                switch (menuAnswer)
                {
                    case "1": Staff.PrintStaff(context); break;
                    case "2": Student.PrintStudentsSortedByName(context); break;
                    case "3": Class.PrintStudentsInClass(context); break;
                    case "4": Grade.PrintGradesSetWithinXDays(context); break;
                    case "5": Course.PrintCouresesWithGradeInformation(context); break;
                    case "6": Student.AddNewStudent(context); break;
                    case "7": Staff.AddNewStaff(context); break;
                    default: Console.WriteLine("You have entered an invalid option. Please try again."); break;
                }
            }
        }
    }
}
