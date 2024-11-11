namespace DataModels
{
    public class FlightModel
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public LayoutModel Layout { get; set; }
        public double TicketPrice { get; set; }
        public string Gate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalDestination { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime DepartureDate { get; set; }
        public string FlightTime { get; set; }

        public int FlightPoints { get; set; }

        // Constructor sort of values terug geven
    public FlightModel(string airline, LayoutModel layout, double ticketPrice, string gate, string departureAirport, string arrivalDestination, bool isCancelled, DateTime departureDate, string flightTime)

    {
        Airline = airline;
        Layout = layout;
        TicketPrice = ticketPrice;
        Gate = gate;
        DepartureAirport = departureAirport;
        ArrivalDestination = arrivalDestination; // Bijvoorbeeld: "Istanbul, Istanbul Airport"
        IsCancelled = isCancelled;
        DepartureDate = departureDate;
        FlightTime = flightTime;
        FlightPoints = Convert.ToInt32(TicketPrice / 10);
    }
    }
}
