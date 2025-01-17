public class FinancialAdvisorPres
{
    public static void LogInFinancialAdvisor()
    {
        FinancialAdvisorLogic financial = new FinancialAdvisorLogic();

        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        // Console.Clear();

        Console.Write("Enter Password: ");
        string password = HidePassword();

        // validate
        bool isValid = financial.ValidateLogin(username, password);
        if (isValid)
        {
            Console.Clear();
            Console.WriteLine("Login successful!");

            var financialAccount = financial.GetFinancialAccountByUsername(username);

            if (financialAccount != null)
            {
                // Call the menu method with the actual FinancialAccountModel
                FrontPageFinancialAdvisor(financialAccount);
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid username or password. Please try again.");
            Console.WriteLine("You will need to wait for 30 seconds before trying again...");
            Thread.Sleep(30000);
            LogInFinancialAdvisor();
        }




    }

    public static string HidePassword()
    {
        string password = "";

        ConsoleKeyInfo keyy;

        while (true)
        {
            keyy = Console.ReadKey(true);

            if (keyy.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }

            else if (keyy.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");            // remove dot 
            }

            else if (!char.IsControl(keyy.KeyChar))
            {
                password += keyy.KeyChar;
                Console.Write("*");
            }

        }

        return password;

    }

    public static void FrontPageFinancialAdvisor(FinancialAccountModel financial)
    {

        while (true)
        {
            Console.Clear();

            Console.WriteLine($"Logged in as Financial Advisor: {financial.UserName}");
            Console.WriteLine("\n=== Front page ===");
            Console.WriteLine("1. Display yearly Financial report ");
            Console.WriteLine("2. Log out");

            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    while (true)
                    {
                        Console.Clear();

                        Console.Write("Enter the year for the financial report: ");
                        string yearStr = Console.ReadLine();

                        if (int.TryParse(yearStr, out int year))
                        {
                            if (year >= 0)
                            {
                                FinancialReportPresentation.GenerateDataForReport(year);
                                MenuPresentation.PressAnyKey();

                                break; // Exit the loop after successfully generating the report.
                            }
                            else
                            {
                                Console.WriteLine("Invalid year. A year can't be negative.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid year.");
                        }

                        MenuPresentation.PressAnyKey();
                    }
                    break;


                case "2":
                    Console.WriteLine("Logging out...");
                    MenuLogic.PopMenu();
                    return; // Exit the method after logging out.

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    MenuPresentation.PressAnyKey();
                    break;
            }
        }
    }





}