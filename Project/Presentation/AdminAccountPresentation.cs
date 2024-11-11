using System.Collections.Concurrent;
using System.Data.Common;
using System.Threading;
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
            string password = Console.ReadLine();

            bool isValid = logic.ValidateLogin(username, password);

            if (isValid)
            {
                Console.WriteLine("Login as Admin successful. Welcome!");
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("Enter (a) to add new flight");
                Console.WriteLine("Press 'Esc' to logout");

                // string addflight = Console.ReadLine().ToLower();
                var keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("You chose to exit.");
                    Console.Clear();
                    MenuPresentation.AuthenticateAccountMenu();
                }
                else if (keyInfo.KeyChar == 'a' || keyInfo.KeyChar == 'A')
                {
                    AdminAddFlightsPresentation adminAddflight = new AdminAddFlightsPresentation();
                    FlightModel newFlight = adminAddflight.AddNewFlights();
                    FlightsAccess.AdminAddNewFlight(newFlight);
                    break;
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
                    Console.WriteLine("You will be locked out for 1 minute due to multiple failed attempts.");
                    Thread.Sleep(60000);
                    i = 0;
                }

            }
        }

    }

}