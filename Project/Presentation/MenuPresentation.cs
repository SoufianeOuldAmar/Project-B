using DataModels;
using DataAccess;

public static class MenuPresentation
{
    public static Stack<Action> menuStack = new Stack<Action>(); // Een stack voor functies

    public static void Start()
    {
        // Start with the main menu
        menuStack.Push(AuthenticateAccountMenu); // Voeg AuthenticateAccountMenu() toe aan de lege stack

        while (menuStack.Count > 0) // terwijl de stack nog functies bevat, blijft ie doorgaan. Zo niet dan stopt de loop en dus het programma.
        {
            Action currentMenu = menuStack.Peek();
            Console.Clear();
            currentMenu.Invoke();

            Console.Write("\nPress any key to continue... ");
            Console.ReadKey();
        }
    }

    public static void AuthenticateAccountMenu() // Begin menu wanneer het programma opstart.
    {

        string message = "Welcome to BOSST Airlines";
        string bigText = Figgle.FiggleFonts.Standard.Render(message);
        Random rand = new Random();

        foreach (char c in bigText)
        {
            Console.ForegroundColor = (ConsoleColor)rand.Next(1, 14);
            Console.Write(c);
            Thread.Sleep(2);
        }
        for (int i = 0; i < 7; i++)
        {
            Console.Clear();
            foreach (char c in bigText)
            {
                Console.ForegroundColor = (ConsoleColor)rand.Next(1, 14);
                Console.Write(c);
            }
            Thread.Sleep(300);
            // Thread.Sleep(200);
            // Console.ForegroundColor = (ConsoleColor)rand.Next(1, 14);
            // Console.WriteLine(bigText);
            // Thread.Sleep(200);
        }
        Console.ResetColor();

        Console.WriteLine("=== Main Menu ===");
        Console.WriteLine("1. Log in");
        Console.WriteLine("2. Create account");
        Console.WriteLine("3. Quit program\n");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Push the submenu onto the stack
                MenuLogic.PushMenu(LogInMenu);
                break;
            case "2":
                MenuLogic.PushMenu(CreateAccountMenu);
                break;
            case "3":
                // Exit by popping the main menu
                MenuLogic.PopMenu();
                Console.WriteLine("Until next time!");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    public static void LogInMenu()
    {
        AccountPresentation.LogIn();
    }


    public static void CreateAccountMenu()
    {
        AccountPresentation.CreateAccount();
    }



    public static void BookFlightMenu()
    {
        BookFlightPresentation.BookFlightMenu();
    }

    public static void CancelMain(string email)
    {
        CancelPres.CancelMain(email);
    }

    public static void RescheduleLogic()
    {
        RescheduleLogic();
    }

    public static void FrontPageUser(AccountModel accountModel)
    {
        Console.WriteLine(accountModel.EmailAddress);
        Console.WriteLine($"Logged in as: {accountModel.FullName}\n");
        Console.WriteLine("=== Front page ===");
        Console.WriteLine("1. Order a ticket");
        Console.WriteLine("2. View history of tickets");
        Console.WriteLine("3. Search for flights");
        Console.WriteLine("4. Log out");
        Console.Write("\nChoose an option: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                MenuLogic.PushMenu(BookFlightMenu);
                break;
            case "2":
                MenuLogic.PushMenu(() => CancelMain(accountModel.EmailAddress));
                break;
            case "3":
                Console.WriteLine("\nLogging out...");
                // AccountsLogic.LogOut();
                MenuLogic.PopMenu();
                MenuLogic.PopMenu();
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    public static void FrontPageAdmin(AccountModel accountModel)
    {
        Console.WriteLine($"Logged in as admin: {accountModel.FullName}\n");
        Console.WriteLine("=== Front page ===");
        Console.WriteLine("1. Modify flights");
        Console.WriteLine("2. Log out");
        Console.Write("\nChoose an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // MenuLogic.PushMenu(OrderTicketMenu);
                Console.WriteLine("\nThis feature isn't available yet.");
                break;
            case "2":
                MenuLogic.PopMenu();
                MenuLogic.PopMenu();
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
    public static List<FlightModel> bookedFlights = new List<FlightModel>(); // Lijst van geboekte vluchten
    public static List<FlightModel> flights = FlightsAccess.ReadAll(); // Dit zou je vluchtdata kunnen laden van een bestand of database

    public static void SearchFlightsMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Search Flights ===");

        // Vraag de gebruiker om zoekcriteria in te vullen
        Console.Write("Enter departure airport (or leave blank for any): ");
        string departureAirport = Console.ReadLine();

        Console.Write("Enter arrival destination (or leave blank for any): ");
        string arrivalDestination = Console.ReadLine();

        Console.Write("Enter departure date (yyyy-MM-dd) (or leave blank for any): ");
        string departureDate = Console.ReadLine();

        // Filter de vluchten op basis van de zoekcriteria
        var searchResults = flights.Where(flight =>
            (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport.Contains(departureAirport, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(arrivalDestination) || flight.ArrivalDestination.Contains(arrivalDestination, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(departureDate) || flight.DepartureDate.Contains(departureDate))
        ).ToList();

        // Toon de zoekresultaten
        Console.WriteLine("\nSearch results:");
        if (searchResults.Count == 0)
        {
            Console.WriteLine("No flights found matching the criteria.");
        }
        else
        {
            for (int i = 0; i < searchResults.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {searchResults[i].Airline} - {searchResults[i].DepartureAirport} to {searchResults[i].ArrivalDestination} on {searchResults[i].DepartureDate}");
            }
        }

        // Voeg de Q-optie toe om te stoppen met zoeken
        Console.WriteLine("\nEnter the number of the flight you want to book, or 'Q' to quit.");

        // Gebruikersinvoer
        string choice = Console.ReadLine();

        if (choice.Equals("Q", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Exiting flight search.");
            MenuLogic.PopMenu(); // Keer terug naar het vorige menu
            return;
        }

        // Verwerk de vluchtkeuze
        if (int.TryParse(choice, out int flightIndex) && flightIndex >= 1 && flightIndex <= searchResults.Count)
        {
            // Haal de geselecteerde vlucht op
            FlightModel selectedFlight = searchResults[flightIndex - 1];

            // Controleer of er nog beschikbare stoelen zijn
            if (selectedFlight.AvailableSeats > 0)
            {
                DisplayFlightLayoutAndChooseSeat(selectedFlight); // Toon de layout en kies een stoel
            }
            else
            {
                Console.WriteLine("Sorry, there are no available seats for this flight.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }

        Console.WriteLine("\nPress any key to return to the previous menu...");
        Console.ReadKey();
        MenuLogic.PopMenu(); // Keer terug naar het vorige menu
    }

    // Nieuwe methode voor het tonen van de layout en het kiezen van een stoel
    public static void DisplayFlightLayoutAndChooseSeat(FlightModel selectedFlight)
    {
        Console.Clear();
        Console.WriteLine($"You have selected the following flight:\n");
        Console.WriteLine("{0,-20} {1,-35}", "Airline:", selectedFlight.Airline);
        Console.WriteLine("{0,-20} {1,-35}", "Departure Airport:", selectedFlight.DepartureAirport);
        Console.WriteLine("{0,-20} {1,-35}", "Arrival Destination:", selectedFlight.ArrivalDestination);
        Console.WriteLine("{0,-20} {1,-35}", "Flight Time:", selectedFlight.FlightTime);
        Console.WriteLine("{0,-20} {1,-35}", "Is Cancelled:", (selectedFlight.IsCancelled ? "Yes" : "No"));
        Console.WriteLine("{0,-20} {1,-35}", "Available Seats", selectedFlight.Layout.AvailableSeats.Count);

        Console.WriteLine("Available seats in layout:");
        selectedFlight.Layout.PrintLayout(); // Print de layout

        // Seat kiezen proces
        while (true)
        {
            Console.Write("\nWhich seat do you want? (press Q to quit or Enter to confirm booking): ");
            string seat = Console.ReadLine();

            if (seat.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting seat selection.");
                break;
            }

            else if (string.IsNullOrWhiteSpace(seat))
            {
                Console.WriteLine("Confirming your selected seats...");
                selectedFlight.Layout.ConfirmBooking(); // Bevestig de boeking
                Console.WriteLine("Booking confirmed. Enjoy your flight!");
                break;
            }

            else if (BookFlightLogic.IsSeatAlreadyBooked(selectedFlight, seat))
            {
                Console.WriteLine("Seat is already booked. Please choose a different seat.");
            }
            else
            {
                // Boek de stoel tijdelijk
                Console.Clear();
                selectedFlight.Layout.PrintLayout(); // Toon bijgewerkte layout
            }
        }

        // Update de data en sla de boeking op
        bookedFlights.Add(selectedFlight);
        selectedFlight.AvailableSeats--;
        FlightsAccess.WriteAll(flights);
        Console.WriteLine("\nBooking process complete.");
    }
}