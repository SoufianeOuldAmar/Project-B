using System;
using System.Collections.Generic;
using System.Linq;

// using DataModels; // FlightModel, LayoutModel
// using DataAccess; // FlightsAccess


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
        return BookFlightPresentation.allFlights
            .Where(flight => flight.DepartureAirport == departureAirport && flight.ArrivalDestination == arrivalDestination && !flight.IsCancelled)
            .ToList();
    }
}
