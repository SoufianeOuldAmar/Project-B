using System;
using System.Text.Json.Serialization;

public class FlightPoint
{

    [JsonPropertyName("ticketsBought")]
    public DateTime TicketsBought { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("flightId")]
    public int FlightId { get; set; }


    public FlightPoint(DateTime ticketsBought, int points, int flightId)
    {
        TicketsBought = ticketsBought;
        Points = points;
        FlightId = flightId;
    }
}