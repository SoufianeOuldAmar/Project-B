using System;
using System.Collections.Generic;

public class BookFlightLogic
{
    private List<FlightModel> flights;

    public BookFlightLogic()
    {
        LoadAllFlights();
    }

    // Laad alle vluchten in de constructor
    private void LoadAllFlights()
    {
        FlightsAccess flightsAccess = new FlightsAccess();
        flights = flightsAccess.ReadAll();
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

    // Laat de gebruiker een vlucht kiezen
    public FlightModel SelectFlight()
    {
        DisplayAvailableFlights();

        Console.WriteLine("Choose a flight number:");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int flightNumber) && flightNumber > 0 && flightNumber <= flights.Count)
        {
            return flights[flightNumber - 1];
        }
        else
        {
            Console.WriteLine("Invalid input. Please try again.");
            return SelectFlight(); // Opnieuw vragen om invoer als het fout gaat
        }
    }

    // Stoel kiezen en vlucht boeken
    public void BookFlight()
    {
        FlightModel selectedFlight = SelectFlight();

        if (selectedFlight.IsFull()) // Controleer of de vlucht vol is
        {
            Console.WriteLine("This flight is fully booked. Please try another flight.");
            return;
        }

        Console.WriteLine("Choose a seat number:");
        string seatChoice = Console.ReadLine();

        if (selectedFlight.BookSeat(seatChoice)) // Boek de stoel
        {
            Console.WriteLine("Your seat has been booked. Would you like to receive a ticket? (yes/no)");
            string ticketResponse = Console.ReadLine();

            if (ticketResponse.ToLower() == "yes")
            {
                PrintTicket(selectedFlight, seatChoice);
            }
            else
            {
                Console.WriteLine("Booking cancelled.");
            }
        }
        else
        {
            Console.WriteLine("Invalid seat. Please try again.");
            BookFlight(); // Opnieuw boeken als stoelkeuze fout is
        }
    }

    // Print het ticket van de gebruiker
    private void PrintTicket(FlightModel flight, string seat)
    {
        Console.WriteLine("\n--- Your Ticket ---");
        Console.WriteLine(flight);
        Console.WriteLine($"Seat: {seat}");
        Console.WriteLine("-----------------\n");
    }
}