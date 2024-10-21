using System;
using System.Collections.Generic;

public class BookFlightLogic
{
    private List<FlightModel> flights;

    public BookFlightLogic()
    {
        LoadAllFlights();
    }

    // Laad alle vluchten (hier aanpassen om willekeurige vluchten te genereren)
    private void LoadAllFlights()
    {
        flights = GenerateRandomFlights(30); // Genereer 30 willekeurige vluchten bij elke start
    }

    // Methode om willekeurige vluchten te genereren
    private List<FlightModel> GenerateRandomFlights(int numberOfFlights)
    {
        List<FlightModel> randomFlights = new List<FlightModel>();
        Random random = new Random();
        string[] europeanAirports = {
            "Schiphol Airport",
            "Charles de Gaulle Airport",
            "Heathrow Airport",
            "Frankfurt Airport",
            "Brussels Airport",
            "Istanbul Airport",
            "Warsaw Chopin Airport",
            "Budapest Ferenc Liszt International Airport",
            "Barcelona-El Prat Airport",
            "Riga International Airport",
            "Athens International Airport",
            "Lisbon Airport"
        };

        string[] europeanDestinations = {
            "Amsterdam",
            "Paris",
            "London",
            "Frankfurt",
            "Brussels",
            "Istanbul",
            "Warsaw",
            "Budapest",
            "Barcelona",
            "Riga",
            "Athens",
            "Lisbon"
        };
        
        for (int i = 0; i < numberOfFlights; i++)
        {
            string arrivalAirport = europeanAirports[random.Next(europeanAirports.Length)];
            string arrivalDestination = europeanDestinations[random.Next(europeanDestinations.Length)];
            decimal ticketPrice = random.Next(100, 500); // Willekeurige prijs tussen 100 en 500
            string gate = "Gate " + random.Next(1, 10); // Willekeurige gate tussen 1 en 10
            string departureDate = GenerateRandomDate(); // Willekeurige datum genereren
            string flightTime = GenerateRandomFlightTime(); // Willekeurige vluchtduur genereren

            FlightModel flight = new FlightModel(
                "BOSST Airlines", 
                null, 
                ticketPrice, 
                gate, 
                "Rotterdam The Hague Airport", 
                arrivalAirport, 
                arrivalDestination, 
                false, 
                departureDate, 
                flightTime);

            randomFlights.Add(flight);
        }

        return randomFlights;
    }

    // Methode om willekeurige datum te genereren
    private string GenerateRandomDate()
    {
        DateTime start = new DateTime(2024, 10, 29);
        DateTime end = new DateTime(2025, 4, 24);
        int range = (end - start).Days;
        return start.AddDays(new Random().Next(range)).ToString("dd-MM-yyyy");
    }

    // Methode om willekeurige vluchtduur te genereren
    private string GenerateRandomFlightTime()
    {
        Random random = new Random();
        int departureHour = random.Next(6, 22); // Willekeurige vertrektijd tussen 6 en 22 uur
        int flightDuration = random.Next(1, 5); // Vluchtduur tussen 1 en 5 uur
        int arrivalHour = departureHour + flightDuration;

        return $"{departureHour:00}:00-{arrivalHour:00}:00";
    }

    // Methode om beschikbare vluchten terug te geven
    public List<FlightModel> GetAvailableFlights()
    {
        return flights;
    }

    // Toon alle beschikbare vluchten
    public void DisplayAvailableFlights()
    {
        if (flights.Count == 0)
        {
            Console.WriteLine("There are no available flights.");
            return;
        }

        Console.WriteLine("Available flights:");
        for (int i = 0; i < flights.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {flights[i]}");
        }
    }
}