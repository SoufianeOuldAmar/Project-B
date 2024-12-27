using System.Data.Common;
using System.Security.Cryptography;
using DataModels;

namespace DataAccess
{
    public static class AdminMangeEmployeesPressentation
    {
        public static void DisplayEmployeesInfo()
        {
            var AllEmployees = EmployeesLogic.GetAllEmployees();
            if (AllEmployees == null || AllEmployees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Employees found.");
                Console.ResetColor();
                return;
            }
            foreach (var employee in AllEmployees)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"Employee ID: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{employee.Id}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"Employee name: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{employee.Name}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"Employee age: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{employee.Age}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"Situation: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{employee.Accepted}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"CV: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{employee.CvFileName}");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("-----------------------------------------");
                Console.ResetColor();
            }
        }

        public static void ReviewJobApplication()
        {
            while (true)
            {

                Console.WriteLine("Choose an ID...");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int id))
                {
                    var selectedEmployee = EmployeesLogic.SelectEmployeeId(id);
                    if (selectedEmployee == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No employee found with the provided ID. Please Enter another ID");
                        Console.ResetColor();
                    }

                    string cvFilePath = Path.Combine(Environment.CurrentDirectory, "EmployeesCV", selectedEmployee.CvFileName);
                    if (!File.Exists(cvFilePath))
                    {
                        Console.WriteLine("The CV file for this employee could not be found");
                        continue;
                    }
                    Console.WriteLine($"Do you want to open the CV for {selectedEmployee.Name}? (y/n)");
                    string openFileInput = Console.ReadLine()?.ToLower();
                    if (openFileInput == "y")
                    {
                        EmployeesLogic.OpenFile(cvFilePath);
                        Console.WriteLine("Would you like to add a review for this employee? (y/n)");
                        string reviewinput = Console.ReadLine().ToLower();
                        if (reviewinput == "y")
                        {
                            Console.WriteLine("Please enter your review for this employee: ");
                            string review = Console.ReadLine();
                            break;
                        }
                        else if (reviewinput == "n")
                        {
                            Console.WriteLine("No review added");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                        }

                    }

                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again");
                }
            }
        }

    }

}