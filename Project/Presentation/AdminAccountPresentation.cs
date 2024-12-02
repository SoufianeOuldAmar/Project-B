using System.Collections.Concurrent;
using System.Data.Common;
using System.Threading;
using DataModels;
using DataAccess;
static class AdminAccountPresentation
{
    static AdminAccountLogic logic = new AdminAccountLogic();
    public static void Login()
    {
        int i = 0;
        while (true)
        {

            Console.Write("Enter Your Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Your password: ");
            // string password = Console.ReadLine();
            string password = "";
            bool passwordVisible = false;

            while (true)
            {
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                    break;
                if (key.Key == ConsoleKey.Tab)
                {
                    passwordVisible = !passwordVisible;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password += key.KeyChar;
                    if (passwordVisible)
                    {
                        Console.Write(key.KeyChar);
                    }
                    else
                    {
                        Console.Write("*");
                    }
                }
            }
            Console.WriteLine();

            bool isValid = logic.ValidateLogin(username, password);

            if (isValid)
            {
                Console.WriteLine("\nLogin as Admin successful. Welcome!");

                Console.Write("\nPress any key to continue... ");
                Console.ReadKey();

                Console.Clear();

                Console.WriteLine("=== Admin Page ===\n");

                Console.WriteLine("1. Add a new flight");
                Console.WriteLine("2. Change current flight details");
                Console.WriteLine("3. Reset all flights");
                Console.WriteLine("4. Log out");

                // string addflight = Console.ReadLine().ToLower();
                string logOut;
                var keyInfo = Console.ReadLine();
                if (keyInfo == "4")
                {

                    while (true)
                    {
                        Console.Write("Are you sure you want to log out? (yes/no): ");
                        logOut = Console.ReadLine();

                        if (logOut == "yes")
                        {
                            MenuLogic.PopMenu(); // Correctly navigate back by popping the current menu
                            break; // Exit the current while loop
                        }

                        else if (logOut == "no")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input, enter either 'yes' or 'no'.");
                        }
                    }

                    if (logOut == "yes")
                    {
                        break;
                    }
                    else if (logOut == "no")
                    {
                        continue;
                    }


                }
                else if (keyInfo == "1")
                {
                    AdminAddFlightsPresentation adminAddflight = new AdminAddFlightsPresentation();
                    FlightModel newFlight = adminAddflight.AddNewFlights();
                    FlightsAccess.AdminAddNewFlight(newFlight);
                    adminAddflight.Exit();
                    break;
                }
                else if (keyInfo == "2")
                {
                    AdminFlightManagerPresentation.LaodFlightPresentaion();
                    AdminFlightManagerPresentation.UpdateDetailsPresentation();
                    AdminFlightManagerPresentation.Exit();
                    break;
                }

                else if (keyInfo == "3")
                {
                    LayoutModel layout = LayoutModel.CreateBoeing737Layout();
                    layout.ResetAllSeats();
                }

                else
                {
                    Console.WriteLine("Invalid key. Press 'Esc' to exit or 'a' to add a new flight.");
                }

            }
            else
            {
                i++;
                Console.WriteLine("Invalid email or password. Please try again.");
                if (i >= 3)
                {
                    Console.WriteLine("You will be locked out for 30 seconds due to multiple failed attempts.");
                    Thread.Sleep(30000);
                    i = 0;
                }

            }
        }

    }

}