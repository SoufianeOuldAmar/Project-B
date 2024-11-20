using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

public class BookedFlightsModel
{
    public int FlightID { get; set; }
    public List<string> BookedSeats { get; set; }
    public List<BaggageLogic> BaggageInfo{ get; set; } 
    public bool IsCancelled { get; set; }

    public BookedFlightsModel(int flightID, List<string> bookedSeats, List<BaggageLogic> baggageInfo, bool isCancelled)
    {
        FlightID = flightID;
        BookedSeats = bookedSeats;
        BaggageInfo = baggageInfo;
        IsCancelled = isCancelled;
    }

    // Total fee 
    public double FeeTotal()
    {
        double fee= 0;
        foreach (var bag in BaggageInfo)
        {
            fee += bag.CalcFee();
        }
        return fee;
    }


}