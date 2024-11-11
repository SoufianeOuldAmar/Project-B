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
    }
}
