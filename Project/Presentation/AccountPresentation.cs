public static class AccountPresentation
{
    public static void LogIn()
    {
        bool newLineValid = true;
        int i = 0;
        while (true)
        {
            Console.WriteLine("=== Log in ===\n");
            Console.WriteLine("Are you Admin or User? A/U");
            Console.WriteLine("Enter q to exit");
            string input1 = Console.ReadLine().ToLower();
            if (input1 == "q")
            {
                Console.WriteLine("You chose to exit.");
                Console.Clear();
                MenuPresentation.AuthenticateAccountMenu();
            }
            if (input1 == "a")
            {
                AdminAccountPresentation.Login();
                break;
            }

            else if (input1 == "u")
            {
                Console.Write("Email address: ");
                string emailAddress = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                AccountModel? accountModel = AccountsLogic.CheckLogin(emailAddress, password);
                if (accountModel != null)
                {
                    Console.WriteLine("\nSucces! Welcome back!");
                    MenuLogic.PushMenu(() => MenuPresentation.FrontPageUser(accountModel));
                    break;
                }
                else
                {
                    bool validInput = false;
                    do
                    {
                        i++;
                        Console.WriteLine("Invalid email or password. Please try again.");
                        if (i >= 3)
                        {
                            Console.WriteLine("You will be locked out for 1 minute due to multiple failed attempts.");
                            Thread.Sleep(60000);
                            i = 0;
                        }

                        string newLine = newLineValid ? "\n" : "";
                        Console.Write($"{newLine}Username or password is incorrect! Do you want to try again? (Input either yes or no): ");
                        string choice = Console.ReadLine();
                        bool? yesOrNo = AccountsLogic.TryLogInAgain(choice);
                        if (yesOrNo.HasValue && yesOrNo.Value)
                        {
                            Console.Clear();
                            validInput = true;
                            newLineValid = true;
                            break;
                        }
                        else if (yesOrNo.HasValue && !yesOrNo.Value)
                        {
                            MenuLogic.PopMenu();
                            newLineValid = true;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input! Please type 'yes' or 'no'.\n");
                            newLineValid = false;
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    } while (!validInput);
                }
            }
        }
    }

    public static void CreateAccount()
    {
        bool validInput = false;

        do

        {
            Console.Clear();
            Console.WriteLine("=== Create account ===");

            Console.Write("\nFull name: ");
            string fullName = Console.ReadLine();

            Console.Write("Email address: ");
            string emailAddress = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            // Attempt to create the account
            string resultMessage = AccountsLogic.CreateAccount(fullName, emailAddress, password);
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
                Console.Write("\nWould you like to try again? (Input either yes or no): ");
                string choice = Console.ReadLine();
                bool? yesOrNo = AccountsLogic.TryLogInAgain(choice);

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

    public static void ViewFlightPoints()
    {
        Console.WriteLine("=== Flight Points ===\n");
        Console.WriteLine($"Total flight points: {AccountsLogic.CurrentAccount.FlightPoints}");

        while (true)
        {
            Console.WriteLine("Press 'Q' to go back");
            string input = Console.ReadLine().ToLower();

            if (input == "q")
            {
                MenuLogic.PopMenu();
                break;
            }
            else
            {
                Console.WriteLine("Incorrect input. Enter 'Q'.");
            }
        }
    }
}
