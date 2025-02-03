using System.Collections.Generic;

public static class AdminManageEmployeesLogic
{
    public static List<EmployeesModel> AllEmployees = DataAccessClass.ReadList<EmployeesModel>("DataSources/employees.json");

    public static bool CheckForEmployees()
    {
        return AllEmployees == null || AllEmployees.Count == 0;
    }

    public static int CalculatePages(int pageSize)
    {
        return (int)Math.Ceiling(AllEmployees.Count / (double)pageSize);
    }

    public static List<EmployeesModel> GetEmployeesForPage(int currentPage, int pageSize)
    {
        var employeesToDisplay = AllEmployees
                    .Skip(currentPage * pageSize)
                    .Take(pageSize)
                    .ToList();

        return employeesToDisplay;
    }

    public static EmployeesModel GetEmployeeByID(int employeeId)
    {   
        var AllEmployees = DataAccessClass.ReadList<EmployeesModel>("DataSources/employees.json");
        return AllEmployees.FirstOrDefault(f => f.Id == employeeId);
    }
}