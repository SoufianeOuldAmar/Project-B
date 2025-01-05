using System;
using DataModels;

public static class EmployeesPresentation
{
    public static void InfoEmployeesPresentation()
    {
        Console.Clear();
        string name;
        while (true)
        {
            Console.WriteLine("Please enter your name: ");
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name) || EmployeesLogic.NameLogic(name))
            {
                Console.WriteLine("Invalid input. Name cannot be empty, contain only spaces, or have numbers. Please try again.");
            }
            else
            {
                break;
            }
        }

        int age;
        while (true)
        {
            Console.WriteLine("Please enter your age");
            string input = Console.ReadLine();
            if (int.TryParse(input, out age) && age > 16)
            {
                break;
            }
            else
            {
                Console.WriteLine("We only accept employees who are 17 years old or older.");
            }
        }

        string filePath;
        while (true)
        {
            Console.WriteLine("Please enter the full path of the CV file: ");
            filePath = Console.ReadLine();


            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("File path cannot be empty. Please try again.");
                continue;
            }


            if (!File.Exists(filePath))
            {
                Console.WriteLine("The file does not exist. Please check the path and try again.");
                continue;
            }

            break;
        }

        bool success = EmployeesLogic.EmpCopyToDestinationLogic(filePath);

        if (success)
        {
            Console.WriteLine("File successfully uploaded!");
        }
        else
        {
            Console.WriteLine("There was an error uploading the file.");
        }
        Random random = new Random();
        HashSet<int> UniqueRegistrationID = new HashSet<int>();

        int registrationID;
        while (true)
        {

            registrationID = random.Next(10000, 100000);
            if (!UniqueRegistrationID.Contains(registrationID))
            {
                UniqueRegistrationID.Add(registrationID);
                break;
            }
        }

        Console.Write($"Thank you for applying! This is your registration number: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{registrationID}");
        Console.ResetColor();
        Console.WriteLine(". Pleas keep it safe, as you will need it to log in later and check the status of your application");


        string cvFileName = Path.GetFileName(filePath);

        List<EmployeesModel> employees = EmployeesAccess.LoadAll();
        EmployeesModel newEmployee = new EmployeesModel(
            name,
            age,
            // false,
            cvFileName,
            registrationID
        )
        {
            Id = employees.Count() + 1
        };

        employees.Add(newEmployee);
        EmployeesAccess.WriteAll(employees);
        Console.WriteLine("Your application has been received successfully. It will be processed shortly.");

    }

    public static void ViewRegistrationStatus(int registerID)
    {
        var AllEmployees = EmployeesLogic.GetAllEmployees();

        while (true)
        {

            var register = AllEmployees.FirstOrDefault(f => f.RegistrationID == registerID);
            if (register != null)
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                var RegisterName = AllEmployees.FirstOrDefault(r => r.Name == name);
                if (register.Name == name)
                {
                    Console.Write($"Your application status: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(register.Accepted);
                    Console.ResetColor();
                    Console.WriteLine("Press any key to go back to the main menu.");
                    Console.ReadKey();
                    Console.Clear();
                    return;

                }
                else
                {
                    Console.WriteLine("The name does not match the Registration ID. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("No employee found with this Registration ID. Please try again.");
                Console.WriteLine("Press any key to go back to the main menu.");
                Console.ReadKey();
                Console.Clear();
                return;

                // }
                // else
                // {
                //     Console.WriteLine("Invalid Registration ID format. Please enter a valid ID.");
                // }
                // }
            }
        }
    }
    public static void EmployeeRegistrationpresentation()
    {
        Console.Clear();
        while (true)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the employee registration system.");
            Console.ResetColor();
            Console.WriteLine("Would you like to:");
            Console.WriteLine("1. Register as a new employee");
            Console.WriteLine("2. View the status of your registration");
            Console.WriteLine("q. to quite");
            Console.Write("Please enter the number of your choice: ");

            string input = Console.ReadLine().ToLower();
            if (input == "1")
            {
                InfoEmployeesPresentation();
                break;

            }
            else if (input == "2")
            {
                while (true)
                {

                    Console.WriteLine("Enter your Registration ID to view the status.");
                    string registrationIDinput = Console.ReadLine();
                    if (int.TryParse(registrationIDinput, out int registrationID))
                    {

                        ViewRegistrationStatus(registrationID);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Registration ID. Please enter any key to continue or 'q' to quit.");
                        string input1 = Console.ReadLine().ToLower();
                        if (input1 == "q")
                        {
                            Console.WriteLine("Exiting to the main menu.");
                            return;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            else if (input == "q")
            {
                Console.WriteLine("Exiting to the main menu.");
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
        }
    }


}
