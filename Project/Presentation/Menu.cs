using System;
using AirplaneSeatReservation.PresentationLayer; // Make sure to include your namespaces

static class Menu
{
    private static AirplaneLayoutUI airplaneLayout = new AirplaneLayoutUI();
    private static BookFlightLogic flightLogic = new BookFlightLogic(); // Add this line

    static public void Start()
    {
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to view available flights"); // Updated option
        Console.WriteLine("Enter 3 to view airplane layout");
        Console.WriteLine("Enter 4 to select a seat");
        Console.WriteLine("Enter 5 to exit");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                UserLogin.Start();
                Start(); // Show menu again after login
                break;
            case "2":
                ShowAvailableFlights(); // Call to show available flights
                Start(); // Show menu again after displaying flights
                break;
            case "3":
                airplaneLayout.DisplayLayout();
                Start(); // Show menu again after displaying the layout
                break;
            case "4":
                SelectSeat();
                Start(); // Show menu again after selecting a seat
                break;
            case "5":
                Console.WriteLine("Exiting...");
                return;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }
    }

    private static void ShowAvailableFlights()
    {
        var flights = flightLogic.GetAvailableFlights(); // Get the available flights
        if (flights.Count == 0)
        {
            Console.WriteLine("No available flights at the moment.");
            return;
        }

        Console.WriteLine("\nAvailable Flights:");
        for (int i = 0; i < flights.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {flights[i]}"); // Display each flight
        }

        Console.WriteLine("\nSelect a flight by entering the corresponding number to see the airplane layout:");
        if (int.TryParse(Console.ReadLine(), out int flightIndex) && flightIndex > 0 && flightIndex <= flights.Count)
        {
            // Assuming your FlightModel has a property to indicate which layout to show
            var selectedFlight = flights[flightIndex - 1]; 
            airplaneLayout.DisplayLayout(); // Display the layout of the selected flight
        }
        else
        {
            Console.WriteLine("Invalid flight selection.");
        }
    }

    private static void SelectSeat()
    {
        Console.WriteLine("\nSelect a seat by entering row number and seat letter (e.g., 12C):");
        string input = Console.ReadLine();
        if (int.TryParse(input.Substring(0, input.Length - 1), out int row) &&
            char.TryParse(input.Substring(input.Length - 1), out char seatLetter))
        {
            airplaneLayout.SelectSeat(row, seatLetter);
        }
        else
        {
            Console.WriteLine("Invalid input format.");
        }
    }
}
