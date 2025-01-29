using DataModels;

public static class FlightLogic
{
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
}