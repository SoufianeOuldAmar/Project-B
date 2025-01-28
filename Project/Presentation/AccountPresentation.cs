using System.Threading;

public static class AccountPresentation
{
    public static void LogIn()
    {
        bool newLineValid = true;
        int i = 0;
        while (true)
        {
            Console.WriteLine("=== 🔑 Log in ===\n");
            Console.Write("Are you an Admin, an User or a Financial Advisor? (Enter A/U/F for admin, user or financial advisor or enter 'Q' to go back): ");
            string input1 = Console.ReadLine().ToLower();
            if (input1 == "a")
            {
                Console.Clear();
                Console.WriteLine("=== 🔑 Log in ===\n");
                AdminAccountPresentation.Login();
                break;
            }

            else if (input1 == "f")
            {
                Console.Clear();
                Console.WriteLine("=== 🔑 Log in ===\n");
                FinancialAdvisorPresentation.LogInFinancialAdvisor();

                break;
            }

            else if (input1 == "q")
            {
                MenuLogic.PopMenu();
                break;
            }

            else if (input1 == "u")
            {
                Console.Clear();
                Console.WriteLine("=== 🔑 Log in ===\n");
                Console.Write("📧 Email address: ");
                string emailAddress = Console.ReadLine();
                Console.Write("🔒 Password: ");
                string password = Console.ReadLine();

                UserAccountModel? userAccountModel = UserAccountLogic.CheckLogin(emailAddress, password);
                if (userAccountModel != null)
                {
                    Console.WriteLine("\n✅ Success! Welcome back!");
                    MenuLogic.PushMenu(() => MenuPresentation.FrontPageUser(userAccountModel));
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
                        bool? yesOrNo = UserAccountLogic.TryLogInAgain(choice);
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
                            MenuPresentation.PressAnyKey();
                            Console.Clear();
                        }
                    } while (!validInput);
                }
            }
        }
    }


    // public static string CreateAccount(string fullName, string email, string password)
    // {

    // }

    public static void CreateAccount()
    {
        bool validInput = false;

        do
        {
            Console.Clear();
            Console.WriteLine("=== Create Account ===");

            string fullName = null;
            string emailAddress = null;
            string password = null;

            if (!GetInputWithQuitOption("Full name", out fullName))
            {
                return;
            }

            if (!GetInputWithQuitOption("Email address", out emailAddress))
            {
                return;
            }

            if (!GetInputWithQuitOption("Password", out password))
            {
                return;
            }

            // Attempt to create the account
            List<UserAccountLogic.CreateAccountStatus> statusList = UserAccountLogic.CheckCreateAccount(fullName, emailAddress, password);

            if (statusList.Count == 0)
            {
                int id = UserAccountLogic._accounts.Count + 1; // Use the next id
                UserAccountModel account = new UserAccountModel(id, emailAddress, password, fullName);
                UserAccountLogic.UpdateList(account);
                Console.WriteLine("\nAccount created successfully!");
                validInput = true;
                MenuLogic.PopMenu();
            }
            else
            {
                // Display error messages
                Console.WriteLine("\nError messages:");
                foreach (var status in statusList)
                {
                    switch (status)
                    {
                        case UserAccountLogic.CreateAccountStatus.IncorrectFullName:
                            Console.WriteLine("- Full name is incorrect. Please enter a valid name.");
                            break;
                        case UserAccountLogic.CreateAccountStatus.IncorrectEmail:
                            Console.WriteLine("- Email is incorrect. Please enter a valid email.");
                            break;
                        case UserAccountLogic.CreateAccountStatus.IncorrectPassword:
                            Console.WriteLine("- Password is too short. It must be at least 5 characters.");
                            break;
                        case UserAccountLogic.CreateAccountStatus.EmailExists:
                            Console.WriteLine("- Email already exists. Use another email.");
                            break;
                    }
                }

                // Ask if the user wants to try again
                Console.Write("\nWould you like to try again? (yes/no): ");
                string choice = Console.ReadLine();
                bool? tryAgain = UserAccountLogic.TryLogInAgain(choice);

                if (tryAgain.HasValue && !tryAgain.Value)
                {
                    MenuLogic.PopMenu();
                    return;
                }
            }

        } while (!validInput);
    }

    private static bool GetInputWithQuitOption(string prompt, out string input)
    {
        input = null;

        while (true)
        {
            Console.Write($"{prompt} (or enter 'Q' to quit the process): ");
            input = Console.ReadLine();

            if (input.ToUpper() == "Q")
            {
                if (ConfirmQuit())
                {
                    MenuLogic.PopMenu();
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }

    private static bool ConfirmQuit()
    {
        while (true)
        {
            Console.Write("\nDo you really want to quit this operation? (yes/no): ");
            string response = Console.ReadLine();

            if (response.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (response.Equals("no", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input! Please type 'yes' or 'no'.");
            }
        }
    }


    public static void ViewFlightPoints()
    {
        Console.WriteLine("=== Flight Points ===\n");
        // Console.WriteLine($"Total flight points: {UserAccountLogic.CurrentAccount.FlightPoints}");

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
