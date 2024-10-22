// LayoutModel.cs
using System.Collections.Generic;

public class LayoutModel
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<string> SeatArrangement { get; set; }
    public List<string> AvailableSeats { get; set; }
    public List<string> BookedSeats { get; set; }
    public List<string> ChosenSeats { get; set; } // Track temporarily chosen seats

    public LayoutModel(int rows, int columns, List<string> seatArrangement)
    {
        Rows = rows;
        Columns = columns;
        SeatArrangement = seatArrangement;
        AvailableSeats = new List<string>(seatArrangement); // Initially, all seats are available
        BookedSeats = new List<string>();
        ChosenSeats = new List<string>(); // Initially, no seats are chosen
    }
}
