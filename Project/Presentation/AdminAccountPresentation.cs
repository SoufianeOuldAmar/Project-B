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

                MenuPresentation.PressAnyKey();

                Console.Clear();

                bool adminMenuRunning = true; // Flag for admin menu loop
                while (adminMenuRunning)
                {
                    Console.WriteLine("=== 🔧 Admin Page ===\n");
                    Console.WriteLine("1. ➕ Add a new flight");
                    Console.WriteLine("2. ✏️  Change current flight details");
                    Console.WriteLine("3. 🔄 Reset all flights");
                    Console.WriteLine("4. 📅 Manage the bookings");
                    Console.WriteLine("5. 👀 View Feedback");
                    Console.WriteLine("6. 👤 Review employee(s)");
                    Console.WriteLine("7. 🔓 Log out");

                    Console.Write("\nChoose an option: ");
                    string keyInfo = Console.ReadLine();

                    if (keyInfo == "1")
                    {
                        AdminAddFlightsPresentation.AddNewFlightsMenu();
                        Console.Clear();
                    }
                    else if (keyInfo == "2")
                    {
                        // AdminFlightManagerPresentation.LaodFlightPresentaion();
                        AdminFlightManagerPresentation.UpdateDetailsPresentation();
                        Console.Clear();
                    }
                    else if (keyInfo == "3")
                    {
                        LayoutLogic.ResetAllSeats();
                        MenuPresentation.PressAnyKey();
                        Console.Clear();
                    }
                    else if (keyInfo == "4")
                    {
                        // AdminManageBookingPresentation.LaodBookedPresentaion();
                        AdminManageBookingPresentation.UpdateBookedDetailsPresentation();
                        // AdminManageBookingPresentation.Another();
                        MenuPresentation.PressAnyKey();
                        Console.Clear();
                    }
                    else if (keyInfo == "5")
                    {
                        FeedbackPresentation.ViewFeedbackMenu();
                        Console.Clear();
                        // break;
                    }
                    else if (keyInfo == "6")
                    {
                        AdminManageEmployeesPresentation.DisplayEmployeesInfo();
                        MenuPresentation.PressAnyKey();
                        Console.Clear();
                        // break;

                    }
                    else if (keyInfo == "7")
                    {
                        while (true)
                        {
                            Console.Write("\nAre you sure you want to log out? (yes/no): ");
                            string logOut = Console.ReadLine()?.ToLower();

                            if (logOut == "yes")
                            {
                                Console.WriteLine("\nLogging out...");
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
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                        MenuPresentation.PressAnyKey();
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
