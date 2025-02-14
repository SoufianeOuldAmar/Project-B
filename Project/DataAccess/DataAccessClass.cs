using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DataModels;

public static class DataAccessClass
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions { WriteIndented = true };

    public static List<T> ReadList<T>(string filePath) where T : IDataModel
    {
        if (!File.Exists(filePath))
        {
            return new List<T>();
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
    }

    public static void WriteList<T>(string filePath, List<T> data) where T : IDataModel
    {
        string json = JsonSerializer.Serialize(data, Options);
        File.WriteAllText(filePath, json);
    }

    public static void AddFeedback(FeedbackModel feedback)
    {
        if (feedback == null || string.IsNullOrWhiteSpace(feedback.Content))
        {
            throw new ArgumentException("Feedback cannot be null or empty.");
        }

        var feedbacks = DataAccessClass.ReadList<FeedbackModel>("DataSources/feedback.json");

        if (feedbacks.Any(f => f.Id == feedback.Id))
        {
            throw new("Feedback with the same ID already exists.");
            return;
        }

        feedbacks.Add(feedback);
        WriteList<FeedbackModel>("DataSources/feedback.json", feedbacks);
    }

    public static void AddSingleFlight(FlightModel newFlight)
    {
        var flights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
        flights.Add(newFlight);
        DataAccessClass.WriteList<FlightModel>("DataSources/flights.json", flights);
    }

    public static void UpdateCurrentAccount(UserAccountModel currentAccount)
    {
        // Load all accounts from the JSON file
        var accounts = DataAccessClass.ReadList<UserAccountModel>("DataSources/useraccounts.json");

        // Find the current account in the list by its ID
        var accountIndex = accounts.FindIndex(acc => acc.Id == currentAccount.Id);


        if (accountIndex != -1)
        {
            // Update the account in the list
            accounts[accountIndex] = currentAccount;

            // Write the updated list back to the JSON file
            DataAccessClass.WriteList<UserAccountModel>("DataSources/useraccounts.json", accounts);
        }
        else
        {
            throw new("Current account not found in the JSON file.");
        }
    }

    public static void SavePayments(List<Payment> payments)
    {
        string _path = "DataSources/financialreports.json";
        var existingPayments = ReadList<Payment>(_path);
        existingPayments.AddRange(payments);
        var jsonData = JsonSerializer.Serialize(existingPayments, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_path, jsonData);
    }

    public static void DeleteBookedFlights(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting bookedflights.json: {ex.Message}");
        }
    }

    public static bool EmpCopyToDestinationLogic(string filePath)
    {
        if (File.Exists(filePath))
        {
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "DataSources/EmployeesCV");

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

}
