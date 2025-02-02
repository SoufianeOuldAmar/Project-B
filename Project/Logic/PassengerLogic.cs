using System.Collections.Generic;
using DataModels;

public static class PassengerLogic
{
    private static List<PassengerModel> GetAllPassengers()
    {
        return DataAccessClass.ReadList<PassengerModel>("DataSources/passengers.json");
    }

    public static int GetPassengerID()
    {
        return GetAllPassengers().Count + 1;
    }

    public static void AddPassengersToFile()
    {
        // Load existing passengers from the file
        var existingPassengers = GetAllPassengers();

        // Add passengers from BookFlightLogic
        existingPassengers.AddRange(BookFlightLogic.passengers);

        // Save the updated list back to the file
        DataAccessClass.WriteList<PassengerModel>("DataSources/passengers.json", existingPassengers);
    }

}