using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


public static class FlightsAccess
{
    private static string filePath = "DataSources/flights.json";

    // ReadAll methode: Lees alle vluchten uit JSON
    public static List<FlightModel> ReadAll()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath); // Lees het JSON-bestand
                var flights = JsonSerializer.Deserialize<List<FlightModel>>(jsonString); // Deserialiseer naar lijst van FlightModel
                if (flights == null || flights.Count == 0)
                {
                    return new List<FlightModel>(); // Lege lijst teruggeven als er geen vluchten zijn
                }
                return flights;
            }
            else
            {
                return new List<FlightModel>(); // Lege lijst teruggeven als bestand niet bestaat
            }
        }
        catch (Exception ex)
        {
            return new List<FlightModel>(); // Lege lijst teruggeven bij fout
        }
    }

    // WriteAll methode: Schrijf alle vluchten naar JSON
    public static void WriteAll(List<FlightModel> flights)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(flights, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString); // Schrijf de JSON-string naar het bestand
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing all flights to JSON: {ex.Message}");
        }
    }
}