using System.Collections.Generic;
using System.Text.Json.Serialization;

public class FlightModel
{
    // Properties
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("Airline")]
    public string Airline { get; set; }

    [JsonPropertyName("Layout")]
    public LayoutModel Layout { get; set; }

    [JsonPropertyName("TicketPrice")]
    public decimal TicketPrice { get; set; }

    [JsonPropertyName("Gate")]
    public string Gate { get; set; }

    [JsonPropertyName("DepartureAirport")]
    public string DepartureAirport { get; set; }

    [JsonPropertyName("ArrivalDestination")]
    public string ArrivalDestination { get; set; } // Stad, Vliegveld\

    [JsonPropertyName("IsCancelled")]
    public bool IsCancelled { get; set; }


    [JsonPropertyName("DepartureDate")]
    public string DepartureDate { get; set; }

    [JsonPropertyName("FlightTime")]
    public string FlightTime { get; set; }

    // Constructor
    public FlightModel(string airline, LayoutModel layout, decimal ticketPrice, string gate, string departureAirport, string arrivalDestination, bool isCancelled, string departureDate, string flightTime)

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
    }

    // Methode om informatie van de vlucht weer te geven
    public override string ToString()
    {
        return $"Airline: {Airline}, Departure: {DepartureAirport}, Arrival: {ArrivalDestination}, " +
               $"Price: {TicketPrice:C}, Gate: {Gate}, Date: {DepartureDate}, Time: {FlightTime}, " +
               $"Cancelled: {IsCancelled}";
    }
}