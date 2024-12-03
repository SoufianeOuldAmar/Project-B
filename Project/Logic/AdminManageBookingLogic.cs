namespace DataAccess
{

    public static class AdminManageBookingLogic
    {
        public static List<BookedFlightsModel> AdminSearchBooking(string email)
        {
            var flightDeatails = FlightsAccess.ReadAll();
            var BookdeFlight = BookedFlightsAccess.LoadAll();
            if (BookdeFlight.ContainsKey(email))
            {
                foreach (var books in BookdeFlight[email])
                {


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  FlightID: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(books.FlightID);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Ticket Price: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(books.TicketBill);
                    // Console.WriteLine($"Ticket Price: {books.TicketBill}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Booked Seats: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", books.BookedSeats));
                    // Console.WriteLine($"  Booked Seats: {string.Join(", ", books.BookedSeats)}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Pets: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", books.Pets));
                    // Console.WriteLine($"  Pets: {string.Join(", ", books.Pets)}");

                    // Console.WriteLine($"  Baggage Info: {string.Join(", ", books.BaggageInfo)}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Baggage Info: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", books.BaggageInfo));

                    // Console.WriteLine($"  Is Cancelled: {books.IsCancelled}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Is Cancelled: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(books.IsCancelled);

                    var flight = flightDeatails.Find(f => f.Id == books.FlightID);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Date: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(flight.DepartureDate);

                    // Console.WriteLine($"  Date: {flight.DepartureDate}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Time: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(flight.FlightTime);
                    // Console.WriteLine($"  Time: {flight.FlightTime}");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("=========================================");
                    Console.ResetColor();
                    Console.WriteLine();
                }

            }
            return null;
        }
    }
}