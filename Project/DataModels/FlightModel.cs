using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataModels
{
    public class FlightModel
    {
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
        public string ArrivalDestination { get; set; }
        [JsonPropertyName("IsCancelled")]
        public bool IsCancelled { get; set; }
        [JsonPropertyName("DepartureDate")]
        public string DepartureDate { get; set; }
        [JsonPropertyName("FlightTime")]
        public string FlightTime { get; set; }

        public override string ToString()
        {
            return $"Airline: {Airline}, Departure: {DepartureAirport}, Arrival: {ArrivalDestination}, Price: {TicketPrice:C}, Gate: {Gate}, Date: {DepartureDate}, Time: {FlightTime}, Cancelled: {IsCancelled}";
        }
    }
}