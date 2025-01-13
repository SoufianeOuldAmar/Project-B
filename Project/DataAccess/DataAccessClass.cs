using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DataModels;

public static class DataAccessClass
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions { WriteIndented = true };

    /// <summary>
    /// Reads a JSON file and deserializes it into a list.
    /// </summary>
    public static List<T> ReadList<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<T>();
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
    }

    /// <summary>
    /// Serializes and writes a list to a JSON file.
    /// </summary>
    public static void WriteList<T>(string filePath, List<T> data)
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

        // Optional: Check for duplicates (e.g., based on `Id`)
        if (feedbacks.Any(f => f.Id == feedback.Id))
        {
            Console.WriteLine("Feedback with the same ID already exists.");
            return;
        }

        feedbacks.Add(feedback);
        WriteList<FeedbackModel>("DataSources/feedback.json", feedbacks);
    }

    public static void AdminAddNewFlight(FlightModel newFlight)
    {
        var flights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
        flights.Add(newFlight);
        DataAccessClass.WriteList<FlightModel>("DataSources/flights.json", flights);
    }

    public static void UpdateCurrentAccount(AccountModel currentAccount)
    {
        // Load all accounts from the JSON file
        var accounts = DataAccessClass.ReadList<AccountModel>("DataSources/accounts.json");

        // Find the current account in the list by its ID
        var accountIndex = accounts.FindIndex(acc => acc.Id == currentAccount.Id);

        // currentAccount.TotalFlightPoints = currentAccount.FlightPointsDataList.Sum(flightPointModel => flightPointModel.Points);

        if (accountIndex != -1)
        {
            // Update the account in the list
            accounts[accountIndex] = currentAccount;

            // Write the updated list back to the JSON file
            DataAccessClass.WriteList<AccountModel>("DataSources/accounts.json", accounts);
            // Console.WriteLine("Account successfully updated in the JSON file.");
        }
        else
        {
            Console.WriteLine("Current account not found in the JSON file.");
        }
    }

}
