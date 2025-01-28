using System.Collections.Generic;
using DataModels;

public static class PassengerLogic
{
    public static List<PassengerModel> GetAllPassengers()
    {
        return DataAccessClass.ReadList<PassengerModel>("DataSources/passengers.json");
    }
}