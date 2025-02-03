using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using DataModels;

namespace DataAccess
{
    public static class AdminManageBookingPresentation
    {

        public static bool IsEmpty { get; set; } = false;

        public static void LoadBookedPresentation()
        {

            Console.Clear();

            Console.WriteLine("=== 📅 Manage the bookings ===\n");

            bool isThereBookedFlight = BookFlightLogic.CheckForBookedFlights().Item1;
            var BookedFlight = BookFlightLogic.CheckForBookedFlights().Item2;


            if (!isThereBookedFlight)
            {
                Console.WriteLine("No booked flights available");
                IsEmpty = true;
                return;
            }


            foreach (var emailBookingPair in BookedFlight)
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

                    var flight = FlightLogic.SearchFlightByID(books.FlightID);

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


        public static List<BookedFlightsModel> SearchBookedPresentation(string email) // List<BookedFlightsModel> 
        {
            List<BookedFlightsModel> bookings = BookFlightLogic.SearchByEmail(email);

            if (bookings.Count > 0)
            {
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

                    var flight = FlightLogic.SearchFlightByID(books.FlightID);

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
                return bookings;

            }
            else
            {
                return new List<BookedFlightsModel>();
            }
        }

        public static void UpdateBookedDetailsPresentation()
        {

            while (true)
            {
                // Console.Clear();
                LoadBookedPresentation();

                if (IsEmpty) return;

                Console.Write("Choose an email: ");
                string email = Console.ReadLine();

                Console.Clear();
                var bookings = SearchBookedPresentation(email);
                if (bookings.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No bookings found for the provided email.");
                    Console.ResetColor();

                    break;

                }
                else if (bookings.Count > 0 && bookings != null)
                {

                    Console.Write("Choose a FlightID: ");
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

                            var flight = FlightLogic.SearchFlightByID(selectedBooking.FlightID);

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

                            string seatStr;
                            while (true)
                            {
                                Console.WriteLine("A. Add a new seat\nC. Change an existing seat\n");
                                Console.Write("Choose an option (leave empty to keep current seat): ");
                                string choice = Console.ReadLine().ToLower();

                                if (!string.IsNullOrWhiteSpace(choice))
                                {
                                    if (choice == "a")
                                    {
                                        Console.Write("Enter the new seat: ");
                                        string seat = Console.ReadLine();
                                        bool isValidSeat = AdminManageBookingLogic.NewSeatLogic(seat, selectedBooking);

                                        if (isValidSeat)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid format. The seat must be a digit followed by a letter.");
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
                                            bool isValidSelection = AdminManageBookingLogic.CheckValidSelection(number, selectedBooking).Item1;
                                            int seatIndex = AdminManageBookingLogic.CheckValidSelection(number, selectedBooking).Item2;


                                            if (isValidSelection)
                                            {
                                                Console.Write("Enter the new seat: ");
                                                string newSeat = Console.ReadLine();
                                                if (AdminManageBookingLogic.ChangeSeatLogic(newSeat, selectedBooking, seatIndex))
                                                {
                                                    Console.WriteLine($"Seat updated successfully! New seats list: {string.Join(", ", selectedBooking.BookedSeats)}");
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid format. The seat must be a digit followed by a letter.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid selection. Please enter a valid seat number.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No seats booked.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid selection. Please enter [A] to add a seat or [C] to change a seat.");
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }



                            while (true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("A. Add a new pet\nC. Change an existing pet\n");
                                Console.Write("Choose an option (leave empty to keep current pet): ");
                                string choice = Console.ReadLine().ToLower();

                                if (!string.IsNullOrWhiteSpace(choice))
                                {
                                    if (choice == "a")
                                    {
                                        if (flight.TotalPets >= 7)
                                        {
                                            Console.WriteLine("Sorry, no more pet space available on this flight.");
                                            break;
                                        }

                                        Console.WriteLine("What type of pet do you have? (dog, cat, bunny, bird): ");
                                        string petType = Console.ReadLine()?.ToLower();

                                        Console.WriteLine("Enter your animal's name: ");
                                        string petName = Console.ReadLine();

                                        if (AdminManageBookingLogic.IsPetTypeValid(petType))
                                        {
                                            AdminManageBookingLogic.CreateNewPet(petType, petName, selectedBooking, flight);
                                            Console.WriteLine($"Pet {petType} added. Fee: 50 EUR.");

                                            if (flight.TotalPets >= 7)
                                            {
                                                Console.WriteLine("Maximum pet capacity reached for this flight.");
                                                break;
                                            }

                                            Console.WriteLine("Do you want to add another pet? (yes/no):");
                                            string addAnother = Console.ReadLine()?.ToLower();
                                            if (addAnother != "yes")
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid pet type. Please enter 'dog', 'cat', 'bunny', or 'bird'.");
                                        }
                                    }
                                    else if (choice == "c")
                                    {
                                        if (selectedBooking.Pets.Count > 0)
                                        {
                                            Console.WriteLine("Current pets:");
                                            for (int i = 0; i < selectedBooking.Pets.Count; i++)
                                            {
                                                Console.WriteLine($"{i + 1}. {selectedBooking.Pets[i].AnimalType}");
                                            }
                                            Console.WriteLine("Enter the number of the pet you want to replace:");
                                            string number = Console.ReadLine();
                                            bool isValidSelection = AdminManageBookingLogic.CheckValidSelectionPet(number, selectedBooking).Item1;
                                            int PetIndex = AdminManageBookingLogic.CheckValidSelectionPet(number, selectedBooking).Item2;

                                            if (isValidSelection)
                                            {
                                                Console.WriteLine("What type of pet do you want to replace it with? (dog, cat, bunny, bird): ");
                                                string newPetType = Console.ReadLine().ToLower();
                                                if (AdminManageBookingLogic.IsPetTypeValid(newPetType))
                                                {
                                                    Console.WriteLine("Name of the pet: ");
                                                    string petName = Console.ReadLine();

                                                    AdminManageBookingLogic.ChangePetLogic(selectedBooking, petName, newPetType, PetIndex);


                                                    Console.WriteLine($"Pet {PetIndex} has been replaced with a {newPetType}.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid pet type. Please try again.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid number. Please try again.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No pets booked.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid selection. Please enter [A] to add a pet or [C] to change a pet.");
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }


                            // --------------


                            while (true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("A. Add new baggage\n");
                                Console.Write("Choose an option (leave empty to keep current baggage): ");
                                string choice = Console.ReadLine().ToLower();

                                if (!string.IsNullOrWhiteSpace(choice))
                                {
                                    if (choice == "a")
                                    {
                                        Console.WriteLine("Enter initials for baggage:");
                                        string initials = Console.ReadLine();
                                        Console.Write("Enter baggage type (1) carry on, (2) checked, or (3) both: ");
                                        string baggageType = Console.ReadLine().ToLower();

                                        double weightBaggage = 0;
                                        // double feeBaggage = 0;

                                        if (baggageType == "1")
                                        {
                                            Console.WriteLine("Enter weight for carry on (in kg): ");
                                            weightBaggage = double.Parse(Console.ReadLine());
                                            int isValidCarryOn = AdminManageBookingLogic.CarryOnIsValid(weightBaggage, initials, baggageType, selectedBooking);

                                            if (isValidCarryOn == 50)
                                            {
                                                Console.WriteLine($"Your carry on goes over the 10kg limit. You'll have to pay a fee of {isValidCarryOn} EUR.");
                                                break;
                                            }
                                            else if (isValidCarryOn == 0)
                                            {
                                                Console.WriteLine("Your checked baggage weight is 20 kg. No additional fee required.");
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Maximum baggage limit is 25 kg. Please try again.");
                                            }
                                        }
                                        else if (baggageType == "2" || baggageType == "3")
                                        {
                                            Console.WriteLine("Enter weight for checked baggage (choose 20 or 25 kg): ");
                                            weightBaggage = double.Parse(Console.ReadLine());
                                            var result = AdminManageBookingLogic.CheckedIsValid(weightBaggage, initials, baggageType, selectedBooking);

                                            if (result == BaggageValidationResult.Valid20Kg)
                                            {
                                                Console.WriteLine("Your checked baggage weight is 20 kg. No additional fee required.");
                                                break;
                                            }
                                            else if (result == BaggageValidationResult.Valid25Kg)
                                            {
                                                Console.WriteLine("Your checked baggage weight is 25 kg. No additional fee required.");
                                                break;
                                            }
                                            else if (result == BaggageValidationResult.Overweight)
                                            {
                                                Console.WriteLine($"Your checked baggage weight exceeds the 25 kg limit. You'll have to pay a fee of 50 EUR.");
                                                break;
                                            }
                                            else if (result == BaggageValidationResult.InvalidWeight)
                                            {
                                                Console.WriteLine("Invalid weight. Please enter a valid weight for your checked baggage.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid baggage type. Please enter 1, 2, or 3.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid selection. Please enter [A] to add baggage.");
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"No booking found with FlightID: {chosenFlightID} for the given email.");
                            Console.ResetColor();
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("This email has no booking with this flightID. Please try again");
                    }
                }
                else
                {
                    Console.WriteLine("No booking foud. pleas try again!!!!");
                }

                Saving(email, bookings);
                NotificationLogic.NotifyBookingModification(email, bookings);

                break;
            }
        }

        public static void Saving(string email, List<BookedFlightsModel> bookings)
        {
            while (true)
            {
                Console.Write("Do you want to save the changes? (yes/no): ");
                string answer = Console.ReadLine().ToLower();
                if (answer == "yes")
                {
                    AdminManageBookingLogic.SaveBookingData(email, bookings);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Saving...");
                    Console.ResetColor();
                    break;
                }
                else if (answer == "no")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again");
                }
            }
        }
    }
}
