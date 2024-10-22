public class FlightModel
{
    public string FlightNumber { get; set; }
    public string Airline { get; set; }
    public decimal TicketPrice { get; set; }
    public string Gate { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalDestination { get; set; }
    public string DepartureDate { get; set; }
    public string FlightTime { get; set; }
    public object Layout { get; set; }

    // Constructor
    public FlightModel(string flightNumber, string airline, decimal ticketPrice, string gate,
                       string departureAirport, string arrivalDestination,
                       string departureDate, string flightTime, object layout = null)
    {
        FlightNumber = flightNumber;
        Airline = airline;
        TicketPrice = ticketPrice;
        Gate = gate;
        DepartureAirport = departureAirport;
        ArrivalDestination = arrivalDestination;
        DepartureDate = departureDate;
        FlightTime = flightTime;
        Layout = layout;
    }

    // ToString
    public override string ToString()
    {
        return $"{FlightNumber} - {Airline}: {DepartureAirport} to {ArrivalDestination} on {DepartureDate} at {FlightTime} (Gate: {Gate})";
    }
}