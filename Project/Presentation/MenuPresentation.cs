using DataModels;
using DataAccess;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Net;
using System.Collections.Generic;

public static class MenuPresentation
{
    public static Stack<Action> menuStack = new Stack<Action>(); // Een stack voor functies

    public static UserAccountModel currentAccount { get; set; }
    public static List<UserAccountModel> GotNotificationScreen = new List<UserAccountModel>();


    public static void Start()
    {
        BookFlightLogic.TakeOff();
        // Start with the main menu
        menuStack.Push(AuthenticateAccountMenu); // Voeg AuthenticateAccountMenu() toe aan de lege stack

        while (menuStack.Count > 0) // terwijl de stack nog functies bevat, blijft ie doorgaan. Zo niet dan stopt de loop en dus het programma.
        {
            Action currentMenu = menuStack.Peek();
            Console.Clear();
            currentMenu.Invoke();

            PressAnyKey();
        }
    }

    public static void AuthenticateAccountMenu() // Begin menu wanneer het programma opstart.
    {
        Console.WriteLine("=== Main Menu ✈️  ===");
        Console.WriteLine("1. 🔑 Log in");
        Console.WriteLine("2. 📝 Create account");
        Console.WriteLine("3. 🔍 Search for a flight");
        Console.WriteLine("4. 📖 About us");
        Console.WriteLine("5. 👥 Join Our Team");
        Console.WriteLine("6. ❌ Quit program\n");
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
                MenuLogic.PushMenu(AboutUsPres.aboutUsMenu);
                break;
            case "5":
                EmployeesPresentation.EmployeeRegistrationpresentation();
                break;
            case "6":
                // Exit by popping the main menu
                MenuLogic.PopMenu();
                Console.WriteLine("\nUntil next time!");
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

    public static void CancelMain(string email)
    {
        CancelPresentation.CancelMain(email);
    }

    public static void ViewFlightPointsMenu()
    {
        FlightPointPresentation.ViewFlightPointsMenu();
    }

    public static void SearchFlightsBeforeLogin()
    {
        SearchFlightPresentation.SearchFlightMenu(true);
    }

    public static void FrontPageUser(UserAccountModel UserAccountModel)
    {
        currentAccount = UserAccountModel;
        bool NotificationsPresent = NotificationLogic.CheckForNotifications(UserAccountModel);

        if (NotificationsPresent && !GotNotificationScreen.Contains(UserAccountModel))
        {
            Console.WriteLine("You have notifications to check!");
            PressAnyKey();
            GotNotificationScreen.Add(UserAccountModel);
            Console.Clear();
        }

        string exclamationMark = NotificationsPresent ? " ❗" : "";

        Console.WriteLine($"Logged in as: 👤 {UserAccountModel.FullName}\n");
        Console.WriteLine("=== 🏠 Front Page ====");
        Console.WriteLine("1. 🔍 Search for flights");
        Console.WriteLine("2. 🧾 View history of tickets");
        Console.WriteLine("3. 🎯 View Flight Point");
        Console.WriteLine("4. 🔔 Notifications" + exclamationMark);
        Console.WriteLine("5. 📖 About us");
        Console.WriteLine("6. 📋 Feedback Menu");
        Console.WriteLine("7. 🔓 Log out");
        Console.Write("\nChoose an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                SearchFlightsMenu();
                break;
            case "2":
                MenuLogic.PushMenu(() => CancelMain(UserAccountModel.EmailAddress));
                break;
            case "3":
                MenuLogic.PushMenu(ViewFlightPointsMenu);
                break;
            case "4":
                MenuLogic.PushMenu(NotificationPage);
                break;
            case "5":
                MenuLogic.PushMenu(AboutUsPres.aboutUsMenu);
                break;
            case "6":
                FeedbackPresentation.FeedbackMenu(UserAccountModel);
                break;
            case "7":
                Console.WriteLine("\nLogging out...");
                MenuLogic.PopMenu();
                MenuLogic.PopMenu();
                currentAccount = null;
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    public static List<FlightModel> bookedFlights = new List<FlightModel>(); // We maken een lijst van geboekte vluchten

    public static void SearchFlightsMenu()
    {
        SearchFlightPresentation.SearchFlightMenu();
    }

    public static void NotificationPage()
    {
        NotificationPresentation.PrintNotificationPage();
    }

    public static void PressAnyKey()
    {
        Console.Write("\nPress any key to continue... ");
        Console.ReadKey();
    }

    public static void PrintColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

}
