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

            Console.WriteLine("Enter Your Username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Your password: ");
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
                Console.WriteLine("Login as Admin successful. Welcome!");
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("Enter (a) to add new flight");
                Console.WriteLine("Enter (c) to add change flights details");
                Console.WriteLine("R. Reset all flights");
                Console.WriteLine("Press 'Esc' to logout");

                // string addflight = Console.ReadLine().ToLower();
                var keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("You chose to exit.");
                    MenuLogic.PopMenu(); // Correctly navigate back by popping the current menu
                    break; // Exit the current while loop
                }
                else if (keyInfo.KeyChar == 'a' || keyInfo.KeyChar == 'A')
                {
                    AdminAddFlightsPresentation adminAddflight = new AdminAddFlightsPresentation();
                    FlightModel newFlight = adminAddflight.AddNewFlights();
                    FlightsAccess.AdminAddNewFlight(newFlight);
                    adminAddflight.Exit();
                    break;
                }
                else if (keyInfo.KeyChar == 'c' || keyInfo.KeyChar == 'C')
                {
                    AdminFlightManagerPresentation.LaodFlightPresentaion();
                    AdminFlightManagerPresentation.UpdateDetailsPresentation();
                    AdminFlightManagerPresentation.Exit();
                    break;
                }

                else if (keyInfo.KeyChar == 'r' || keyInfo.KeyChar == 'R')
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