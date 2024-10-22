using System.Collections.Generic;

public static class BookFlightPresentation
{
    public static List<FlightModel> allFlights = FlightsAccess.ReadAll();
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

            Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-12}",
                              "ID", "Airline", "Departure Airport", "Arrival Destination",
                              "Flight Time", "Cancelled");
            Console.WriteLine(new string('-', 195));

            foreach (var flight in allFlights)
            {
                Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-12}",
                                  flight.Id,
                                  flight.Airline,
                                  flight.DepartureAirport,
                                  flight.ArrivalDestination,
                                  flight.FlightTime,
                                  flight.IsCancelled ? "Yes" : "No");
            }

            Console.WriteLine("\n" + new string('-', 195));
            Console.Write("\nEnter the ID of the flight you wish to book: ");

            if (int.TryParse(Console.ReadLine(), out int selectedId))
            {
                var selectedFlight = BookFlightLogic.SearchFlightByID(selectedId);
                if (selectedFlight != null)
                {
                    Console.WriteLine($"\nYou have selected the following flight:\n");
                    Console.WriteLine("{0,-20} {1,-35}", "Airline:", selectedFlight.Airline);
                    Console.WriteLine("{0,-20} {1,-35}", "Departure Airport:", selectedFlight.DepartureAirport);
                    Console.WriteLine("{0,-20} {1,-35}", "Arrival Destination:", selectedFlight.ArrivalDestination);
                    Console.WriteLine("{0,-20} {1,-35}", "Flight Time:", selectedFlight.FlightTime);
                    Console.WriteLine("{0,-20} {1,-35}", "Is Cancelled:", (selectedFlight.IsCancelled ? "Yes" : "No"));

                    Console.Write("\nAre you sure you want to book this flight? (yes/no) ");
                    string confirmation = Console.ReadLine();

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
                                break;
                            }
                            // else
                            // {   
                            //     Console.WriteLine("Invalid input");
                            //     Console.WriteLine("Press any key to continue...");
                            //     Console.ReadKey();
                            //     continue;
                            // }

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
}
