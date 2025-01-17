using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;
using DataAccess;
using System.Threading;
using System.Globalization;

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


    public static void TakeOff()
    {
        var allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
        var allBookedFlights = BookedFlightsAccess.LoadAll();

        DateTime currentDateTime = DateTime.Now;
        var flightsTakenOff = new List<int>();

        // Mark flights as taken off and record their IDs
        foreach (var flight in allFlights)
        {
            // Combine DepartureDate and FlightTime into a DateTime
            string departureDateTimeString = flight.DepartureDate + " " + flight.FlightTime;

            try
            {
                DateTime departureDateTime = DateTime.ParseExact(
                    departureDateTimeString,
                    "dd-MM-yyyy HH:mm",
                    CultureInfo.InvariantCulture // Ensures culture-independent parsing
                );

                // Check if the flight's departure is in the past
                if (currentDateTime >= departureDateTime)
                {
                    flight.HasTakenOff = true;
                    flightsTakenOff.Add(flight.Id);
                }
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Error parsing the departure date and time: {departureDateTimeString}. Ensure it follows the format 'dd-MM-yyyy HH:mm'.", ex);
            }
        }

        // Track updated booked flights that need to be written back
        var updatedBookedFlights = new Dictionary<string, List<BookedFlightsModel>>();

        // Grant points for booked flights that have taken off
        foreach (var kvp in allBookedFlights)
        {
            string email = kvp.Key; // Access the key (email)
            List<BookedFlightsModel> bookedFlights = kvp.Value; // Access the value (list of booked flights)
            bool updated = false;
            var account = AccountsLogic.GetByEmail(email);

            // Process each booked flight for this email
            foreach (var bookedFlight in bookedFlights)
            {
                // Check if the booked flight is in the list of taken-off flights
                if (flightsTakenOff.Contains(bookedFlight.FlightID))
                {
                    // Find the corresponding FlightModel to get FlightPoints
                    var flight = allFlights.FirstOrDefault(f => f.Id == bookedFlight.FlightID);
                    if (flight != null)
                    {
                        // Only add points if they haven't been added before
                        if (bookedFlight.FlightPoints.Points == 0) // Check if no points were added yet
                        {
                            // var f = flight.FlightPoints * bookedFlight.BookedSeats.Count;
                            bookedFlight.FlightPoints.Points = flight.FlightPoints * bookedFlight.BookedSeats.Count;
                            bookedFlight.FlightPoints.Earned = true;
                            account.TotalFlightPoints += flight.FlightPoints;

                            DataAccessClass.UpdateCurrentAccount(account);

                            updated = true;
                        }
                    }
                }
            }

            // If any points were updated, add the email's booked flights to the updated dictionary
            if (updated)
            {
                updatedBookedFlights[email] = bookedFlights;
            }
        }

        // Save the updated flights and booked flights only once
        DataAccessClass.WriteList<FlightModel>("DataSources/flights.json", allFlights);


        // Write back only the emails that had updates
        foreach (var kvp in updatedBookedFlights)
        {
            BookedFlightsAccess.WriteAll(kvp.Key, kvp.Value);
        }

        flightsTakenOff.Clear();
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