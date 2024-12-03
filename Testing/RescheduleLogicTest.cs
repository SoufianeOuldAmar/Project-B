using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Testing;
using DataModels;
using DataAccess;

namespace RescheduleLogicTest
{
    [TestClass]
    public class RescheduleLogicTests
    {

        [TestInitialize]
        public void SetUp()
        {
            LayoutModel LayoutSimple = new LayoutModel(10, 6, new List<string> { "01A", "01B", "01C", "01D", "01E", "01F"});

            var flightsToTest= new List<FlightModel>
            {
                new FlightModel(LayoutSimple, 200, "GateA", "Airport1", "Destination1", false, "2024-12-02", "11:00 AM", 6, 0) { Id = 1},
                new FlightModel(LayoutSimple, 200, "GateB", "Airport2", "Destination2", false, "2025-12-02", "12:00 AM", 5, 0) { Id = 2},
           
            };


            //Minimal Mock Booked Flights 
            List<string> bookedSeats = new List<string> { "01A" }; 
            List<BaggageLogic> baggageInfo = new List<BaggageLogic> { new BaggageLogic("AA", "Carry-On", 10) }; 
            List<PetLogic> pets = new List<PetLogic> { new PetLogic("Cat")};

            // data to test 
            BookFlightPresentation.allFlights = flightsToTest; 
            BookFlightPresentation.allBookedFlights = new Dictionary<string, List<BookedFlightsModel>> 
            { 
            
                {
                    "emaildoesntexist@gmail.com", 
                    new List<BookedFlightsModel>
                    {
                        new BookedFlightsModel (1, bookedSeats, baggageInfo, pets, false), 
                        new BookedFlightsModel (2, bookedSeats, baggageInfo, pets, false)
                    }

                }
              
                
            };

        }

        [TestMethod]
        public void TestReschedulingFlight_ReschedulingNotSucessfull()
        {
            string email = "emaildoesntexist@gmail.com";
            int bookedFlightID = 2;
            int rescheduleToId= 1;

            string result = RescheduleLogic.RescheduleFlight(email, bookedFlightID, rescheduleToId);
            Assert.IsFalse(result.Contains("no flight with id"), result );

        }

        [TestMethod]
        public void TestReschedulingFlight_InvalidIdProvided()
        {
            string email = "emaildoesntexist@gmail.com";
            int bookedFlightID = 2200;
            int rescheduleToId= 2;

            string result = RescheduleLogic.RescheduleFlight(email, bookedFlightID, rescheduleToId);
            Assert.IsTrue(result.Contains($"No booked flights with this ID: {bookedFlightID}"), result);



        }







    }






















}





