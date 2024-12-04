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
                    Console.WriteLine(string.Join(", ", books.Pets.Select(p => $"{p.AnimalType} (Fee: {p.Fee})")));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Baggage Info: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", books.BaggageInfo.Select(b => $"{b.BaggageWeight}kg ({b.BaggageType}, Fee: {b.Fee})")));


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
                    Console.WriteLine(string.Join(", ", books.Pets.Select(p => $"{p.AnimalType} (Fee: {p.Fee})")));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  Baggage Info: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(", ", books.BaggageInfo.Select(b => $"{b.BaggageWeight}kg ({b.BaggageType}, Fee: {b.Fee})")));


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
            return bookings;

        }

        public static void UpdateBookedDetailsPresentation()
        {
            var BookdeFlight = BookedFlightsAccess.LoadAll();
            var flightDeatails = FlightsAccess.ReadAll();
            // while (true)
            // {
            //     Console.WriteLine("Choose an email... ");
            //     string email = Console.ReadLine();
            //     Console.Clear();
            //     var bookings = SearchBookedPresentaion(email);
            //     if (bookings.Count == 0)
            //     {
            //         Console.ForegroundColor = ConsoleColor.Red;
            //         Console.WriteLine("No bookings found for the provided email.");
            //         Console.ResetColor();
            //         break;
            //     }

            // }
            while (true)
            {

                Console.WriteLine("Choose an email... ");
                string email = Console.ReadLine();
                Console.Clear();
                var bookings = SearchBookedPresentaion(email);
                if (bookings.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No bookings found for the provided email.");
                    Console.ResetColor();
                    break;
                }
                else
                {

                    Console.WriteLine("Choose a FlighID... ");
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
                            Console.WriteLine(string.Join(", ", selectedBooking.Pets.Select(p => $"{p.AnimalType} (Fee: {p.Fee})")));

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write($"  Baggage Info: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(string.Join(", ", selectedBooking.BaggageInfo.Select(b => $"{b.BaggageWeight}kg ({b.BaggageType}, Fee: {b.Fee})")));


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

                            Console.WriteLine("");

                            string seatStr;
                            while (true)
                            {
                                Console.WriteLine("A. To add new Seat\nC. To change the seat\n(leave empty to keep current):");
                                string choice = Console.ReadLine().ToLower();
                                if (!string.IsNullOrWhiteSpace(choice))
                                {
                                    if (choice == "a")
                                    {
                                        Console.WriteLine("Enter a new seat");
                                        string seat = Console.ReadLine();
                                        if (AdminManageBookingLogic.SeatLogic(seat))
                                        {
                                            // Substring(startIndex, length);
                                            string letterPart = seat.Substring(1).ToUpper();
                                            string numberPart = seat.Substring(0, 1);
                                            seatStr = $"{numberPart}{letterPart}";
                                            selectedBooking.BookedSeats.Add(seatStr);
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid format. The string must be a digit followed by letter.");
                                        }
                                    }
                                    else if (choice == "c")
                                    {

                                        if (selectedBooking.BookedSeats.Count > 0)
                                        {
                                            Console.WriteLine("Current booked seats:");
                                            for (int i = 0; i < selectedBooking.BookedSeats.Count; i++)
                                            {
                                                Console.WriteLine($"{i + 1}. {selectedBooking.BookedSeats[i]}");
                                            }
                                            Console.WriteLine("Enter the number of the seat you want to replace:");

                                            string number = Console.ReadLine();
                                            if (int.TryParse(number, out int seatIndex) && seatIndex > 0 && seatIndex <= selectedBooking.BookedSeats.Count)
                                            {
                                                Console.WriteLine("Enter a new seat");
                                                string newSeat = Console.ReadLine();
                                                if (AdminManageBookingLogic.SeatLogic(newSeat))
                                                {

                                                    string letterPart = newSeat.Substring(1).ToUpper();
                                                    string numberPart = newSeat.Substring(0, 1);
                                                    newSeat = $"{numberPart}{letterPart}";
                                                    selectedBooking.BookedSeats[seatIndex - 1] = newSeat;
                                                    Console.WriteLine($"Seat updated successfully! New seats list: {string.Join(", ", selectedBooking.BookedSeats)}");
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid format. The string must be a digit followed by letter.");
                                                }


                                                //Save
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid selection. Please enter a valid seat number.");
                                            }

                                        }
                                        Console.WriteLine("No seat to update.");
                                        break;
                                    }
                                    Console.WriteLine("Invalid selection. Please enter [a] or [c]");

                                }
                                else
                                {
                                    break;
                                }
                            }


                            while (true)
                            {
                                Console.WriteLine("A. To add new Pet\nC. To change the Pet\n(leave empty to keep current):");

                            }





                        }
                    }
                }



            }




        }













    }

}