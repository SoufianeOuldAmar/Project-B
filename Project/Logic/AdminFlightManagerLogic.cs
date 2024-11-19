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
            var flights = FlightsAccess.ReadAll();
            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight ID: {flight.Id}, Airline: {flight.Airline}, TicketPrice: {flight.TicketPrice}, Gate: {flight.Gate}, DepartureAirport: {flight.DepartureAirport}, ArrivalDestination: {flight.ArrivalDestination}, IsCancelled: {flight.IsCancelled}, DepartureDate: {flight.DepartureDate}, FlightTime: {flight.FlightTime}, AvailableSeats: {flight.AvailableSeats}, FlightPoints: {flight.FlightPoints}");
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