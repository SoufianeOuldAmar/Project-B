using System;
using System.Collections.Generic;
using DataModels;

public static class DataManagerLogic
{
    public static List<T> GetAll<T>(string filePath) where T : IDataModel
    {
        try
        {
            return DataAccessClass.ReadList<T>(filePath);
        }
        catch (Exception ex)
        {
            throw new($"Error reading data from {filePath}: {ex.Message}");
            return new List<T>(); // Return an empty list in case of errors
        }
    }

    public static void Save<T>(string filePath, List<T> data) where T : IDataModel
    {
        try
        {
            DataAccessClass.WriteList(filePath, data);
        }
        catch (Exception ex)
        {
            throw new($"Error saving data to {filePath}: {ex.Message}");
        }
    }

    public static void AddFeedback(FeedbackModel feedback)
    {
        DataAccessClass.AddFeedback(feedback);
    }

    public static void AdminAddNewFlight(FlightModel newFlight)
    {
        DataAccessClass.AddSingleFlight(newFlight);
    }

    public static void UpdateCurrentAccount(UserAccountModel userAccountModel)
    {
        DataAccessClass.UpdateCurrentAccount(userAccountModel);
    }

    public static void SavePayments(List<Payment> payments)
    {
        DataAccessClass.SavePayments(payments);
    }

    public static void WriteAll(string email, List<BookedFlightsModel> newBookedFlights)
    {
        BookedFlightsAccess.WriteAll(email, newBookedFlights);
    }

    public static Dictionary<string, List<BookedFlightsModel>> LoadAll()
    {
        return BookedFlightsAccess.LoadAll();
    }

    public static void Save(string email, BookedFlightsModel singleFlight)
    {
        BookedFlightsAccess.Save(email, new List<BookedFlightsModel> { singleFlight });
    }

    public static void Save(string email, List<BookedFlightsModel> newBookedFlights)
    {
        BookedFlightsAccess.Save(email, newBookedFlights);
    }

    public static List<BookedFlightsModel> LoadByEmail(string email)
    {
        return BookedFlightsAccess.LoadByEmail(email);
    }
}
