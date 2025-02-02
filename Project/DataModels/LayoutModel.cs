using DataAccess;
using PresentationLayer;
using DataModels;

public class LayoutModel
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<string> SeatArrangement { get; set; }
    public List<string> AvailableSeats { get; set; }
    public List<string> BookedSeats { get; set; }
    public List<string> ChosenSeats { get; set; }
    public bool IsAirbusA330 { get; set; }
    public bool IsBoeing787 { get; set; }
    public Dictionary<string, string> SeatInitials { get; set; } = new Dictionary<string, string>();

    public LayoutModel(int rows, int columns, List<string> seatArrangement, bool isAirbusA330 = false, bool isBoeing787 = false)
    {
        Rows = rows;
        Columns = columns;
        SeatArrangement = seatArrangement;
        AvailableSeats = new List<string>(seatArrangement);
        BookedSeats = new List<string>();
        ChosenSeats = new List<string>();
        IsAirbusA330 = isAirbusA330;
        IsBoeing787 = isBoeing787;
        SeatInitials = new Dictionary<string, string>();
    }
}