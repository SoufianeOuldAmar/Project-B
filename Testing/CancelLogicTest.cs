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
                )
                {
                    Id = id  
                };
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

            BookFlightPresentation.allBookedFlights = MockFlightData.GetMockBookedFlights();
            BookFlightPresentation.allFlights = MockFlightData.GetMockFlights();

            string result = CancelLogic.CancelFlights("n@b.c", 1); 

            Assert.AreEqual("Flight is cancelled", result);

            // Check if the flight cancellation status is updated
            Assert.IsTrue(BookFlightPresentation.allBookedFlights["n@b.c"][0].IsCancelled);
        }


        [TestMethod]
        public void TestCancelFlights_AlreadyCancelled()
        {

            BookFlightPresentation.allBookedFlights = MockFlightData.GetMockBookedFlights();
            BookFlightPresentation.allFlights = MockFlightData.GetMockFlights();

            string result = CancelLogic.CancelFlights("n@b.c", 2); 

            // Check if the correct error message is given
            Assert.AreEqual("You have already cancelled this flight", result);
        }
    }
}





