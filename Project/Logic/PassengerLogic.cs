using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;

namespace BusinessLogic
{
    public static class PassengerLogic
    {
        public static List<PassengerModel> CollectPassengerDetails(int numberOfPassengers)
        {
            var passengers = new List<PassengerModel>();

            for (int i = 0; i < numberOfPassengers; i++)
            {
                Console.WriteLine($"=== Enter details for Passenger {i + 1} ===");
                PassengerModel passenger = new PassengerModel();

                Console.Write("First Name: ");
                passenger.FirstName = Console.ReadLine();

                Console.Write("Last Name: ");
                passenger.LastName = Console.ReadLine();

                Console.Write("Title (Mr/Ms/Dr): ");
                passenger.Title = Console.ReadLine();

                Console.Write("Age Group (adult/child/infant): ");
                passenger.AgeGroup = Console.ReadLine().ToLower();

                if (passenger.AgeGroup == "infant")
                {
                    Console.Write("Date of Birth (yyyy-MM-dd): ");
                    string dobInput = Console.ReadLine();
                    if (DateTime.TryParse(dobInput, out DateTime dob))
                    {
                        passenger.DateOfBirth = dob;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please try again.");
                        i--; // Repeat for the same passenger
                        continue;
                    }
                }

                passengers.Add(passenger);
            }

            return passengers;
        }

        public static void ValidatePassengerDetails(List<PassengerModel> passengers)
        {
            foreach (var passenger in passengers)
            {
                if (string.IsNullOrWhiteSpace(passenger.FirstName) || string.IsNullOrWhiteSpace(passenger.LastName))
                {
                    throw new ArgumentException("First Name and Last Name cannot be empty.");
                }

                if (passenger.AgeGroup == "infant" && passenger.DateOfBirth == null)
                {
                    throw new ArgumentException("Infants must have a valid Date of Birth.");
                }
            }
        }
    }
}
