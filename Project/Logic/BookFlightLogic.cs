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
}
