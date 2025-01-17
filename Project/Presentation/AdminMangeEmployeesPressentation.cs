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
            const int pageSize = 3;
            int currentPage = 0;
            int totalPages = (int)Math.Ceiling(AllEmployees.Count / (double)pageSize);

            

            if (AllEmployees == null || AllEmployees.Count == 0)
            {   
                Console.WriteLine("=== 👤 Review employee(s) ===\n");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Employees found.");
                Console.ResetColor();
                return;
            }
            while (true)
            {   
                Console.Clear();
                Console.WriteLine("=== 👤 Review employee(s) ===\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Displaying page {currentPage + 1}/{totalPages}");
                Console.WriteLine("=====================================");
                Console.ResetColor();

                // Get employees for the current page
                var emlooesTodisplay = AllEmployees
                    .Skip(currentPage * pageSize)
                    .Take(pageSize)
                    .ToList();

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

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nOptions:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("N - Next Page");
                Console.WriteLine("B - Previous Page");
                Console.WriteLine("R - Review employee by ID");
                Console.WriteLine("Q - Quit\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter your choice: ");
                Console.ResetColor();
                string input = Console.ReadLine().ToUpper();

                if (input == "N" && currentPage < totalPages - 1)
                {
                    currentPage++;
                }
                else if (input == "B" && currentPage > 0)
                {
                    currentPage--;
                }
                else if (input == "R")
                {
                    Console.Write("Enter the Emplooy ID to review: ");
                    if (int.TryParse(Console.ReadLine(), out int employeeId))
                    {
                        var employee = AllEmployees.FirstOrDefault(f => f.Id == employeeId);
                        if (employee != null)
                        {
                            ReviewJobApplication(employee); // Call a review to edit the employee

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Employee not found.");
                            Console.ResetColor();
                        }
                    }
                }
                else if (input == "Q")
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option.");
                    Console.ResetColor();
                }

            }
        }

        public static void ReviewJobApplication(EmployeesModel selectedEmployee)
        {
            while (true)
            {
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
                }
                else if (openFileInput == "n")
                {

                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                    continue;
                }

                Console.WriteLine("Would you like to add a review for this employee? (y/n)");
                string reviewInput = Console.ReadLine().ToLower();

                if (reviewInput == "y")
                {
                    Console.WriteLine("Please enter your review for this employee: ");
                    string review = Console.ReadLine();
                    selectedEmployee.Accepted = review;
                    EmployeesLogic.SaveChangesLogic(selectedEmployee);
                    break;
                }
                else if (reviewInput == "n")
                {
                    Console.WriteLine("No review added");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                }
            }
        }


    }

}