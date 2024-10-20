using System.Collections.Generic;

public class FlightModel
{
    public string Airline { get; set; }
    public Layout Layout { get; set; }
    public decimal TicketPrice { get; set; }
    public string Gate { get; set; }
    public string Airport { get; set; }
    public bool IsCancelled { get; set; }
    public List<string> AvailableSeats { get; set; } = new List<string>(); // Beschikbare stoelen

    public FlightModel(string airline, Layout layout, decimal ticketPrice, string gate, string airport, bool isCancelled)
    {
        Airline = airline;
        Layout = layout;
        TicketPrice = ticketPrice;
        Gate = gate;
        Airport = airport;
        IsCancelled = isCancelled;

    // Voeg standaard stoelen toe aan de beschikbare stoelen
        AvailableSeats = new List<string> { "1A", "1B", "1C", "2A", "2B", "2C" };
    }

    // Controleer of de vlucht volgeboekt is
    public bool IsFull()
    {
        return AvailableSeats.Count == 0;
    }

    // Boek een stoel en verwijder deze uit de lijst van beschikbare stoelen
    public bool BookSeat(string seat)
    {
        if (AvailableSeats.Contains(seat))
        {
            AvailableSeats.Remove(seat); // Stoel wordt geboekt en verwijdert van beschikbare stoelen lijst
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Airline: {Airline}, Ticket Price: {TicketPrice:C}, Gate: {Gate}, Airport: {Airport}, Cancelled: {IsCancelled}";
    }
}