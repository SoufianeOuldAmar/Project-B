using System.Collections.Generic;

public class FlightModel
{
    // Properties
    public string Airline { get; set; }
    public Layout Layout { get; set; }
    public decimal TicketPrice { get; set; } 
    public string Gate { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalDestination { get; set; } // Stad, Vliegveld
    public bool IsCancelled { get; set; }
    public List<string> AvailableSeats { get; set; }
    public string DepartureDate { get; set; }
    public string FlightTime { get; set; }

    // Constructor
    public FlightModel(string airline, Layout layout, decimal ticketPrice, string gate, string departureAirport, string arrivalDestination, bool isCancelled, string departureDate, string flightTime)
    {
        Airline = airline;
        Layout = layout;
        TicketPrice = ticketPrice;
        Gate = gate;
        DepartureAirport = departureAirport;
        ArrivalDestination = arrivalDestination; // Bijvoorbeeld: "Istanbul, Istanbul Airport"
        IsCancelled = isCancelled;
        DepartureDate = departureDate;
        FlightTime = flightTime;

        // Initialiseer de lijst met standaard beschikbare stoelen, nu uitgebreid met D, E en F
        AvailableSeats = new List<string>();
        for (int row = 1; row <= 10; row++) // Stel het aantal rijen in (bijv. 10)
        {
            AvailableSeats.Add($"{row}A");
            AvailableSeats.Add($"{row}B");
            AvailableSeats.Add($"{row}C");
            AvailableSeats.Add($"{row}D");
            AvailableSeats.Add($"{row}E");
            AvailableSeats.Add($"{row}F");
        }
    }

    // Methode om informatie van de vlucht weer te geven
    public override string ToString()
    {
        return $"Airline: {Airline}, Departure: {DepartureAirport}, Arrival: {ArrivalDestination}, " +
               $"Price: {TicketPrice:C}, Gate: {Gate}, Date: {DepartureDate}, Time: {FlightTime}, " +
               $"Cancelled: {IsCancelled}";
    }
}

// Mock Layout class
public class Layout
{
    public string LayoutType { get; set; }

    public Layout(string layoutType)
    {
        LayoutType = layoutType;
    }
}