using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class FlightsAccess
{
    private string filePath = "flights.json";

    public void WriteAll(List<FlightModel> flights)
    {
        string jsonData = JsonConvert.SerializeObject(flights, Formatting.Indented);

        File.WriteAllText(filePath, jsonData);
    }
    public List<FlightModel> ReadAll()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            List<FlightModel> flights = JsonConvert.DeserializeObject<List<FlightModel>>(jsonData);
            return JsonConvert.DeserializeObject<List<FlightModel>>(jsonData);
        }

        return new List<FlightModel>();
    }
}