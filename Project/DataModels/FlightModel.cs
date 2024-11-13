using System;

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
        public string DepartureDate { get; set; }
        public string FlightTime { get; set; }

        public int AvailableSeats { get; set; }
        public int FlightPoints { get; set; }
        public FlightModel(string airline, LayoutModel layout, double ticketPrice, string gate, string departureAirport, string arrivalDestination, bool isCancelled, string departureDate, string flightTime, int availableSeats)

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
            AvailableSeats = availableSeats;
            FlightPoints = Convert.ToInt32(TicketPrice) / 10;
        }
    }

    // Constructor sort of values terug geven
}