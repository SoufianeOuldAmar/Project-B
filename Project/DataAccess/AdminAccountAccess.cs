using System.Text.Json;
public class AdminAccountAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/AdminAccount.json"));
    // static string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/AdminAccount.json"));



    public static List<AdminAccountModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<AdminAccountModel>>(json);
    }


}