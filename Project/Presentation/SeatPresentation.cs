public class SeatPresentation
{
    public string SeatNumber { get; set; }
    public bool IsBooked { get; set; }

    // Constructor
    public SeatPresentation(string seatNumber)
    {
        SeatNumber = seatNumber;
        IsBooked = false; // Stoel is niet geboekt bij het aanmaken
    }

    // Functie om stoel te boeken
    public bool BookSeat()
    {
        if (!IsBooked)
        {
            IsBooked = true;
            return true;
        }
        return false; // Stoel is al geboekt
    }
}
