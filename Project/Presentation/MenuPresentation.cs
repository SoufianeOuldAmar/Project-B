public static class MenuPresentation
{
    public static Stack<Action> menuStack = new Stack<Action>();
    public static AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        // Start with the main menu
        menuStack.Push(AuthenticateAccountMenu);

        while (menuStack.Count > 0)
        {
            Action currentMenu = menuStack.Peek();
            Console.Clear();
            currentMenu.Invoke();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    public static void AuthenticateAccountMenu()
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
        while (true)
        {
            Console.WriteLine("=== Log in ===\n");
            Console.Write("Email address: ");
            string emailAddress = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            AccountModel? accountModel = accountsLogic.CheckLogin(emailAddress, password);

            if (accountModel != null)
            {
                MenuLogic.PushMenu(() => FrontPage(accountModel));
                break;
            }
            else
            {
                bool validInput = false;
                do
                {
                    Console.WriteLine("\nUsername or password is incorrect! Do you want to try again? (Input either yes or no)");
                    string choice = Console.ReadLine();
                    bool? yesOrNo = accountsLogic.TryLogInAgain(choice);

                    if (yesOrNo.HasValue && yesOrNo.Value)
                    {
                        Console.Clear();
                        validInput = true;
                        break;
                    }
                    else if (yesOrNo.HasValue && !yesOrNo.Value)
                    {
                        MenuLogic.PopMenu();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Please type 'yes' or 'no'.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (!validInput);
            }
        }
    }


    public static void CreateAccountMenu()
    {
        bool validInput = false;

        do
        {
            Console.Clear();
            Console.WriteLine("=== Create account ===");

            Console.Write("Full name: ");
            string fullName = Console.ReadLine();

            Console.Write("Email address: ");
            string emailAddress = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            // Attempt to create the account
            string resultMessage = accountsLogic.CreateAccount(fullName, emailAddress, password);
            Console.WriteLine(resultMessage);

            // Check if the account was created successfully
            if (resultMessage == "\nAccount created successfully!") // Adjust this condition based on your actual success message
            {
                validInput = true; // Exit the loop if the account is created successfully
                MenuLogic.PopMenu();
            }
            else
            {
                // Only ask the user if they want to try again if account creation failed
                Console.WriteLine("\nWould you like to try again? (Input either yes or no)");
                string choice = Console.ReadLine();
                bool? yesOrNo = accountsLogic.TryLogInAgain(choice);

                if (yesOrNo.HasValue && yesOrNo.Value)
                {
                    // Clear the screen and try again
                    Console.Clear();
                    validInput = false; // Stay in the loop, which is the default state
                }
                else if (yesOrNo.HasValue && !yesOrNo.Value)
                {
                    // Exit the menu
                    MenuLogic.PopMenu();
                    return;
                }
                else
                {
                    // Invalid input, prompt again
                    Console.WriteLine("Invalid input! Please type 'yes' or 'no'.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

        } while (!validInput); // Loop until a valid account is created or user opts to quit
    }



    public static void OrderTicketMenu()
    {

    }

    public static void ViewTicketHistoryMenu()
    {

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
                MenuLogic.PushMenu(OrderTicketMenu);
                break;
            case "2":
                MenuLogic.PushMenu(ViewTicketHistoryMenu);
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