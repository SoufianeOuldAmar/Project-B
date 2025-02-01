using System.Collections.Generic;
using DataModels;

public static class RescheduleLogic
{
    public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
    public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = DataManagerLogic.LoadAll();

    public static List<FlightModel> GetEligibleFlights(int FlightID)
    {
        var bookedFlight = allBookedFlights[MenuPresentation.currentAccount.EmailAddress];

        // Find the specific booked flight by its ID
        var specificBookedFlight = bookedFlight.FirstOrDefault(bf => bf.FlightID == FlightID);
        if (specificBookedFlight == null)
        {
            return new List<FlightModel>(); // Return empty if not found
        }

        var currentFlight = FlightLogic.SearchFlightByID(FlightID);
        if (currentFlight == null)
        {
            return new List<FlightModel>(); // Return empty if flight not found
        }

        string currentDepartureAirport = currentFlight.DepartureAirport;
        string currentArrivalDestination = currentFlight.ArrivalDestination;
        string currentDepartureDate = currentFlight.DepartureDate;

        var eligibleFlights = allFlights.Where(f =>
            f.Id != FlightID &&
            f.DepartureAirport == currentDepartureAirport &&
            f.ArrivalDestination == currentArrivalDestination &&
            !f.IsCancelled &&
            !f.HasTakenOff
        ).ToList();

        return eligibleFlights;
    }

    public static void RescheduleFlight(int newFlightID, int oldFlightId)
    {
        var bookedFlight = allBookedFlights[MenuPresentation.currentAccount.EmailAddress];
        int index = bookedFlight.FindIndex(flight => flight.FlightID == oldFlightId);

        if (index != -1) // If found
        {
            bookedFlight[index].FlightID = newFlightID;
        }
    }

    public static List<string> AreFormerSeatsTaken(int newFlightID, int oldFlightId)
    {
        var bookedFlight = allBookedFlights[MenuPresentation.currentAccount.EmailAddress];
        FlightModel flight = FlightLogic.SearchFlightByID(newFlightID);
        List<string> seats = new List<string>();
        List<string> occupiedSeats = new List<string>();

        foreach (var bf in bookedFlight)
        {
            if (bf.FlightID == oldFlightId)
            {
                bf.FlightID = newFlightID;
                foreach (var seat in bf.BookedSeats)
                {
                    seats.Add(seat);
                }

                break;
            }
        }

        foreach (var seat in seats)
        {
            if (flight.Layout.BookedSeats.Contains(seat))
            {
                occupiedSeats.Add(seat);
            }
            else if (flight.Layout.AvailableSeats.Contains(seat)) // Check if seat is available
            {
                flight.Layout.AvailableSeats.Remove(seat); // Remove from available seats
                flight.Layout.BookedSeats.Add(seat); // Add to booked seats
            }
        }

        for (int i = 0; i < allFlights.Count; i++)
        {
            if (allFlights[i].Id == flight.Id)
            {
                allFlights[i] = flight;
            }
        }

        DataAccessClass.WriteList<FlightModel>("DataSources/flights.json", allFlights);
        BookedFlightsAccess.WriteAll(MenuPresentation.currentAccount.EmailAddress, bookedFlight);

        return occupiedSeats;

    }
}