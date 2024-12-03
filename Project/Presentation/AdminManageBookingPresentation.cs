namespace DataAccess
{
    public static class AdminManageBookingPresentation
    {

        public static void LaodBookedPresentaion()
        {
            Console.Clear();
            var flightDeatails = FlightsAccess.ReadAll();
            var BookdeFlight = BookedFlightsAccess.LoadAll();

            foreach (var emailBookingPair in BookdeFlight)
            {
                string email = emailBookingPair.Key;
                var bookings = emailBookingPair.Value;

                // Console.ForegroundColor = ConsoleColor.Cyan;
                // Console.Write($"  Email: {email}");
                // Console.ForegroundColor = ConsoleColor.Green;
                // Console.WriteLine(email);
                foreach (var books in bookings)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Email: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(email);
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

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Pets: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", books.Pets));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Baggage Info: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", books.BaggageInfo));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Is Cancelled: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(books.IsCancelled);

                    var flight = flightDeatails.Find(f => f.Id == books.FlightID);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Date: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(flight.DepartureDate);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Time: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(flight.FlightTime);

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("=========================================");
                    Console.ResetColor();
                    Console.WriteLine();
                }

            }
        }


        public static List<BookedFlightsModel> SearchBookedPresentaion(string email) // List<BookedFlightsModel> 
        {
            var flightDeatails = FlightsAccess.ReadAll();
            var BookdeFlight = BookedFlightsAccess.LoadAll();
            List<BookedFlightsModel> bookings = new List<BookedFlightsModel>();

            if (BookdeFlight.ContainsKey(email))
            {
                bookings = BookdeFlight[email];
                foreach (var books in bookings)
                {

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Email: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(email);
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

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Pets: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", books.Pets));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Baggage Info: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", books.BaggageInfo));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Is Cancelled: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(books.IsCancelled);

                    var flight = flightDeatails.Find(f => f.Id == books.FlightID);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Date: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(flight.DepartureDate);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Time: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(flight.FlightTime);

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("=========================================");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No booking is found");
                Console.ResetColor();
            }
            return bookings;

        }

        public static void UpdateBookedDetailsPresentation()
        {

            var flightDeatails = FlightsAccess.ReadAll();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Choose an email... ");
            Console.ForegroundColor = ConsoleColor.Green;
            string email = Console.ReadLine();
            Console.ResetColor();
            var bookings = SearchBookedPresentaion(email);
            if (bookings.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No bookings found for the provided email.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Choose a FlighID... ");
            Console.ResetColor();
            string input = Console.ReadLine();
            Console.Clear();

            if (int.TryParse(input, out int chosenFlightID))
            {
                var selectedBooking = bookings.Find(b => b.FlightID == chosenFlightID);
                if (selectedBooking != null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Email: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(email);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  FlightID: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(selectedBooking.FlightID);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Ticket Price: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(selectedBooking.TicketBill);
                    // Console.WriteLine($"Ticket Price: {books.TicketBill}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Booked Seats: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", selectedBooking.BookedSeats));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Pets: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", selectedBooking.Pets));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Baggage Info: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", selectedBooking.BaggageInfo));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Is Cancelled: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(selectedBooking.IsCancelled);

                    var flight = flightDeatails.Find(f => f.Id == selectedBooking.FlightID);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Date: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(flight.DepartureDate);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Time: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(flight.FlightTime);

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("=========================================");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }



        }













    }

}