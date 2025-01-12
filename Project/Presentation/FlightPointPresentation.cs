public static class FlightPointPresentation
{
    public static void ViewFlightPointsMenu()
    {
        Console.WriteLine("=== 🎯 View Flight Points ===\n");
        // var currentAccount = AccountsLogic.CurrentAccount;


        string email = MenuPresentation.currentAccount.EmailAddress; // Get the email of the current account

        // Load the booked flights for this specific email
        var bookedFlights = BookedFlightsAccess.LoadByEmail(email);

        if (bookedFlights.Count > 0)
        {
            Console.WriteLine(new string('-', 105));
            // Header
            Console.WriteLine("| {0,-8} | {1,-22} | {2,-17} | {3,-22}  | {4,-18} |",
                              "Index", "Flight Points Earned",
                              "Flight ID", "Tickets Bought", "Total Flight Points");
            Console.WriteLine(new string('-', 105)); // Divider with adjusted width

            int totalFlightPoints = 0;
            int index = 1;

            // Rows
            foreach (var bookedFlight in bookedFlights)
            {
                totalFlightPoints += bookedFlight.FlightPoints;

                Console.WriteLine("| {0,-8} | {1,-22} | {2,-17} | {3,-22}  | {4,-18}  |",
                                  index,
                                  bookedFlight.FlightPoints,
                                  bookedFlight.FlightID,
                                  bookedFlight.BookedSeats.Count, // Assuming TicketsBought is the number of seats
                                  ""); // Empty for total column in rows
                index++;
            }

            Console.WriteLine(new string('-', 105));

            // Total Row
            Console.WriteLine("| {0,-8} | {1,-22} | {2,-17} | {3,-22}  | {4,-18}  |",
                              "", // Empty for other columns
                              "",
                              "",
                              "",
                              "🏆 " + totalFlightPoints);

            Console.WriteLine(new string('-', 105) + "\n");
        }
        else
        {
            Console.WriteLine("You have zero booked flights so there is no transaction history.\n");
        }


        // Prompt to go back
        while (true)
        {
            Console.Write("Press 'Q' to go back: ");
            string input = Console.ReadLine().ToUpper();

            if (input != "Q")
            {
                Console.WriteLine("Wrong input try again, press 'Q' to go back");
                continue;
            }
            else
            {
                MenuLogic.PopMenu();
                break;
            }
        }
    }
}