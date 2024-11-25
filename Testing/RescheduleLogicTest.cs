using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Testing;

namespace RescheduleLogicTest
{
    [TestClass]
    public class RescheduleLogicTests
    {
        private List<FlightModel> Flights;
        private Dictionary<string, List<BookedFlightsModel> BookedFlights;

        [TestInitialize]
        public void SetUp()
        {
            BookedFlights= = new List<FlightModel>
            {
                new FlightModel { Id = 1, Airline = "Airline1", DepartureAirport = "Airport1", ArrivalDestination = "Destination1", TicketPrice = 100, AvailableSeats = 10 },
                new FlightModel { Id = 2, Airline = "Airline2", DepartureAirport = "Airport1", ArrivalDestination = "Destination1", TicketPrice = 200, AvailableSeats = 5},
            };

        }





    }






















}





