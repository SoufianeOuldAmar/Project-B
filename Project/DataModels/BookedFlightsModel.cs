using System.Collections.Generic;
using System.Runtime.InteropServices;

public class BookedFlightsModel
{
    public int FlightID { get; set; }
    public List<string> BookedSeats { get; set; }
    public bool IsCancelled { get; set; }

    public BookedFlightsModel(int flightID, List<string> bookedSeats, bool isCancelled)
    {
        FlightID = flightID;
        BookedSeats = bookedSeats;
        IsCancelled = isCancelled;
    }

}