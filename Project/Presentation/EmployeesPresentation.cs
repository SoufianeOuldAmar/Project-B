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
        string cvFileName = Path.GetFileName(filePath);

        List<EmployeesModel> employees = EmployeesAccess.LoadAll();
        EmployeesModel newEmployee = new EmployeesModel(
            name,
            age,
            // false,
            cvFileName
        )
        {
            Id = employees.Count() + 1
        };

        employees.Add(newEmployee);
        EmployeesAccess.WriteAll(employees);
        Console.WriteLine("Your application has been received successfully. It will be processed shortly.");

    }
}
