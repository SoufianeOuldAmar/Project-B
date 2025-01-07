using System.Linq;
using System.Text.Json;

public static class AccountsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));


    public static List<AccountModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<AccountModel>>(json);
    }


    public static void WriteAll(List<AccountModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }

    public static void UpdateCurrentAccount(AccountModel currentAccount)
    {
        // Load all accounts from the JSON file
        var accounts = AccountsAccess.LoadAll();

        // Find the current account in the list by its ID
        var accountIndex = accounts.FindIndex(acc => acc.Id == currentAccount.Id);

        currentAccount.TotalFlightPoints = currentAccount.FlightPointsDataList.Sum(flightPointModel => flightPointModel.Points);

        if (accountIndex != -1)
        {
            // Update the account in the list
            accounts[accountIndex] = currentAccount;

            // Write the updated list back to the JSON file
            AccountsAccess.WriteAll(accounts);
            // Console.WriteLine("Account successfully updated in the JSON file.");
        }
        else
        {
            Console.WriteLine("Current account not found in the JSON file.");
        }
    }

}