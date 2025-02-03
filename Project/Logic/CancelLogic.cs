using System.Collections.Generic;
using DataModels;

public static class CancelLogic
{
    public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();
    public static void CancelFlight(string email, BookedFlightsModel bookedFlight)
    {
        bookedFlight.IsCancelled = true;
        BookedFlightsAccess.Save(email, bookedFlight);
    }

    public static bool CheckBookedFlights(string email)
    {
        return allBookedFlights.TryGetValue(email, out var bookedFlights);
    }

    public static double CalculateTotalCost(BookedFlightsModel bookedFlight, FlightModel neededFlight)
    {
        if (bookedFlight == null || neededFlight == null)
            throw new ArgumentNullException("Flight information is missing.");

        double totalPetFee = bookedFlight.Pets?.Sum(pet => pet.Fee) ?? 0;
        double totalBaggageFee = bookedFlight.BaggageInfo?.Sum(bag => bag.Fee) ?? 0;

        return neededFlight.TicketPrice + totalPetFee + totalBaggageFee;
    }


    public static bool IsBookedFlightCancelled(BookedFlightsModel bookedFlight)
    {
        return bookedFlight.IsCancelled;
    }
}