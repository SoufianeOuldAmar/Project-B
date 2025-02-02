using System.Collections.Generic;
using DataModels;

public static class FlightLogic
{
    public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");

    public static FlightModel CreateReturnFlight(FlightModel originalFlight, string returnDate, string returnTime, string returnGate)
    {
        return new FlightModel(
            originalFlight.Layout,
            originalFlight.TicketPrice, // Same ticket price
            returnGate, // New gate for the return flight
            originalFlight.ArrivalDestination, // Swap the departure and arrival
            originalFlight.DepartureAirport,
            false, // Assuming the return flight isn't cancelled
            returnDate,
            returnTime,
            originalFlight.AvailableSeats // Assume same number of seats initially
        );
    }

    public static List<string> GetAllDestinations()
    {
        List<string> allDestinations = allFlights.Select(flight => flight.ArrivalDestination).Distinct().ToList();

        return allDestinations;
    }

    public static FlightModel SearchFlightByID(int id)
    {
        return allFlights.FirstOrDefault(flight => flight.Id == id);
    }

    public static int GetFlightIdByLayout(LayoutModel layout)
    {
        return allFlights.FirstOrDefault(f => f.Layout == layout)?.Id ?? 0;
    }

}