using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;
using DataAccess;

public static class BookFlightLogic
{
    // Method to search for a flight by its ID
    public static FlightModel SearchFlightByID(int id)
    {
        var allFlights = BookFlightPresentation.allFlights;
        return allFlights.FirstOrDefault(flight => flight.Id == id);
    }

    public static bool IsSeatAlreadyBooked(FlightModel flight, string seat)
    {
        return flight.Layout.BookedSeats.Contains(seat);
    }

    public static int GetAvailableSeatsCount(FlightModel flight)
    {
        return (flight.Layout.Rows * flight.Layout.Columns) - flight.Layout.BookedSeats.Count;
    }

    public static List<FlightModel> SearchFlights(string departureAirport, string arrivalDestination)
    {
        var availableFlights = BookFlightPresentation.allFlights
            .Where(flight => flight.DepartureAirport == departureAirport && flight.ArrivalDestination == arrivalDestination && !flight.IsCancelled)
            .ToList();

        return availableFlights;
    }

    // New method to load existing bookings including seat initials
    public static void LoadExistingBookings(FlightModel flight, string email)
    {
        var bookedFlights = BookedFlightsAccess.LoadByEmail(email);
        var existingBooking = bookedFlights.FirstOrDefault(b => b.FlightID == flight.Id);

        if (existingBooking != null)
        {
            // Restore booked seats
            foreach (var seat in existingBooking.BookedSeats)
            {
                if (!flight.Layout.BookedSeats.Contains(seat))
                {
                    flight.Layout.BookedSeats.Add(seat);
                }
            }

            // Restore seat initials
            if (existingBooking.SeatInitials != null)
            {
                foreach (var kvp in existingBooking.SeatInitials)
                {
                    flight.Layout.SeatInitials[kvp.Key] = kvp.Value;
                }
            }

            // Update available seats
            flight.Layout.AvailableSeats = flight.Layout.SeatArrangement
                .Where(s => !flight.Layout.BookedSeats.Contains(s))
                .ToList();
        }
    }

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
                false
            );

            bookedFlight.UpdateSeatInitials(flight.Layout.SeatInitials);
            bookedFlight.TicketBill = totalPrice;

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

    public static string GenerateSeatInitials(PassengerModel passenger)
    {
        if (string.IsNullOrWhiteSpace(passenger.FirstName) || string.IsNullOrWhiteSpace(passenger.LastName))
        {
            return null;
        }
        return $"{passenger.FirstName[0]}{passenger.LastName[0]}".ToUpper();
    }
}