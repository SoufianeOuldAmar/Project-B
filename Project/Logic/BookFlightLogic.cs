using System;
using System.Collections.Generic;
using System.Linq;
using DataModels; // FlightModel, LayoutModel
using DataAccess; // FlightsAccess

public static class BookFlightLogic
{
    // Method to search for a flight by its ID
    public static FlightModel SearchFlightByID(int id)
    {
        // Retrieve all flights
        var allFlights = BookFlightPresentation.allFlights;

        // Return the flight that matches the given ID, or null if not found
        return allFlights.FirstOrDefault(flight => flight.Id == id);
    }

    public static bool IsSeatAlreadyBooked(FlightModel flight, string seat)
    {
        return flight.Layout.BookedSeats.Contains(seat);
    }

    // Method om te berekenen hoeveel beschikbare seats er nog zijn
    public static int GetAvailableSeatsCount(FlightModel flight)
    {
        return (flight.Layout.Rows * flight.Layout.Columns) - flight.Layout.BookedSeats.Count;
    }

    public static List<FlightModel> SearchFlights(string departureAirport, string arrivalDestination)
    {
        // Return all flights that match the departure and arrival airport
        var availableFlights = BookFlightPresentation.allFlights
            .Where(flight => flight.DepartureAirport == departureAirport && flight.ArrivalDestination == arrivalDestination && !flight.IsCancelled)
            .ToList();

        return availableFlights;
    }

    // New Method to save booking details
    public static bool SaveBooking(FlightModel flight, List<string> selectedSeats, List<PassengerModel> passengers, List<BaggageLogic> baggageInfo, List<PetLogic> petInfo, double totalPrice)
    {
        try
        {
            PassengerAccess.SavePassengers(passengers);

            var currentAccount = AccountsLogic.CurrentAccount;

            var bookedFlight = new BookedFlightsModel(
                flight.Id,
                selectedSeats,
                baggageInfo,
                petInfo,
                false,
                currentAccount.EmailAddress
            );

            if (!BookFlightPresentation.allBookedFlights.ContainsKey(currentAccount.EmailAddress))
            {
                BookFlightPresentation.allBookedFlights[currentAccount.EmailAddress] = new List<BookedFlightsModel>();
            }
            BookFlightPresentation.allBookedFlights[currentAccount.EmailAddress].Add(bookedFlight);

            BookedFlightsAccess.WriteAll(currentAccount.EmailAddress, BookFlightPresentation.allBookedFlights[currentAccount.EmailAddress]);

            flight.AvailableSeats -= selectedSeats.Count;
            FlightsAccess.WriteAll(BookFlightPresentation.allFlights);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving booking: {ex.Message}");
            return false;
        }
    }

    public static void TakeOff()
    {
        // var allFlights = FlightsAccess.ReadAll();
        // var allBookedFlights = BookedFlightsAccess.LoadAll();

        // DateTime currentDateTime = DateTime.Now;
        // var flightsTakenOff = new List<int>();

        // // Mark flights as taken off and record their IDs
        // foreach (var flight in allFlights)
        // {
        //     // Combine DepartureDate and FlightTime into a DateTime
        //     DateTime departureDateTime = DateTime.Parse(flight.DepartureDate + " " + flight.FlightTime);

        //     // Check if the flight's departure is in the past
        //     if (currentDateTime >= departureDateTime)
        //     {
        //         flight.HasTakenOff = true;
        //         flightsTakenOff.Add(flight.Id);
        //     }
        // }

        // // Track updated booked flights that need to be written back
        // var updatedBookedFlights = new Dictionary<string, List<BookedFlightsModel>>();

        // // Grant points for booked flights that have taken off
        // foreach (var kvp in allBookedFlights)
        // {
        //     string email = kvp.Key; // Access the key (email)
        //     List<BookedFlightsModel> bookedFlights = kvp.Value; // Access the value (list of booked flights)
        //     bool updated = false;

        //     // Process each booked flight for this email
        //     foreach (var bookedFlight in bookedFlights)
        //     {
        //         // Check if the booked flight is in the list of taken-off flights
        //         if (flightsTakenOff.Contains(bookedFlight.FlightID))
        //         {
        //             // Find the corresponding FlightModel to get FlightPoints
        //             var flight = allFlights.FirstOrDefault(f => f.Id == bookedFlight.FlightID);
        //             if (flight != null)
        //             {
        //                 // Only add points if they haven't been added before
        //                 if (bookedFlight.FlightPoints == 0) // Check if no points were added yet
        //                 {
        //                     bookedFlight.FlightPoints += flight.FlightPoints * bookedFlight.BookedSeats.Count;
        //                     updated = true;
        //                 }
        //             }
        //         }
        //     }

        //     // If any points were updated, add the email's booked flights to the updated dictionary
        //     if (updated)
        //     {
        //         updatedBookedFlights[email] = bookedFlights;
        //     }
        // }

        // // Save the updated flights and booked flights only once
        // FlightsAccess.WriteAll(allFlights);

        // // Write back only the emails that had updates
        // foreach (var kvp in updatedBookedFlights)
        // {
        //     BookedFlightsAccess.WriteAll(kvp.Key, kvp.Value);
        // }

        // flightsTakenOff.Clear();
    }


    public static void RemoveDuplicateSeats(BookedFlightsModel bookedFlight)
    {
        // Using LINQ to remove duplicates
        bookedFlight.BookedSeats = bookedFlight.BookedSeats.Distinct().ToList();

        // OR Using a for loop (if you prefer that method)
        for (int i = 0; i < bookedFlight.BookedSeats.Count; i++)
        {
            for (int j = i + 1; j < bookedFlight.BookedSeats.Count; j++)
            {
                if (bookedFlight.BookedSeats[i] == bookedFlight.BookedSeats[j])
                {
                    bookedFlight.BookedSeats.RemoveAt(j);
                    j--;
                }
            }
        }

    }

}
