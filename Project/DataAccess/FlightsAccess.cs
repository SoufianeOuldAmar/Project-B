using System.Text.Json;
using DataModels;
using System.Text.Json.Serialization;

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
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var flights = JsonSerializer.Deserialize<List<FlightModel>>(jsonString, options);

                    if (flights != null)
                    {
                        foreach (var flight in flights)
                        {
                            if (flight.Layout != null && flight.Layout.SeatInitials == null)
                            {
                                flight.Layout.SeatInitials = new Dictionary<string, string>();
                            }
                        }
                    }

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
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                string jsonString = JsonSerializer.Serialize(flights, options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing flights data: {ex.Message}");
            }
        }


        public static void AdminAddNewFlight(FlightModel newFlight)
        {
            var flights = ReadAll();
            flights.Add(newFlight);
            WriteAll(flights);
        }

        public static List<FlightModel> SearchFlights(
            string departureAirport = null,
            string arrivalDestination = null,
            DateTime? departureDate = null,
            string flightTime = null)
        {
            var flights = ReadAll();

            return flights.Where(flight =>
                (departureAirport == null || flight.DepartureAirport == departureAirport) &&
                (arrivalDestination == null || flight.ArrivalDestination == arrivalDestination) &&
                (!departureDate.HasValue ||
                    (DateTime.TryParse(flight.DepartureDate, out DateTime flightDepartureDate) &&
                     flightDepartureDate.Date == departureDate.Value.Date)) &&
                (flightTime == null || flight.FlightTime == flightTime)
            ).ToList();
        }
    }
}