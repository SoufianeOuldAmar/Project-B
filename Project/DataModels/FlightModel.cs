using System.Collections.Generic;

public class FlightModel
{
    public string Airline { get; set; }
    public string Layout { get; set; }
    public decimal TicketPrice { get; set; }
    public string Gate { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public string ArrivalDestination { get; set; }
    public bool IsCancelled { get; set; }
    public List<string> AvailableSeats { get; set; } = new List<string>();
    public string DepartureDate { get; set; }
    public string FlightTime { get; set; }

    public FlightModel(string airline, string layout, decimal ticketPrice, string gate, string departureAirport, string arrivalAirport, string arrivalDestination, bool isCancelled, string departureDate, string flightTime)
    {
        Airline = airline;
        Layout = layout;
        TicketPrice = ticketPrice;
        Gate = gate;
        DepartureAirport = departureAirport;
        ArrivalAirport = arrivalAirport;
        ArrivalDestination = arrivalDestination;
        IsCancelled = isCancelled;
        DepartureDate = departureDate;
        FlightTime = flightTime;
        AvailableSeats = new List<string> { "1A", "1B", "1C", "2A", "2B", "2C" };
    }

    public bool IsFull()
    {
        return AvailableSeats.Count == 0;
    }

    public bool BookSeat(string seat)
    {
        if (AvailableSeats.Contains(seat))
        {
            AvailableSeats.Remove(seat);
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Airline: {Airline}, Ticket Price: {TicketPrice:C}, Gate: {Gate}, Departure Airport: {DepartureAirport}, " +
               $"Arrival Airport: {ArrivalAirport}, Arrival Destination: {ArrivalDestination}, Departure Date: {DepartureDate}, " +
               $"Flight Time: {FlightTime}, Cancelled: {IsCancelled}";
    }
}