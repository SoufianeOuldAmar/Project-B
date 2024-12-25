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
    }

}