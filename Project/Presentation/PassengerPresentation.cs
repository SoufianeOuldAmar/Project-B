using System;
using System.Collections.Generic;
using DataModels;
using BusinessLogic;
using DataAccess;

namespace PresentationLayer
{
    public static class PassengerPresentation
    {
        public static void CollectAndConfirmBooking(int numberOfPassengers, FlightModel flight, List<string> selectedSeats)
        {
            var passengers = PassengerLogic.CollectPassengerDetails(numberOfPassengers);

            Console.WriteLine("\n=== Booking Summary ===");
            Console.WriteLine($"Flight: {flight.Airline}, {flight.DepartureAirport} to {flight.ArrivalDestination}, {flight.DepartureDate}, {flight.FlightTime}");
            Console.WriteLine($"Selected Seats: {string.Join(", ", selectedSeats)}");

            Console.WriteLine("\nPassenger Details:");
            foreach (var passenger in passengers)
            {
                Console.WriteLine($"- {passenger.Title} {passenger.FirstName} {passenger.LastName} ({passenger.AgeGroup})");
                if (passenger.AgeGroup == "infant" && passenger.DateOfBirth.HasValue)
                {
                    Console.WriteLine($"  Date of Birth: {passenger.DateOfBirth.Value:yyyy-MM-dd}");
                }
            }

            Console.Write("\nConfirm booking? (yes/no): ");
            string confirmation = Console.ReadLine().ToLower();
            if (confirmation == "yes")
            {
                Console.WriteLine("Booking confirmed! Thank you.");
                PassengerAccess.SavePassengers(passengers);
                // Save booking logic here
            }
            else
            {
                Console.WriteLine("Booking canceled.");
            }
        }
    }
}
