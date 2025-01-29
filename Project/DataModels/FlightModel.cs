using System;
using System.Collections.Generic;

namespace DataModels
{
    public class FlightModel : IDataModel
    {
        public int Id { get; set; }
        public string Airline { get; } = "BOSST AIRLINES";

        public LayoutModel Layout { get; set; }
        public double TicketPrice { get; set; }
        public string Gate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalDestination { get; set; }
        public string? TimeOfDay { get; set; } // Optioneel gemaakt met nullable type
        public bool IsCancelled { get; set; }
        public string DepartureDate { get; set; }
        public string FlightTime { get; set; }
        public int AvailableSeats { get; set; }

        public int FlightPoints { get; set; }

        public int TotalPets { get; set; } = 0;

        public bool HasTakenOff { get; set; } = false;

        public FlightModel? ReturnFlight { get; set; }


        public FlightModel(LayoutModel layout, double ticketPrice, string gate,
        string departureAirport, string arrivalDestination, bool isCancelled,
        string departureDate, string flightTime, int availableSeats, string timeOfDay = null)
        {
            Layout = layout;
            TicketPrice = ticketPrice;
            Gate = gate;
            DepartureAirport = departureAirport;
            ArrivalDestination = arrivalDestination;
            TimeOfDay = timeOfDay;
            IsCancelled = isCancelled;
            DepartureDate = departureDate;
            FlightTime = flightTime;
            AvailableSeats = availableSeats;
            FlightPoints = Convert.ToInt32(TicketPrice) / 10;
        }

    }
}