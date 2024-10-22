using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class FlightsAccess
{
    private string filePath = "flights.json";

    // ReadAll methode: Lees alle vluchten uit JSON
    public List<FlightModel> ReadAll()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath); // Lees het JSON-bestand
                var flights = JsonSerializer.Deserialize<List<FlightModel>>(jsonString); // Deserialiseer naar lijst van FlightModel
                if (flights == null || flights.Count == 0)
                {
                    Console.WriteLine("No flights available in the JSON file.");
                    return new List<FlightModel>(); // Lege lijst teruggeven als er geen vluchten zijn
                }
                Console.WriteLine("All flights successfully read from JSON.");
                return flights;
            }
            else
            {
                Console.WriteLine("Flight JSON file does not exist.");
                return new List<FlightModel>(); // Lege lijst teruggeven als bestand niet bestaat
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading all flights from JSON: {ex.Message}");
            return new List<FlightModel>(); // Lege lijst teruggeven bij fout
        }
    }

    // WriteAll methode: Schrijf alle vluchten naar JSON
    public void WriteAll(List<FlightModel> flights)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(flights, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString); // Schrijf de JSON-string naar het bestand
            Console.WriteLine("All flights successfully written to JSON.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing all flights to JSON: {ex.Message}");
        }
    }
}