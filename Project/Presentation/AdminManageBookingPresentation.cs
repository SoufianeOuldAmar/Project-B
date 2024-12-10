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
                else if (bookings.Count > 0 && bookings != null)
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

                                        else
                                        {

                                            Console.WriteLine("What type of pet do you have? (dog, cat, bunny, bird): ");
                                            string petType = Console.ReadLine()?.ToLower();

                                            if (petType == "dog" || petType == "cat" || petType == "bunny" || petType == "bird")
                                            {
                                                var newPet = new PetLogic(petType) { Fee = 50.0 }; // Adds a new pet with a 50 EUR fee
                                                selectedBooking.Pets.Add(newPet);
                                                flight.TotalPets++; // Increment total pets for the flight
                                                Console.WriteLine($"Pet {petType} added. Fee: 50 EUR."); // Notify the user of the fee

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


                                        }
                                    }
                                    else if (choice == "c")
                                    {
                                        if (selectedBooking.Pets.Count > 0)
                                        {
                                            Console.WriteLine("Current Pets:");
                                            for (int i = 0; i < selectedBooking.Pets.Count; i++)
                                            {
                                                Console.WriteLine($"{i + 1}. {selectedBooking.Pets[i].AnimalType}");
                                            }
                                            Console.WriteLine("Enter the number of the pet you want to replace:");
                                            string number = Console.ReadLine();
                                            if (int.TryParse(number, out int PetIndex) && PetIndex > 0 && PetIndex <= selectedBooking.Pets.Count)
                                            {

                                                Console.WriteLine("What type of pet do you want to replace it with? (dog, cat, bunny, bird): ");
                                                string newPetType = Console.ReadLine().ToLower();
                                                if (newPetType == "dog" || newPetType == "cat" || newPetType == "bunny" || newPetType == "bird")
                                                {
                                                    selectedBooking.Pets[PetIndex - 1] = new PetLogic(newPetType) { Fee = 50.0 };
                                                    Console.WriteLine("Pet update successfully!");
                                                    break;

                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid pet type. Please try again");
                                                }

                                                // Console.WriteLine($"Seat updated successfully! New seats list: {string.Join(", ", selectedBooking.Pets)}");
                                            }
                                            Console.WriteLine("Invalid number. Please try again");

                                        }
                                        else
                                        {
                                            Console.WriteLine("No pets booked.");
                                        }
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

                                Console.WriteLine("A. To add new BaggageType\n(leave empty to keep current):");
                                string userBaggage = Console.ReadLine().ToLower();
                                if (!string.IsNullOrWhiteSpace(userBaggage))
                                {
                                    if (userBaggage == "a")
                                    {
                                        Console.WriteLine("Enter Initials");
                                        string initials = Console.ReadLine();
                                        Console.Write("Enter the number for the baggage (1) carry on, (2)checked or (3)both): ");
                                        string baggageType = Console.ReadLine().ToLower();
                                        double weightBaggage = 0;
                                        double feeBaggage = 0;

                                        if (baggageType == "1")
                                        {
                                            Console.WriteLine("Enter weight for carry on (in kg): ");
                                            weightBaggage = double.Parse(Console.ReadLine());

                                            if (weightBaggage > 0 && weightBaggage <= 20)
                                            {
                                                feeBaggage = 45;
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine($"Your carry on goes over the 10kg limit. you'll have to pay a fee of {feeBaggage} EUR.");
                                                Console.ResetColor();

                                                var newBaggage = new BaggageLogic(initials, baggageType, weightBaggage) { Fee = feeBaggage };
                                                selectedBooking.BaggageInfo.Add(newBaggage);


                                                break;
                                            }

                                            else if (weightBaggage > 20 && weightBaggage <= 25)
                                            {
                                                feeBaggage = 50;
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine($"Your carry on goes over the 10kg limit. you'll have to pay a fee of {feeBaggage} EUR.");
                                                Console.ResetColor();
                                                var newBaggage = new BaggageLogic(initials, baggageType, weightBaggage) { Fee = feeBaggage };
                                                selectedBooking.BaggageInfo.Add(newBaggage);
                                                break;
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Maximum baggage limit is 25 kg. Pleas try again");
                                                Console.ResetColor();
                                            }

                                        }
                                        else if (baggageType == "2" || baggageType == "3")
                                        {


                                            Console.WriteLine("Enter weight for checked baggage choose 20 or 25(in kg): ");
                                            weightBaggage = double.Parse(Console.ReadLine());

                                            if (weightBaggage == 20)
                                            {
                                                Console.WriteLine("Your checked baggage weight is 20 kg. No additional fee  required.");
                                                var newBaggage = new BaggageLogic(initials, baggageType, weightBaggage) { Fee = feeBaggage };
                                                selectedBooking.BaggageInfo.Add(newBaggage);
                                                break;
                                            }
                                            else if (weightBaggage == 25)
                                            {
                                                Console.WriteLine("Your checked baggage weight is 25 kg. No additional fee is required.");
                                                var newBaggage = new BaggageLogic(initials, baggageType, weightBaggage) { Fee = feeBaggage };
                                                selectedBooking.BaggageInfo.Add(newBaggage);
                                                break;
                                            }
                                            else if (weightBaggage > 25)
                                            {
                                                feeBaggage = 50;
                                                Console.WriteLine($"Your checked baggage weight exceeds the 25 kg limit. You'll have to pay a fee of {feeBaggage} EUR.");
                                                var newBaggage = new BaggageLogic(initials, baggageType, weightBaggage) { Fee = feeBaggage };
                                                selectedBooking.BaggageInfo.Add(newBaggage);
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid weight. Please enter a valid weight for your checked baggage.");

                                            }
                                            // 
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid weight");
                                        }


                                        // if (selectedBooking.BaggageInfo == null)
                                        // {
                                        //     selectedBooking.BaggageInfo = new List<BaggageLogic>();
                                        // }
                                        // string initials = selectedBooking.BaggageInfo.Count > 0
                                        //     ? selectedBooking.BaggageInfo[0].Initials
                                        //     : "Default";

                                        // selectedBooking.BaggageInfo.Add(new BaggageLogic(initials, baggageType, weightBaggage) { Fee = feeBaggage });
                                        // Console.WriteLine("Baggage added successfully!");


                                        // string initials = selectedBooking.BaggageInfo[0].Initials;

                                        // selectedBooking.BaggageInfo.Add(new BaggageLogic(initials, baggageType, weightBaggage) { Fee = feeBaggage });


                                    }
                                    else
                                    {
                                        Console.WriteLine("Ivalid input. Please try again");
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
                saving(email, bookings);
                break;

            }


        }
        public static void saving(string email, List<BookedFlightsModel> bookings)
        {
            while (true)
            {
                Console.WriteLine("Do you want to save the changes?");
                string answer = Console.ReadLine().ToLower();
                if (answer == "yes")
                {
                    BookedFlightsAccess.Save(email, bookings);
                    Console.ForegroundColor = ConsoleColor.Red;
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




        // public static void Another()
        // {
        //     Console.WriteLine("Do you want to manage another booking?");
        //     string input1 = Console.ReadLine().ToLower();
        //     if (input1 == "yes")
        //     {
        //         UpdateBookedDetailsPresentation();
        //     }
        //     else if (input1 == "no")
        //     {
        //         return;
        //     }
        //     else
        //     {
        //         Console.WriteLine("Invalid input. Please try again");
        //     }
        // }






    }

}
