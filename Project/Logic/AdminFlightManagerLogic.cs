using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using DataModels;

namespace DataAccess
{

    public static class AdminFlightManagerLogic
    {
        public static void LaodFlight()
        {
            Console.Clear();
            var flights = FlightsAccess.ReadAll();
            foreach (var flight in flights)
            {

                // Console.WriteLine($"Flight ID: {flight.Id}, Airline: {flight.Airline}, TicketPrice: {flight.TicketPrice}, Gate: {flight.Gate}, DepartureAirport: {flight.DepartureAirport}, ArrivalDestination: {flight.ArrivalDestination}, IsCancelled: {flight.IsCancelled}, DepartureDate: {flight.DepartureDate}, FlightTime: {flight.FlightTime}, AvailableSeats: {flight.AvailableSeats}, FlightPoints: {flight.FlightPoints}");
                Console.WriteLine($"Flight ID: {flight.Id}");
                Console.WriteLine($"Airline: {flight.Airline}");
                Console.WriteLine($"Ticket Price: {flight.TicketPrice}");
                Console.WriteLine($"Gate: {flight.Gate}");
                Console.WriteLine($"Departure Airport: {flight.DepartureAirport}");
                Console.WriteLine($"Arrival Destination: {flight.ArrivalDestination}");
                Console.WriteLine($"Is Cancelled: {flight.IsCancelled}");
                Console.WriteLine($"Departure Date: {flight.DepartureDate}");
                Console.WriteLine($"Flight Time: {flight.FlightTime}");
                Console.WriteLine($"Available Seats: {flight.AvailableSeats}");
                Console.WriteLine($"Flight Points: {flight.FlightPoints}");
                Console.WriteLine("-----------------------------------------");
            }
        }
        // public static List<FlightModel> allFlights = FlightsAccess.ReadAll();
        public static FlightModel SearchFlightLogic(int id)
        {
            var Flight = FlightsAccess.ReadAll();
            return Flight.FirstOrDefault(flight => flight.Id == id);
        }

    }
}