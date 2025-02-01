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
        FlightModel newFlight = FlightLogic.SearchFlightByID(newFlightID);
        FlightModel oldFlight = FlightLogic.SearchFlightByID(oldFlightId);
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

                // Remove old seats from booked list and add back to available seats
                foreach (var seat in seats)
                {
                    if (oldFlight.Layout.BookedSeats.Contains(seat))
                    {
                        oldFlight.Layout.BookedSeats.Remove(seat);
                        oldFlight.Layout.AvailableSeats.Add(seat);
                    }
                }

                break;
            }
        }

        foreach (var seat in seats)
        {
            if (newFlight.Layout.BookedSeats.Contains(seat))
            {
                occupiedSeats.Add(seat);
            }
            else if (newFlight.Layout.AvailableSeats.Contains(seat)) // Check if seat is available
            {
                newFlight.Layout.AvailableSeats.Remove(seat); // Remove from available seats
                newFlight.Layout.BookedSeats.Add(seat); // Add to booked seats
            }
        }

        for (int i = 0; i < allFlights.Count; i++)
        {
            if (allFlights[i].Id == newFlight.Id)
            {
                allFlights[i] = newFlight;
            }
            if (allFlights[i].Id == oldFlight.Id)
            {
                allFlights[i] = oldFlight;
            }
        }

        DataAccessClass.WriteList<FlightModel>("DataSources/flights.json", allFlights);
        BookedFlightsAccess.WriteAll(MenuPresentation.currentAccount.EmailAddress, bookedFlight);

        return occupiedSeats;
    }

}