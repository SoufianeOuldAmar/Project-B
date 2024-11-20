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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Flight ID: {flight.Id}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Airline: {flight.Airline}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Ticket Price: {flight.TicketPrice}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Gate: {flight.Gate}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Departure Airport: {flight.DepartureAirport}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"Arrival Destination: {flight.ArrivalDestination}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Is Cancelled: {flight.IsCancelled}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Departure Date: {flight.DepartureDate}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Flight Time: {flight.FlightTime}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"Available Seats: {flight.AvailableSeats}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Flight Points: {flight.FlightPoints}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("-----------------------------------------");
                Console.ResetColor();
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