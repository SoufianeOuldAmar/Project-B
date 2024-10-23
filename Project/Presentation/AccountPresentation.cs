public static class AccountPresentation
{
    public static void LogIn()
    {
        bool newLineValid = true;
        while (true)
        {
            Console.WriteLine("=== Log in ===\n");
            Console.Write("Email address: ");
            string emailAddress = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            AccountModel? accountModel = AccountsLogic.CheckLogin(emailAddress, password);

            if (accountModel != null)
            {
                Console.WriteLine("\nSucces! Welcome back!");
                MenuLogic.PushMenu(() => MenuPresentation.FrontPage(accountModel));
                break;
            }
            else
            {
                bool validInput = false;
                do
                {
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
}