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
        public FlightModel? ReturnFlight { get; set; }

        public FlightModel(string airline, LayoutModel layout, double ticketPrice, string gate, string departureAirport, string arrivalDestination, bool isCancelled, string departureDate, string flightTime, int availableSeats)
        {
            Airline = airline;
            Layout = layout;
            TicketPrice = ticketPrice;
            Gate = gate;
            DepartureAirport = departureAirport;
            ArrivalDestination = arrivalDestination;
            IsCancelled = isCancelled;
            DepartureDate = departureDate;
            FlightTime = flightTime;
            AvailableSeats = availableSeats;
            FlightPoints = Convert.ToInt32(TicketPrice) / 10;
        }

        // Method to create a return flight based on the current flight
        public FlightModel CreateReturnFlight(string returnDate, string returnTime, string returnGate)
        {
            return new FlightModel(
                Airline,
                Layout,
                TicketPrice, // Same ticket price
                returnGate, // New gate for the return flight
                ArrivalDestination, // Swap the departure and arrival
                DepartureAirport,
                false, // Assuming the return flight isn't cancelled
                returnDate,
                returnTime,
                AvailableSeats // Assume same number of seats initially
            );
        }
    }


    // Constructor sort of values terug geven
}