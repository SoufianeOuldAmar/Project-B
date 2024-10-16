public class Seat
{
    public int Row { get; set; }
    public char SeatLetter { get; set; }
    public string Class { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    public Seat(int row, char seatLetter)
    {
        Row = row;
        SeatLetter = seatLetter;
        IsAvailable = true;
    }
}