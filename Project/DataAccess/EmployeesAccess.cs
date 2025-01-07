using System.Linq;
using System.Text.Json;

public static class EmployeesAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Emplyoees.json"));


    public static List<EmployeesModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<EmployeesModel>>(json);
    }


    public static void WriteAll(List<EmployeesModel> employee)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(employee, options);
        File.WriteAllText(path, json);
    }
}