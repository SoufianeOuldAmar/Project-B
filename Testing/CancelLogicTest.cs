using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataModels;
using System;

namespace Testing
{
    [TestClass]
    public class TestCancelLogic
    {
        // fake data 
        private static class MockFlightData
        {
            public static LayoutModel GetMockLayout()
            {

                List<string> seatArrangement = new List<string> { "01A", "01B", "02A", "02B", "03A", "03B" };
                return new LayoutModel(
                    rows: 3,
                    columns: 2,
                    seatArrangement: seatArrangement,
                    isAirbusA330: false,
                    isBoeing787: false
                );
            }

            // fake data with given id 
            public static FlightModel GetMockFlight(int id, bool isCancelled = false)
            {
                return new FlightModel(
                    id,
                    layout: GetMockLayout(),
                    ticketPrice: 200.0,
                    gate: "Gate A1",
                    departureAirport: "JFK",
                    arrivalDestination: "LAX",
                    isCancelled: isCancelled,
                    departureDate: "2025-01-15",
                    flightTime: "12:00",
                    availableSeats: 100,
                    timeOfDay: "Morning"
                );
            }

            public static List<FlightModel> GetMockFlights()
            {
                return new List<FlightModel>
                {
                    GetMockFlight(1), // Flight ID 1 is not cancelled
                    GetMockFlight(2, true) // Flight ID 2 is already cancelled
                };
            }

            public static Dictionary<string, List<BookedFlightsModel>> GetMockBookedFlights()
            {
                return new Dictionary<string, List<BookedFlightsModel>>
                {
                    {
                        "n@b.c",
                        new List<BookedFlightsModel>
                        {
                            new BookedFlightsModel { FlightID = 1, IsCancelled = false }, // not cancelled flight 
                            new BookedFlightsModel { FlightID = 2, IsCancelled = true }  // Already cancelled flight
                        }
                    }
                };
            }
        }


        [TestMethod]
        public void TestCancelFlights()
        {

            BookFlightLogic.allBookedFlights = MockFlightData.GetMockBookedFlights();

            var flightToCancel = BookFlightLogic.SearchBookedFlightByFlightID(1, "n@b.c");

            CancelLogic.CancelFlight("n@b.c", flightToCancel);

            Assert.IsTrue(flightToCancel.IsCancelled);
        }


        [TestMethod]
        public void TestCancelFlights_AlreadyCancelled()
        {

            BookFlightLogic.allBookedFlights = MockFlightData.GetMockBookedFlights();

            var bookedFlight = BookFlightLogic.SearchBookedFlightByFlightID(2, "n@b.c");

            CancelLogic.CancelFlight("n@b.c", bookedFlight);

            Assert.IsTrue(CancelLogic.IsBookedFlightCancelled(bookedFlight));
        }
    }
}