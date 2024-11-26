// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;

// [TestClass]
// public class BookFlightLogicTests
// {
//     private BookFlightLogic _bookFlightLogic;

//     [TestInitialize]
//     public void Setup()
//     {
//         _bookFlightLogic = new BookFlightLogic();
//     }

//     [TestMethod]
//     public void TestGetAvailableFlights()
//     {
//         // Act
//         var flights = _bookFlightLogic.GetAvailableFlights();

//         // Assert
//         Assert.IsNotNull(flights);
//         Assert.AreEqual(30, flights.Count); // Controleer of er 30 vluchten zijn gegenereerd
//     }

//     [TestMethod]
//     public void TestSelectFlight_ValidSelection()
//     {
//         // Arrange
//         var flights = _bookFlightLogic.GetAvailableFlights();
//         Assert.IsNotNull(flights);

//         // Act
//         var selectedFlight = flights[0]; // Selecteer de eerste vlucht

//         // Assert
//         Assert.IsNotNull(selectedFlight);
//         Assert.AreEqual("BOSST Airlines", selectedFlight.Airline);
//     }

//     // Verwijder de test voor het reserveren van een stoel, omdat deze functie niet bestaat

//     // Voeg eventueel andere tests toe die relevant zijn voor je vluchtlogica
// }