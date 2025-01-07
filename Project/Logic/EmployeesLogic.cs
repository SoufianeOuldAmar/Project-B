using System;
using System.IO;


public static class EmployeesLogic
{
    public static bool EmpCopyToDestinationLogic(string filePath)
    {
        if (File.Exists(filePath))
        {
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "EmployeesCV");

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            string fileName = Path.GetFileName(filePath);
            string newFilePath = Path.Combine(destinationPath, fileName);
            File.Copy(filePath, newFilePath, true);
            return true;
        }

        return false;
    }
    public static bool NameLogic(string input)
    {
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                return true;
            }
        }
        return false;
    }

    public static List<EmployeesModel> GetAllEmployees()
    {
        return EmployeesAccess.LoadAll();
    }
    public static EmployeesModel SelectEmployeeId(int id)
    {
        var employee = EmployeesAccess.LoadAll();

        return employee.FirstOrDefault(empl => empl.Id == id);
    }

    // public static void OpenFile(string filePath)
    // {
    //     string command = Environment.OSVersion.Platform == PlatformID.Win32NT ? "explorer" :
    //                      Environment.OSVersion.Platform == PlatformID.Unix ? "open" : "";

    //     if (!string.IsNullOrEmpty(command))
    //     {
    //         System.Diagnostics.Process.Start(command, filePath);
    //     }
    //     else
    //     {
    //         Console.WriteLine("Unsupported operating system for opening files.");
    //     }
    // }
    public static bool SaveChangesLogic(EmployeesModel employee)
    {
        var employees = EmployeesAccess.LoadAll();
        var emloyeeToUpdate = employees.FirstOrDefault(f => f.Id == employee.Id);
        if (emloyeeToUpdate != null)
        {
            emloyeeToUpdate.Accepted = employee.Accepted;
            EmployeesAccess.WriteAll(employees);
            return true;
        }
        return false;
    }
    public static void OpenFile(string filePath)
    {
        // Determine the operating system
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            // If Windows, use 'explorer' to open the file
            System.Diagnostics.Process.Start("explorer", filePath);
        }
        else if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            // If macOS or Unix-based, use 'open' to open the file
            System.Diagnostics.Process.Start("open", filePath);
        }
        else
        {
            // If the operating system is unsupported, show an error message
            Console.WriteLine("Unsupported operating system for opening files.");
        }
    }

}
