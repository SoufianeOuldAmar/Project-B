public static class AdminManageBookingPresentation
{
    public static void AdminBookingManager()
    {

        var AllBooking = BookedFlightsAccess.LoadAll();
        if (AllBooking.Count == 0)
        {
            Console.WriteLine("No bookings founf");
        }
        else
        {
            foreach (var email in AllBooking.Keys)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Email: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(email);


                var Bookings = AllBooking[email];
                foreach (var booking in Bookings)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"FlightID:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(booking.FlightID);

                    // Console.WriteLine($"Booked seats: {string.Join(", ", booking.BookedSeats)}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Booked seats: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", booking.BookedSeats));

                    // Console.WriteLine($"Pets: {string.Join(", ", booking.Pets)}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Pets: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", booking.Pets));

                    // Console.WriteLine($"BaggageInfo: {string.Join(", ", booking.BaggageInfo)}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("BaggageInfo: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", booking.BaggageInfo));
                    // Console.WriteLine($"IsCancellled: {booking.IsCancelled}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("IsCancelled: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(booking.IsCancelled);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("-----------------------------------------");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
        }
        Console.ResetColor();
    }

    public static void UpdatBooking()
    {

        Console.WriteLine("Enter an email");
        string email = Console.ReadLine();
        Console.Clear();
        var bookedFlight = AdminManageBookingLogic.AdminSearchBooking(email);
        if (bookedFlight == null || bookedFlight.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No bookings found for the email: {email}");
            Console.ResetColor();
            return; // Exit the method if no bookings are found
        }
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write($"The booking details for the given email: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(email);
        foreach (var booking in bookedFlight)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"FlightID:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(booking.FlightID);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Booked seats: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Join(", ", booking.BookedSeats));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Pets: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Join(", ", booking.Pets));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("BaggageInfo: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Join(", ", booking.BaggageInfo));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("IsCancelled: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(booking.IsCancelled);

            Console.ResetColor();
            Console.WriteLine();

        }

        while (true)
        {
            Console.WriteLine("Enter new Ticket Price (leave empty to keep current):");
        }



    }

}
