using System.Collections.Generic;

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
}