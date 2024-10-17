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


    // public static void WriteAll(List<AdminAccountModel> accounts)
    // {
    //     var options = new JsonSerializerOptions { WriteIndented = true };
    //     string json = JsonSerializer.Serialize(accounts, options);
    //     File.WriteAllText(path, json);
    // }



}