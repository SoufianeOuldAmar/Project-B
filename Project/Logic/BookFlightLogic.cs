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
            false
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

}
