// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System.Linq;
// using DataModels;

// namespace Testing
// {
//     [TestClass]
//     public class TestRescheduleLogic
//     {
//         // fake data 
//         private static class MockFlightData
//         {
//             public static LayoutModel GetMockLayout()
//             {
//                 List<string> seatArrangement = new List<string> { "01A", "01B", "02A", "02B", "03A", "03B" };
//                 return new LayoutModel(
//                     rows: 3,
//                     columns: 2,
//                     seatArrangement: seatArrangement,
//                     isAirbusA330: false,
//                     isBoeing787: false
//                 );
//             }

//             public static FlightModel GetMockFlight(int id, bool isCancelled = false)
//             {
//                 return new FlightModel(
//                     layout: GetMockLayout(),
//                     ticketPrice: 200.0,
//                     gate: "Gate A1",
//                     departureAirport: "JFK",
//                     arrivalDestination: "LAX",
//                     isCancelled: isCancelled,
//                     departureDate: "2025-01-15",
//                     flightTime: "12:00",
//                     availableSeats: 100,
//                     timeOfDay: "Morning"
//                 )
//                 {
//                     Id = id
//                 };
//             }

//             public static List<FlightModel> GetMockFlights()
//             {
//                 return new List<FlightModel>
//                 {
//                     GetMockFlight(1), // Flight ID 1 (existing flight)
//                     GetMockFlight(2), // Flight ID 2 (new flight to reschedule to)
//                 };
//             }

//             public static Dictionary<string, List<BookedFlightsModel>> GetMockBookedFlights()
//             {
//                 return new Dictionary<string, List<BookedFlightsModel>>
//                 {
//                     {
//                         "n@b.c", // Email
//                         new List<BookedFlightsModel>
//                         {
//                             new BookedFlightsModel { FlightID = 1, IsCancelled = false } // Not cancelled flight
//                         }
//                     }
//                 };
//             }
//         }

//         [TestMethod]
//         public void TestRescheduleFlight_Success()
//         {
//             // Arrange
//             BookFlightPresentation.allBookedFlights = MockFlightData.GetMockBookedFlights();
//             BookFlightPresentation.allFlights = MockFlightData.GetMockFlights();

//             string expectedMessage = "Flight successfully rescheduled.";
//             string actualMessage = RescheduleLogic.RescheduleFlight("n@b.c", 1, 2); 

//             Assert.AreEqual(expectedMessage, actualMessage);
//         }



//     }
// }
































