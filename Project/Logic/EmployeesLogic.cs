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

}
