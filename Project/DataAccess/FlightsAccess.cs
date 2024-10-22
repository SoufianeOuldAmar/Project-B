using System;
using System.Collections.Generic;
using System.IO;

public class FlightsAccess
{
    private string filePath = "flights.json";

    public void WriteAll(List<FlightModel> flights)
    {
        List<string> jsonLines = new List<string>();
        jsonLines.Add("["); // Begin JSON-array

        for (int i = 0; i < flights.Count; i++)
        {
            FlightModel flight = flights[i];
            jsonLines.Add("  {");
            jsonLines.Add($"    \"Airline\": \"{flight.Airline}\",");
            jsonLines.Add($"    \"Layout\": null,"); // Aangenomen dat Layout null is, pas aan indien nodig
            jsonLines.Add($"    \"TicketPrice\": {flight.TicketPrice},");
            jsonLines.Add($"    \"Gate\": \"{flight.Gate}\",");
            jsonLines.Add($"    \"DepartureAirport\": \"{flight.DepartureAirport}\",");
            jsonLines.Add($"    \"ArrivalDestination\": \"{flight.ArrivalDestination}\",");
            jsonLines.Add($"    \"IsCancelled\": {flight.IsCancelled.ToString().ToLower()},");
            jsonLines.Add($"    \"AvailableSeats\": [\"{string.Join("\", \"", flight.AvailableSeats)}\"],");
            jsonLines.Add($"    \"DepartureDate\": \"{flight.DepartureDate}\",");
            jsonLines.Add($"    \"FlightTime\": \"{flight.FlightTime}\"");
            jsonLines.Add(i < flights.Count - 1 ? "  }," : "  }"); // Voeg een komma toe, behalve na het laatste item
        }

        jsonLines.Add("]"); // Einde JSON-array
        File.WriteAllLines(filePath, jsonLines); // Schrijf naar bestand
    }

    public List<FlightModel> ReadAll()
    {
        if (File.Exists(filePath))
        {
            string[] jsonLines = File.ReadAllLines(filePath);
            List<FlightModel> flights = new List<FlightModel>();
            string json = string.Join("\n", jsonLines);
            json = json.Replace("[", "").Replace("]", "").Trim(); // Verwijder de array-haken

            string[] flightEntries = json.Split(new[] { "}," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var entry in flightEntries)
            {
                // Verwerk elke vluchtinvoer
                var fields = entry.Split(',');
                FlightModel flight = new FlightModel(
                    GetFieldValue(fields[0]), // Airline
                    null, // Aangenomen dat Layout null is
                    decimal.Parse(GetFieldValue(fields[2])), // TicketPrice
                    GetFieldValue(fields[3]), // Gate
                    GetFieldValue(fields[4]), // DepartureAirport
                    GetFieldValue(fields[5]), // ArrivalDestination
                    bool.Parse(GetFieldValue(fields[6])), // IsCancelled
                    GetFieldValue(fields[7]), // DepartureDate
                    GetFieldValue(fields[8]) // FlightTime
                );
                flights.Add(flight);
            }
            return flights;
        }
        return new List<FlightModel>();
    }

    private string GetFieldValue(string field)
    {
        // Verwijder de veldnaam en de aanhalingstekens
        var parts = field.Split(':');
        return parts.Length > 1 ? parts[1].Trim().Trim('"') : string.Empty;
    }
}