public class FlightModel
{
    public string Airline { get; set; }
    public Layout Layout { get; set; }
    public decimal TicketPrice { get; set; }
    public string Gate { get; set; }
    public string Airport { get; set; }
    public bool IsCancelled { get; set; }

    public FlightModel(string airline, Layout layout, decimal ticketPrice, string gate, string airport, bool isCancelled)
    {
        Airline = airline;
        Layout = layout;
        TicketPrice = ticketPrice;
        Gate = gate;
        Airport = airport;
        IsCancelled = isCancelled;
    }

    public override string ToString()
    {
        return $"Airline: {Airline}, Ticket Price: {TicketPrice:C}, Gate: {Gate}, Airport: {Airport}, Cancelled: {IsCancelled}";
    }
}