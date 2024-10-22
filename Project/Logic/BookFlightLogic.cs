public class BookFlightLogic
{
    private List<FlightModel> flights;
    private FlightsAccess flightsAccess;

    public BookFlightLogic()
    {
        flightsAccess = new FlightsAccess();
        flights = flightsAccess.ReadAll(); // Laad vluchten uit JSON bij initialisatie
        if (flights.Count == 0)
        {
            Console.WriteLine("No flights available in the JSON file.");
        }
    }

    public List<FlightModel> GetAvailableFlights()
    {
        return flights;
    }

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

    public void SaveFlightsToJson()
    {
        flightsAccess.WriteAll(flights); // Sla de vluchten op via FlightsAccess
    }

    public void BookFlight(int flightIndex)
    {
        if (flightIndex < 1 || flightIndex > flights.Count)
        {
            Console.WriteLine("Invalid flight selection.");
            return;
        }

        // Logica om de vlucht te boeken (bijvoorbeeld markeren als geboekt)
        FlightModel selectedFlight = flights[flightIndex - 1];
        Console.WriteLine($"Flight {selectedFlight.FlightNumber} has been booked.");
        SaveFlightsToJson(); // Sla wijzigingen op
    }
}