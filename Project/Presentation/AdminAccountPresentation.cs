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
        bool isRunning = true; // Add a flag to control the main loop

        while (isRunning)
        {
            Console.Write("Enter Your Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Your password: ");
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

                bool adminMenuRunning = true; // Flag for admin menu loop
                while (adminMenuRunning)
                {
                    Console.WriteLine("=== Admin Page ===\n");

                    Console.WriteLine("1. Add a new flight");
                    Console.WriteLine("2. Change current flight details");
                    Console.WriteLine("3. Reset all flights");
                    Console.WriteLine("4. Log out");

                    Console.Write("\nChoose an option: ");
                    string keyInfo = Console.ReadLine();

                    if (keyInfo == "4")
                    {
                        while (true)
                        {
                            Console.Write("\nAre you sure you want to log out? (yes/no): ");
                            string logOut = Console.ReadLine()?.ToLower();

                            if (logOut == "yes")
                            {
                                Console.WriteLine("Logging out...");
                                MenuLogic.PopMenu();
                                adminMenuRunning = false;
                                isRunning = false; // Exit the main login loop
                                break;
                            }
                            else if (logOut == "no")
                            {
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Incorrect input, enter either 'yes' or 'no'.");
                            }
                        }
                    }
                    else if (keyInfo == "1")
                    {
                        AdminAddFlightsPresentation adminAddflight = new AdminAddFlightsPresentation();
                        FlightModel newFlight = adminAddflight.AddNewFlights();
                        FlightsAccess.AdminAddNewFlight(newFlight);
                        adminAddflight.Exit();
                    }
                    else if (keyInfo == "2")
                    {
                        // AdminFlightManagerPresentation.LaodFlightPresentaion();
                        AdminFlightManagerPresentation.UpdateDetailsPresentation();
                        AdminFlightManagerPresentation.Exit();
                    }
                    else if (keyInfo == "3")
                    {
                        LayoutModel layout = LayoutModel.CreateBoeing737Layout();
                        layout.ResetAllSeats();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();

                    }
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