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

    public static void FrontPage(AccountModel accountModel)
    {
        Console.WriteLine($"Logged in as: {accountModel.FullName}\n");
        Console.WriteLine("=== Front page ===");
        Console.WriteLine("1. Order a ticket");
        Console.WriteLine("2. View history of tickets");
        Console.WriteLine("3. Log out");
        Console.Write("\nChoose an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                MenuLogic.PushMenu(BookFlightMenu);
                break;
            case "2":
                MenuLogic.PushMenu(ViewTicketHistoryMenu);
                Console.WriteLine("\nThis feature isn't available yet.");
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
}