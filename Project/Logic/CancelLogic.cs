using System.Text.Json;
using DataModels;
using System.Text;
using DataAccess;

public static class CancelLogic
{
    public static string fileName = "DataSources/flights.json";
    public static List<FlightModel> LoadFlights(string fileName)
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            // Deserialize the JSON content into a list of FlightModel objects
            return JsonSerializer.Deserialize<List<FlightModel>>(json) ?? new List<FlightModel>();
        }
        return new List<FlightModel>();
    }

}
