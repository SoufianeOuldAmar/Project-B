using System.Collections.Generic;
using DataModels;

public static class SearchFlightLogic
{
    public static List<FlightModel> flights = DataManagerLogic.GetAll<FlightModel>("DataSources/flights.json");

    public static List<string> UniqueDepartures()
    {
        return flights.Select(f => f.DepartureAirport).Distinct().OrderBy(d => d).ToList();
    }

    public static List<string> GetValidDestinations(string departureAirport)
    {
        return string.IsNullOrEmpty(departureAirport)
                ? flights.Select(f => f.ArrivalDestination).Distinct().OrderBy(d => d).ToList()
                : flights.Where(f => f.DepartureAirport.Equals(departureAirport, StringComparison.OrdinalIgnoreCase))
                         .Select(f => f.ArrivalDestination)
                         .Distinct()
                         .OrderBy(d => d)
                         .ToList();
    }

    public static List<FlightModel> FilterAvailableFlights(string departureAirport, string arrivalDestination, string departureDateString, string timeOfDay, int seatCount)
    {
        Dictionary<string, (TimeSpan Start, TimeSpan End)> timeOfDayMapping = new Dictionary<string, (TimeSpan Start, TimeSpan End)>
        {
            { "morning", (Start: new TimeSpan(6, 0, 0), End: new TimeSpan(11, 59, 59)) },
            { "midday", (Start: new TimeSpan(12, 0, 0), End: new TimeSpan(17, 59, 59)) },
            { "evening", (Start: new TimeSpan(18, 0, 0), End: new TimeSpan(23, 59, 59)) },
            { "night", (Start: new TimeSpan(0, 0, 0), End: new TimeSpan(5, 59, 59)) }
        };

        var searchResults = flights.Where(flight =>
    !flight.HasTakenOff && // Exclude flights that have already taken off
    (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport.Contains(departureAirport, StringComparison.OrdinalIgnoreCase)) &&
    (string.IsNullOrEmpty(arrivalDestination) || flight.ArrivalDestination.Contains(arrivalDestination, StringComparison.OrdinalIgnoreCase)) &&
    (string.IsNullOrEmpty(departureDateString) || flight.DepartureDate.Contains(departureDateString)) &&
    (string.IsNullOrEmpty(timeOfDay) ||
        (timeOfDayMapping.TryGetValue(timeOfDay, out var timeRange) &&
        DateTime.TryParse(flight.FlightTime, out var flightTime) &&
        flightTime.TimeOfDay >= timeRange.Start && flightTime.TimeOfDay <= timeRange.End))
    &&
    (seatCount == 0 || flight.AvailableSeats >= seatCount)
    ).ToList();

        return searchResults;
    }
}