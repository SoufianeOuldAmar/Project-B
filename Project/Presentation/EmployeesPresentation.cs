using System;
using DataModels;

public static class EmployeesPresentation
{
    public static void InfoEmployeesPresentation()
    {
        string name;
        while (true)
        {
            Console.Clear();

            Console.WriteLine("=== 📝 Register ===\n");

            Console.Write("Please enter your name: ");
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name) || EmployeesLogic.NameLogic(name))
            {
                Console.WriteLine("Invalid input. Name cannot be empty, contain only spaces, or have numbers. Please try again.");
                MenuPresentation.PressAnyKey();
            }
            else
            {
                break;
            }
        }

        int age;
        while (true)
        {
            Console.Write("Please enter your age: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out age))
            {
                Console.WriteLine("Please input a valid number.");
            }
            else if (age > 18 && age < 60)
            {
                break;
            }
            else
            {
                Console.WriteLine("We only accept employees who are 19 years old or older.");

                return;
            }

        }

        string filePath;
        while (true)
        {
            Console.Write("Please enter the full path of the CV file (you do that by dragging the wanted file to the terminal and making sure to enclose the path with ''): ");
            filePath = Console.ReadLine();
            var fileOperationStatus = EmployeesLogic.SaveEmployeeFile(filePath);

            if (fileOperationStatus == FileOperationStatus.EmptyPath)
            {
                Console.WriteLine("File path cannot be empty. Please try again.");
                continue;
            }


            if (fileOperationStatus == FileOperationStatus.FileDoesNotExist)
            {
                Console.WriteLine("The file does not exist. Please check the path and try again.");
                continue;
            }

            if (fileOperationStatus == FileOperationStatus.Success)
            {
                Console.WriteLine("File was successfully uploaded.");
                break;
            }

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

        EmployeesLogic.SaveEmployee(name,
            age,
            cvFileName,
            registrationID);

        Console.WriteLine("Your application has been received successfully. It will be processed shortly.");
    }

    public static void ViewRegistrationStatus(int registerID)
    {
        while (true)
        {

            var register = EmployeesLogic.GetEmployeeByID(registerID);
            if (register != null)
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                var RegisterName = EmployeesLogic.GetEmployeeByName(name);
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
                Console.WriteLine("\nNo employee found with this Registration ID. Please try again.");
                MenuPresentation.PressAnyKey();
                Console.Clear();
                return;
            }
        }
    }
    public static void EmployeeRegistrationpresentation()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("=== 👥 Join Our Team ===\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the employee registration system.\n");
            Console.ResetColor();
            Console.WriteLine("1. 📝 Register as a new employee");
            Console.WriteLine("2. 🕒 View the status of your registration");
            Console.WriteLine("Q. ❌ To quit");
            Console.Write("\nPlease enter the number of your choice: ");

            string input = Console.ReadLine().ToLower();
            if (input == "1")
            {
                InfoEmployeesPresentation();
                break;

            }
            else if (input == "2")
            {
                Console.Clear();

                while (true)
                {
                    Console.WriteLine("=== 🕒 Status of your registration ===\n");

                    Console.Write("Enter your Registration ID to view the status: ");
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
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
        }
    }


}
