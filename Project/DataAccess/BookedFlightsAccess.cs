using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class BookedFlightsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/bookedflights.json"));

    public static Dictionary<string, List<BookedFlightsModel>> LoadAll()
    {
        try
        {
            if (!File.Exists(path))
            {
                return new Dictionary<string, List<BookedFlightsModel>>();
            }

            string json = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Deserialize<Dictionary<string, List<BookedFlightsModel>>>(json, options)
                ?? new Dictionary<string, List<BookedFlightsModel>>();
        }
        catch (Exception ex)
        {
            throw new($"Error loading booked flights: {ex.Message}");
            return new Dictionary<string, List<BookedFlightsModel>>();
        }
    }

    public static void WriteAll(string email, List<BookedFlightsModel> newBookedFlights)
    {
        // Load the current booked flights data from the file
        Dictionary<string, List<BookedFlightsModel>> bookedFlights = LoadAll();

        int flightPoints = 0;

        // Check if the email (key) already exists in the dictionary
        if (bookedFlights.ContainsKey(email))
        {
            // If it exists, replace the existing list with the new one
            bookedFlights[email] = new List<BookedFlightsModel>(newBookedFlights);
        }
        else
        {
            // If the email doesn't exist, create a new entry
            bookedFlights[email] = new List<BookedFlightsModel>(newBookedFlights);
        }

        // Serialize and write the updated data back to the JSON file
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(bookedFlights, options);
        File.WriteAllText(path, json);
    }

    public static void Save(string email, BookedFlightsModel singleFlight)
    {
        Save(email, new List<BookedFlightsModel> { singleFlight });
    }

    public static void Save(string email, List<BookedFlightsModel> newBookedFlights)
    {

        Dictionary<string, List<BookedFlightsModel>> bookedFlights = LoadAll();


        if (bookedFlights.ContainsKey(email))
        {

            foreach (var newFlight in newBookedFlights)
            {
                bool flightUpdated = false;


                for (int i = 0; i < bookedFlights[email].Count; i++)
                {
                    if (bookedFlights[email][i].FlightID == newFlight.FlightID)
                    {

                        bookedFlights[email][i] = newFlight;
                        flightUpdated = true;
                        break;
                    }
                }


                if (!flightUpdated)
                {
                    bookedFlights[email].Add(newFlight);
                }
            }
        }
        else
        {
            // If the email doesn't exist, create a new entry
            bookedFlights[email] = new List<BookedFlightsModel>(newBookedFlights);
        }

        // Serialize and write the updated data back to the JSON file
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(bookedFlights, options);
        File.WriteAllText(path, json);
    }
}
