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

        // string message = "Welcome to BOSST Airlines";
        // string bigText = Figgle.FiggleFonts.Standard.Render(message);
        // Random rand = new Random();

        // foreach (char c in bigText)
        // {
        //     Console.ForegroundColor = (ConsoleColor)rand.Next(1, 14);
        //     Console.Write(c);
        //     Thread.Sleep(2);
        // }
        // for (int i = 0; i < 7; i++)
        // {
        //     Console.Clear();
        //     foreach (char c in bigText)
        //     {
        //         Console.ForegroundColor = (ConsoleColor)rand.Next(1, 14);
        //         Console.Write(c);
        //     }
        //     Thread.Sleep(300);
        //     // Thread.Sleep(200);
        //     // Console.ForegroundColor = (ConsoleColor)rand.Next(1, 14);
        //     // Console.WriteLine(bigText);
        //     // Thread.Sleep(200);
        // }
        // Console.ResetColor();

        Console.WriteLine("=== Main Menu ===");
        Console.WriteLine("1. Log in");
        Console.WriteLine("2. Create account");
        Console.WriteLine("3. Search for a flight");
        Console.WriteLine("4. Quit program\n");
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
                MenuLogic.PushMenu(SearchFlightsBeforeLogin);
                break;
            case "4":
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

    // public static void ViewTicketHistoryMenu()
    // {
    //     BookFlightPresentation.CancelBookedFlightMenu();
    // }

    public static void CancelMain(string email)
    {
        CancelPres.CancelMain(email);
    }

    public static void ViewFlightPointsMenu()
    {
        int index = 1;

        Console.WriteLine("=== View Flight Points ===\n");
        var currentAccount = AccountsLogic.CurrentAccount;


        if (currentAccount.FlightPointsDataList.Count == 0)
        {
            Console.WriteLine("You have zero booked flights so there is no transaction history.\n");
        }
        else
        {
            Console.WriteLine(new string('-', 105));
            // Header
            Console.WriteLine("| {0,-8} | {1,-22} | {2,-17} | {3,-22}  | {4,-18} |",
                              "Index", "Flight Points Earned",
                              "Flight ID", "Tickets Bought", "Total Flight Points");
            Console.WriteLine(new string('-', 105)); // Divider with adjusted width

            int totalFlightPoints = 0;

            // Rows
            foreach (var fp in currentAccount.FlightPointsDataList)
            {
                Console.WriteLine("| {0,-8} | {1,-22} | {2,-17} | {3,-22}  | {4,-18}  |",
                                  index,
                                  fp.Points,
                                  fp.FlightId,
                                  fp.TicketsBought,
                                  ""); // Empty for total column in rows
                index++;
                totalFlightPoints += fp.Points;
            }

            Console.WriteLine(new string('-', 105));

            // Total Row
            Console.WriteLine("| {0,-8} | {1,-22} | {2,-17} | {3,-22}  | {4,-18}  |",
                              "", // Empty for other columns
                              "",
                              "",
                              "",
                              totalFlightPoints);

            Console.WriteLine(new string('-', 105) + "\n");
        }


        // Prompt to go back
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

    public static void SearchFlightsBeforeLogin()
    {
        Console.Clear();
        Console.WriteLine("=== Search Flights ===");

    start:
        string departureAirport = string.Empty;
        while (true)
        {
            Console.Write("Enter departure airport (or leave blank for any, or press Q to quit): ");
            departureAirport = Console.ReadLine();
            Console.Clear();

            if (departureAirport.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting to the main menu...");
                MenuLogic.PopMenu();
                return;
            }

            List<string> validDepartures = new List<string>
            {
                "Rotterdam",
                "rotterdam",
                "Rotterdam, The Hague Airport",
                "rotterdam, the hague airport"
            };

            if (string.IsNullOrWhiteSpace(departureAirport) || validDepartures.Contains(departureAirport, StringComparer.OrdinalIgnoreCase))
            {
                break; // Geldige invoer of leeg
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter one of the following: Rotterdam, rotterdam, Rotterdam, The Hague Airport, or rotterdam, the hague airport.");
                Console.ResetColor();
            }
        }

    destination:
        string arrivalDestination = string.Empty;
        while (true)
        {
            Console.Write("Enter arrival destination, you can choose out (or leave blank for any, or press Q to quit, or B to go back): ");
            for (int i = 0; i < ArrivalDestinations.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\n{i + 1}. {ArrivalDestinations[i]}");
            }
            Console.ResetColor();
            Console.Write("Select the number of the arrival destination (or leave blank for any): ");
            string arrivalChoice = Console.ReadLine();
            Console.Clear();

            if (arrivalChoice.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting to the main menu...");
                return;
            }

            if (arrivalChoice.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                goto start;
            }

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a valid number corresponding to a destination.");
                Console.ResetColor();
            }
        }

    departureDate:
        string departureDate = string.Empty;
        while (true)
        {
            Console.Write("Enter departure date (yyyy-MM-dd) (or leave blank for any, or press Q to quit, or B to go back): ");
            departureDate = Console.ReadLine();
            Console.Clear();

            if (departureDate.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting to the main menu...");
                Console.ResetColor();
                return;
            }

            if (departureDate.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                goto destination;
            }

            if (string.IsNullOrWhiteSpace(departureDate) || DateTime.TryParseExact(departureDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
            {
                break; // Geldige invoer of leeg
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date format. Please enter a date in the format yyyy-MM-dd.");
                Console.ResetColor();
                Console.Clear();
            }
        }

    timeOfDay:
        string timeOfDay = string.Empty;
        while (true)
        {
            Console.WriteLine("Enter the time of day (Morning, Midday, Evening, Night) or leave blank for any (or press Q to quit, or B to go back): ");
            timeOfDay = Console.ReadLine()?.Trim().ToLower();
            Console.Clear();

            if (timeOfDay.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting to the main menu...");
                Console.ResetColor();
                return;
            }

            if (timeOfDay.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                goto departureDate;
            }

            List<string> validTimesOfDay = new List<string> { "morning", "midday", "evening", "night" };

            if (string.IsNullOrWhiteSpace(timeOfDay) || validTimesOfDay.Contains(timeOfDay))
            {
                break; // Geldige invoer
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter 'Morning', 'Midday', 'Evening', 'Night', or leave blank.");
                Console.ResetColor();
                Console.Clear();
            }
        }

        Dictionary<string, (TimeSpan Start, TimeSpan End)> timeOfDayMapping = new Dictionary<string, (TimeSpan Start, TimeSpan End)>
        {
            { "morning", (Start: new TimeSpan(6, 0, 0), End: new TimeSpan(11, 59, 59)) },
            { "midday", (Start: new TimeSpan(12, 0, 0), End: new TimeSpan(17, 59, 59)) },
            { "evening", (Start: new TimeSpan(18, 0, 0), End: new TimeSpan(23, 59, 59)) },
            { "night", (Start: new TimeSpan(0, 0, 0), End: new TimeSpan(5, 59, 59)) }
        };

        var searchResults = flights.Where(flight =>
            (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport.Contains(departureAirport, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(arrivalDestination) || flight.ArrivalDestination.Contains(arrivalDestination, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(departureDate) || flight.DepartureDate.Contains(departureDate)) &&
            (string.IsNullOrEmpty(timeOfDay) ||
                (timeOfDayMapping.TryGetValue(timeOfDay, out var timeRange) &&
                DateTime.TryParse(flight.FlightTime, out var flightTime) &&
                flightTime.TimeOfDay >= timeRange.Start && flightTime.TimeOfDay <= timeRange.End))
        ).ToList();

        Console.WriteLine("\nSearch results:");
        if (searchResults.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No flights found matching the criteria.");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"{"#",-3} {"Airline",-20} {"From",-46} {"To",-38} {"Date",-16} {"Time",-15} {"Price"}");
            Console.WriteLine(new string('-', 195));
            for (int i = 0; i < searchResults.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{(i + 1),-3}. {searchResults[i].Airline,-20} {searchResults[i].DepartureAirport,-45} {searchResults[i].ArrivalDestination,-35} {searchResults[i].DepartureDate,-18}  {searchResults[i].FlightTime,-15} €{searchResults[i].TicketPrice},-");
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nNote: You need to log in to book a flight.");
            Console.ResetColor();
        }

        Console.WriteLine("\nPress any key to return to the main menu...");
        MenuLogic.PopMenu();
        Console.ReadKey();
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
                // Console.WriteLine("This feautre isn't implemented yet.");
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

    start:
        string departureAirport = string.Empty;
        while (true)
        {
            Console.Write("Enter departure airport (or leave blank for any, or press Q to quit): ");
            departureAirport = Console.ReadLine();
            Console.Clear();

            if (departureAirport.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting to the main menu...");
                MenuLogic.PopMenu();
                return;
            }

            List<string> validDepartures = new List<string>
        {
            "Rotterdam",
            "rotterdam",
            "Rotterdam, The Hague Airport",
            "rotterdam, the hague airport"
        };

            if (string.IsNullOrWhiteSpace(departureAirport) || validDepartures.Contains(departureAirport, StringComparer.OrdinalIgnoreCase))
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter one of the following: Rotterdam, rotterdam, Rotterdam, The Hague Airport, or rotterdam, the hague airport.");
                Console.ResetColor();
                Console.Clear();
            }
        }

    destination:
        string arrivalDestination = string.Empty;
        while (true)
        {
            Console.Write("Enter arrival destination, you can choose out (or leave blank for any, or press Q to quit, or B to go back): ");
            for (int i = 0; i < ArrivalDestinations.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\n{i + 1}. {ArrivalDestinations[i]}");
            }
            Console.ResetColor();
            Console.Write("Select the number of the arrival destination (or leave blank for any): ");
            string arrivalChoice = Console.ReadLine();
            Console.Clear();

            if (arrivalChoice.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting to the main menu...");
                return;
            }

            if (arrivalChoice.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                goto start;
            }

            int choiceNumber;
            if (string.IsNullOrWhiteSpace(arrivalChoice))
            {
                break;
            }

            if (int.TryParse(arrivalChoice, out choiceNumber) && choiceNumber >= 1 && choiceNumber <= ArrivalDestinations.Count)
            {
                arrivalDestination = ArrivalDestinations[choiceNumber - 1];
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a valid number corresponding to a destination.");
                Console.ResetColor();
            }
        }

    departureDate:
        string departureDate = string.Empty;
        while (true)
        {
            Console.Write("Enter departure date (yyyy-MM-dd) (or leave blank for any, or press Q to quit, or B to go back): ");
            departureDate = Console.ReadLine();
            Console.Clear();

            if (departureDate.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting to the main menu...");
                Console.ResetColor();
                return;
            }

            if (departureDate.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                goto destination;
            }

            if (string.IsNullOrWhiteSpace(departureDate) || DateTime.TryParseExact(departureDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date format. Please enter a date in the format yyyy-MM-dd.");
                Console.ResetColor();
            }
        }

    timeOfDay:
        string timeOfDay = string.Empty;
        while (true)
        {
            Console.WriteLine("Enter the time of day (Morning, Midday, Evening, Night) or leave blank for any (or press Q to quit, or B to go back): ");
            timeOfDay = Console.ReadLine()?.Trim().ToLower();
            Console.Clear();

            if (timeOfDay.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting to the main menu...");
                Console.ResetColor();
                return;
            }

            if (timeOfDay.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                goto departureDate;
            }

            List<string> validTimesOfDay = new List<string> { "morning", "midday", "evening", "night" };

            if (string.IsNullOrWhiteSpace(timeOfDay) || validTimesOfDay.Contains(timeOfDay))
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter 'Morning', 'Midday', 'Evening', 'Night', or leave blank.");
                Console.ResetColor();
            }
        }

        Dictionary<string, (TimeSpan Start, TimeSpan End)> timeOfDayMapping = new Dictionary<string, (TimeSpan Start, TimeSpan End)>
    {
        { "morning", (Start: new TimeSpan(6, 0, 0), End: new TimeSpan(11, 59, 59)) },
        { "midday", (Start: new TimeSpan(12, 0, 0), End: new TimeSpan(17, 59, 59)) },
        { "evening", (Start: new TimeSpan(18, 0, 0), End: new TimeSpan(23, 59, 59)) },
        { "night", (Start: new TimeSpan(0, 0, 0), End: new TimeSpan(5, 59, 59)) }
    };

        var searchResults = flights.Where(flight =>
            (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport.Contains(departureAirport, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(arrivalDestination) || flight.ArrivalDestination.Contains(arrivalDestination, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(departureDate) || flight.DepartureDate.Contains(departureDate)) &&
            (string.IsNullOrEmpty(timeOfDay) ||
                (timeOfDayMapping.TryGetValue(timeOfDay, out var timeRange) &&
                DateTime.TryParse(flight.FlightTime, out var flightTime) &&
                flightTime.TimeOfDay >= timeRange.Start && flightTime.TimeOfDay <= timeRange.End))
        ).ToList();

        Console.WriteLine("\nSearch results:");
        if (searchResults.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No flights found matching the criteria.");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"{"#",-3} {"Airline",-20} {"From",-46} {"To",-38} {"Date",-16} {"Time",-15} {"Price"}");
            Console.WriteLine(new string('-', 195));
            for (int i = 0; i < searchResults.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{(i + 1),-3}. {searchResults[i].Airline,-20} {searchResults[i].DepartureAirport,-45} {searchResults[i].ArrivalDestination,-35} {searchResults[i].DepartureDate,-18}  {searchResults[i].FlightTime,-15} €{searchResults[i].TicketPrice},-");
            }
            Console.ResetColor();

            while (true)
            {
                Console.WriteLine("\nEnter the flight number to book a seat, or 'Q' to quit:");
                string choice = Console.ReadLine();

                if (choice.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting to the main menu...");
                    return;
                }

                if (int.TryParse(choice, out int flightIndex) && flightIndex >= 1 && flightIndex <= searchResults.Count)
                {
                    FlightModel selectedFlight = searchResults[flightIndex - 1];
                    DisplayFlightLayoutAndChooseSeat(selectedFlight);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please enter a valid flight number or 'Q' to quit.");
                    Console.ResetColor();
                }
            }
        }

        Console.WriteLine("\nPress any key to return to the main menu...");
        MenuLogic.PopMenu();
        Console.ReadKey();
    }

    // Nieuwe methode voor het tonen van de layout en het kiezen van een stoel
    public static void DisplayFlightLayoutAndChooseSeat(FlightModel selectedFlight)
    {
        BookFlightPresentation.BookFlightMenu(true, selectedFlight);
    }
}
