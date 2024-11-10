using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DataModels;

namespace DataAccess
{
    public static class FlightsAccess
    {
        private static string filePath = "DataSources/flights.json";

        public static List<FlightModel> ReadAll()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);
                    var flights = JsonSerializer.Deserialize<List<FlightModel>>(jsonString);
                    return flights ?? new List<FlightModel>();
                }
                return new List<FlightModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading flights data: {ex.Message}");
                return new List<FlightModel>();
            }
        }

        public static void WriteAll(List<FlightModel> flights)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(flights, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing flights data: {ex.Message}");
            }
        }

        // Logic to randomly assign a layout to flights
        public static void UpdateFlightsWithRandomLayouts()
        {
            try
            {
                // Read JSON file and deserialize it
                var flightsData = ReadAll();

                // Layout providers to use for random assignment
                var layouts = new List<Func<LayoutModel>>
                {
                    LayoutModel.CreateBoeing737Layout,
                    LayoutModel.CreateAirbusA330200Layout,
                    LayoutModel.CreateBoeing757Layout
                };

                // Randomly assign a layout to each flight
                var random = new Random();
                foreach (var flight in flightsData)
                {
                    int index = random.Next(layouts.Count);
                    flight.Layout = layouts[index]();
                }

                // Serialize updated data back to JSON
                WriteAll(flightsData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating flights with random layouts: {ex.Message}");
            }
        }
    }
}