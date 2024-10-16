using System;
using System.Collections.Generic;

public class BookFlightLogic
{
    private List<FlightModel> flights;

    public BookFlightLogic()
    {
        LoadAllFlights();
    }

    private void LoadAllFlights()
    {
        FlightsAccess flightsAccess = new FlightAccess
        flights = flightsAccess.ReadAll();
    }

    public List<FlightModel> GetAvailableFlights()
    {
        return flights;
    }
}