using System.Collections.Generic;

public static class BookFlightPresentation
{
    public static List<FlightModel> allFlights = FlightsAccess.ReadAll();
    public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();
    public static void BookFlightMenu(AccountModel? accountModel = null)
    {
        while (true)
        {
            Console.WriteLine("=== Book Ticket ===\n");
            if (allFlights.Count == 0)
            {
                Console.WriteLine("No flights available.");
                return;
            }

            Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-12} {6,-30}",
                              "ID", "Airline", "Departure Airport", "Arrival Destination",
                              "Flight Time", "Cancelled", "Ticket Price");
            Console.WriteLine(new string('-', 200));

            foreach (var flight in allFlights)
            {
                Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-12} {6,-30}",
                                  flight.Id,
                                  flight.Airline,
                                  flight.DepartureAirport,
                                  flight.ArrivalDestination,
                                  flight.FlightTime,
                                  flight.IsCancelled ? "Yes" : "No",
                                  $"€ {flight.TicketPrice},-");
            }

            Console.WriteLine("\n" + new string('-', 200));
            Console.Write("\nEnter the ID of the flight you wish to book (or press 'Q' to quit the booking process): ");

            string idInput = Console.ReadLine();

            if (idInput.ToUpper() == "Q")
            {
                Console.WriteLine("Exiting booking process menu. Press any key to continue.");
                Console.ReadKey();
                MenuLogic.PopMenu();
                break;
            }

            if (int.TryParse(idInput, out int selectedId))
            {
                var selectedFlight = BookFlightLogic.SearchFlightByID(selectedId);
                if (selectedFlight != null)
                {
                    Console.WriteLine($"\nYou have selected the following flight:\n");
                    Console.WriteLine("{0,-20} {1,-35}", "Airline:", selectedFlight.Airline);
                    Console.WriteLine("{0,-20} {1,-35}", "Departure Airport:", selectedFlight.DepartureAirport);
                    Console.WriteLine("{0,-20} {1,-35}", "Arrival Destination:", selectedFlight.ArrivalDestination);
                    Console.WriteLine("{0,-20} {1,-35}", "Flight Time:", selectedFlight.FlightTime);
                    Console.WriteLine("{0,-20} {1,-35}", "Cancelled:", (selectedFlight.IsCancelled ? "Yes" : "No"));
                    Console.WriteLine("{0,-20} {1,-35}", "Ticket Price:", ($"€ {selectedFlight.TicketPrice},-"));


                    Console.Write("\nAre you sure you want to book this flight? (yes/no) ");
                    string confirmation = Console.ReadLine();
                    // TODO: Make more people be able to choose a seat and when, multiple seats are chosen, increase those points to that amount.
                    if (confirmation == "yes")
                    {

                        // Seat selection process
                        List<string> chosenSeats = new List<string>();
                        selectedFlight.Layout.PrintLayout(); // Print the initial layout

                        while (true)
                        {   
                            Console.Write("\nWhich seat do you want? (press Q to quit or Enter to confirm booking and keep choosing by seat number if you want more seats): ");
                            string seat = Console.ReadLine();

                            if (seat == "Q")
                            {
                                break; // Exit seat selection
                            }
                            else if (string.IsNullOrWhiteSpace(seat))
                            {
                                Console.WriteLine("Confirming your selected seats...");
                                selectedFlight.Layout.ConfirmBooking(); // Confirm booking (turn yellow seats to red)
                                AccountsLogic.CurrentAccount.FlightPoints += selectedFlight.FlightPoints;
                                break;
                            }

                            // Book the seat temporarily (yellow)
                            selectedFlight.Layout.BookFlight(seat);

                            // Clear console and reprint the layout to show updates
                            Console.Clear();
                            selectedFlight.Layout.PrintLayout(); // Show updated layout with chosen seats in yellow
                        }

                        List<BookedFlightsModel> bookedFlightModel = new List<BookedFlightsModel>
                        {
                            new BookedFlightsModel(selectedFlight.Id, selectedFlight.Layout.BookedSeats, false)
                        };
                        var currentAccount = AccountsLogic.CurrentAccount;
                        FlightsAccess.WriteAll(allFlights);
                        BookedFlightsAccess.WriteAll(currentAccount.EmailAddress, bookedFlightModel);
                        // Optionally, save booked seats to an external source (e.g., file, database) after confirming
                        // SaveBookedSeats(accountModel.Email, selectedFlight.Id, chosenSeats);

                    }
                    else if (confirmation == "no")
                    {
                        Console.WriteLine("Booking flight is cancelled, choose a new flight.\n");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, try again.\n");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                }

                else
                {
                    Console.WriteLine("Invalid ID selected. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric ID.");
            }
        }
    }

    public static void CancelBookedFlightMenu()
    {
        Console.WriteLine("=== Cancel Booked Flight ===\n");

        var currentAccount = AccountsLogic.CurrentAccount;
        var email = currentAccount.EmailAddress;

        // Check if the user has booked flights
        if (!allBookedFlights.ContainsKey(email) || allBookedFlights[email].Count == 0)
        {
            Console.WriteLine("No booked tickets.");
            return;
        }

        // Create a list to hold the booked flights
        var bookedFlightModels = new List<FlightModel>();
        var bookedFlights = allBookedFlights[email]; // Get booked flights for the current user
        int index = 0;

        // Populate bookedFlightModels with the corresponding FlightModels
        foreach (var bookedFlightModel in bookedFlights)
        {
            // Fetching the flight model using the booked flight ID
            var flightModel = BookFlightLogic.SearchFlightByID(bookedFlightModel.FlightID);
            if (flightModel != null)
            {
                bookedFlightModels.Add(flightModel);
            }
        }

        // Display the booked flights
        if (bookedFlightModels.Count > 0)
        {
            Console.WriteLine("Your booked flights:\n");
            Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-28} {6, -27} {7,-18} {8,-29}",
                              "ID", "Airline", "Departure Airport", "Arrival Destination",
                              "Flight Time", "Cancelled by airline", "Cancelled by customer", "Ticket Price", "Flight Points");

            Console.WriteLine(new string('-', 275));

            foreach (var flight in bookedFlightModels)
            {
                Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-28} {6, -27} {7,-18} {8,-29}",
                                  flight.Id,
                                  flight.Airline,
                                  flight.DepartureAirport,
                                  flight.ArrivalDestination,
                                  flight.FlightTime,
                                  flight.IsCancelled ? "Yes" : "No",
                                  bookedFlights[index].IsCancelled ? "Yes" : "No",
                                  flight.TicketPrice,
                                  flight.FlightPoints);
                index++;
            }

            // Prompt user to select a flight to cancel
            Console.Write("\nEnter the ID of the flight you wish to cancel (or 'Q' to quit): ");
            string input = Console.ReadLine();

            if (input.ToUpper() == "Q")
            {
                Console.WriteLine("Exiting cancellation process.");
                MenuLogic.PopMenu();
                return; // Exit the cancellation process
            }

            if (int.TryParse(input, out int flightToCancelId))
            {
                // Find the booked flight model to cancel
                var bookedFlightToCancel = bookedFlights.FirstOrDefault(b => b.FlightID == flightToCancelId);
                if (bookedFlightToCancel != null)
                {
                    // Update the IsCancelled property
                    bookedFlightToCancel.IsCancelled = true;
                    Console.WriteLine($"Flight ID {flightToCancelId} has been successfully cancelled.");
                    // BookFlightLogic.CancelBookedFlight(allBookedFlights);
                }
                else
                {
                    Console.WriteLine("Invalid Flight ID. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric ID or 'Q' to quit.");
            }
        }
        else
        {
            Console.WriteLine("You have no booked flights.");
        }
    }
}
