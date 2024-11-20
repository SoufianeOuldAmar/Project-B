using DataModels;
using DataAccess;
using System.Runtime.CompilerServices;

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

    public static void ViewTicketHistoryMenu()
    {
        BookFlightPresentation.CancelBookedFlightMenu();
    }

    public static void CancelMain(string email)
    {
        CancelPres.CancelMain(email);
    }

    public static void ViewFlightPointsMenu()
    {
        Console.WriteLine("=== View Flight Points ===");
        var currentAccount = AccountsLogic.CurrentAccount;

        Console.WriteLine($"Total Flight Points: {currentAccount.FlightPoints}\n\n");
        while (true)
        {
            Console.Write("Press 'Q' to go back: ");
            string input = Console.ReadLine().ToUpper();

            if (input != "Q")
            {
                Console.WriteLine("Wrong input try again, press 'Q' to go back");
                continue;
            }
            else
            {
                MenuLogic.PopMenu();
                break;
            }
        }


    }

    public static void FrontPageUser(AccountModel accountModel)
    {
        Console.WriteLine($"Logged in as: {accountModel.FullName}\n");
        Console.WriteLine("=== Front page ===");
        Console.WriteLine("1. Order a ticket");
        Console.WriteLine("2. View history of tickets");
        Console.WriteLine("3. Search for flights");
        Console.WriteLine("4. View Flight Points");
        Console.WriteLine("5. Log out");
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
                MenuLogic.PushMenu(SearchFlightsMenu);  // Voeg de zoekfunctie toe
                break;
            case "4":
                MenuLogic.PushMenu(ViewFlightPointsMenu);
                break;
            case "5":
                Console.WriteLine("\nLogging out...");
                // AccountsLogic.LogOut(); (voeg logout-logica toe indien nodig)
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
    
    public static List<FlightModel> bookedFlights = new List<FlightModel>(); // We maken een lijst van geboekte vluchten
    public static List<FlightModel> flights = FlightsAccess.ReadAll(); // dit zorgt ervoor dat we de json file kunnen lezen

    //Een lijst met de destinations waar de user uit kan kiezen
    static readonly List<string> ArrivalDestinations = new List<string>
    {
        "Paris, Charles de Gaulle Airport",
        "Frankfurt, Frankfurt Airport",
        "Brussels, Brussels Airport",
        "Warsaw, Warsaw Chopin Airport",
        "Budapest, Budapest Ferenc Liszt International Airport",
        "Riga, Riga International Airport",
        "Lisbon, Lisbon Airport",
        "London, Heathrow Airport",
        "Mallorca, Palma de Mallorca Airport",
        "Istanbul, Istanbul Airport",
        "Milan, Malpensa Airport",
        "Oslo, Oslo Gardermoen Airport",
        "Zurich, Zurich Airport",
        "Vienna, International Airport",
        "Naples, Naples Airport",
        "Madrid, Madrid Barajas Airport"
    };
    public static void SearchFlightsMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Search Flights ===");

        // Vraag de gebruiker om vertrek luchthaven met validatie
        string departureAirport = string.Empty;
        while (true)
        {
            Console.Write("Enter departure airport (or leave blank for any): ");
            departureAirport = Console.ReadLine();

            // Lijst van toegestane invoeropties
            List<string> validDepartures = new List<string>
            {
                "Rotterdam",
                "rotterdam",
                "Rotterdam, The Hague Airport",
                "rotterdam, the hague airport"
            };

            if (string.IsNullOrWhiteSpace(departureAirport) || validDepartures.Contains(departureAirport))
            {
                break; // Geldige invoer of leeg
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter one of the following: Rotterdam, rotterdam, Rotterdam, The Hague Airport, or rotterdam, the hague airport.");
            }
        }

        // Vraag de gebruiker om een aankomst bestemming met validatie
        string arrivalDestination = string.Empty;
        while (true)
        {
            Console.Write("Enter arrival destination, you can choose out (or leave blank for any): ");
            for (int i = 0; i < ArrivalDestinations.Count; i++)
            {
                Console.WriteLine($"\n{i + 1}. {ArrivalDestinations[i]}");
            }

            Console.Write("Select the number of the arrival destination (or leave blank for any): ");
            string arrivalChoice = Console.ReadLine();
            int choiceNumber;
            if (string.IsNullOrWhiteSpace(arrivalChoice))
            {
                break; // Geen keuze betekent "alle bestemmingen"
            }

            if (int.TryParse(arrivalChoice, out choiceNumber) && choiceNumber >= 1 && choiceNumber <= ArrivalDestinations.Count)
            {
                arrivalDestination = ArrivalDestinations[choiceNumber - 1];
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number corresponding to a destination.");
            }
        }

        // Vraag om een vertrekdatum met validatie
        string departureDate = string.Empty;
        while (true)
        {
            Console.Write("Enter departure date (yyyy-MM-dd) (or leave blank for any): ");
            departureDate = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(departureDate) || DateTime.TryParseExact(departureDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
            {
                break; // Geldige invoer of leeg
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter a date in the format yyyy-MM-dd.");
            }
        }

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
                Console.WriteLine($"{i + 1}. {searchResults[i].Airline} - {searchResults[i].DepartureAirport} to {searchResults[i].ArrivalDestination} on {searchResults[i].DepartureDate} on {searchResults[i].FlightTime} for €{searchResults[i].TicketPrice},-");
            }
        }

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
        BookFlightPresentation.BookFlightMenu(true, selectedFlight);
    }
}