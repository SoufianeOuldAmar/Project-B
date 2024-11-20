using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;
using DataAccess;

public static class BookFlightPresentation
{
    public static List<FlightModel> allFlights = FlightsAccess.ReadAll();
    public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();
    public static void BookFlightMenu(bool searchFlightFunction = false, FlightModel flightModel = null)
    {   
        var currentAccount = AccountsLogic.CurrentAccount;

        if (!searchFlightFunction)
        {
            while (true)
            {
                Console.WriteLine("=== Book Ticket ===\n");
                if (allFlights.Count == 0)
                {
                    Console.WriteLine("No flights available.");
                    return;
                }

                Console.WriteLine("{0,-5} {1,-25} {2,-46} {3,-58} {4,-15} {5,-14} {6,-14} {7,-15}",
                                  "ID", "Airline", "Departure Airport", "Arrival Destination",
                                  "Flight Date", "Flight Time", "Ticket Price", "Cancelled");
                Console.WriteLine(new string('-', 195));

                foreach (var flight in allFlights)
                {
                    Console.WriteLine("{0,-5} {1,-25} {2,-46} {3,-58} {4,-15} {5,-14} {6,-14} {7,-15}",
                                      flight.Id,
                                      flight.Airline,
                                      flight.DepartureAirport,
                                      flight.ArrivalDestination,
                                      flight.DepartureDate,
                                      flight.FlightTime,
                                      flight.TicketPrice,
                                      flight.IsCancelled ? "Yes" : "No");
                }

                Console.WriteLine("\n" + new string('-', 195));
                Console.Write("\nEnter the ID of the flight you wish to book (or 'Q' to quit): ");
            
                var input = Console.ReadLine();
                if (input?.ToUpper() == "Q")
                {
                Console.WriteLine("Booking canceled. Returning to main menu.");
                MenuLogic.PopMenu();
                break;
                }

                if (int.TryParse(input, out int selectedId))
                {
                    var selectedFlight = BookFlightLogic.SearchFlightByID(selectedId);
                    if (selectedFlight != null)
                    {
                        Console.WriteLine($"\nYou have selected the following flight:\n");
                        Console.WriteLine("{0,-20} {1,-35}", "Airline:", selectedFlight.Airline);
                        Console.WriteLine("{0,-20} {1,-35}", "Departure Airport:", selectedFlight.DepartureAirport);
                        Console.WriteLine("{0,-20} {1,-35}", "Arrival Destination:", selectedFlight.ArrivalDestination);
                        Console.WriteLine("{0,-20} {1,-35}", "Flight Date:", selectedFlight.DepartureDate);
                        Console.WriteLine("{0,-20} {1,-35}", "Flight Time:", selectedFlight.FlightTime);
                        Console.WriteLine("{0,-20} {1,-35}", "Ticket Price:", selectedFlight.TicketPrice);
                        Console.WriteLine("{0,-20} {1,-35}", "Available Seats:", selectedFlight.AvailableSeats);
                        Console.WriteLine("{0,-20} {1,-35}", "Is Cancelled:", (selectedFlight.IsCancelled ? "Yes" : "No"));

                        Console.Write("\nAre you sure you want to book this flight? (yes/no) ");
                        string confirmation = Console.ReadLine();

                        if (confirmation.ToLower() == "yes")
                        {
                            List<string> chosenSeats = new List<string>();
                            selectedFlight.Layout.PrintLayout();

                            while (true)
                            {
                                Console.Write("\nWhich seat do you want? (press Q to quit or Enter to confirm booking and keep choosing by seat number if you want more seats): ");
                                string seat = Console.ReadLine()?.ToUpper();

                                if (seat == "Q")
                                {
                                    break;
                                }
                                else if (string.IsNullOrWhiteSpace(seat))
                                {
                                    Console.WriteLine("Confirming your selected seats...");
                                    selectedFlight.Layout.ConfirmBooking();
                                    AccountsAccess.WriteAll(AccountsLogic._accounts);
                                    break;
                                }

                                // Get user initials
                                Console.Write("Enter your initials (2 characters): ");
                                string initials = Console.ReadLine()?.ToUpper() ?? "";
                                while (initials.Length != 2)
                                {
                                    Console.Write("Please enter exactly 2 characters for your initials: ");
                                    initials = Console.ReadLine()?.ToUpper() ?? "";
                                }

                                // Book the seat with initials
                                selectedFlight.Layout.BookFlight(seat, initials);

                                Console.Clear();
                                selectedFlight.Layout.PrintLayout();
                                currentAccount.FlightPoints += selectedFlight.FlightPoints;
                            }

                            List<BookedFlightsModel> bookedFlightModel = new List<BookedFlightsModel>
                        {
                            new BookedFlightsModel(selectedFlight.Id, selectedFlight.Layout.BookedSeats, false)
                        };
                            FlightsAccess.WriteAll(allFlights);
                            BookedFlightsAccess.WriteAll(currentAccount.EmailAddress, bookedFlightModel);
                        }
                        else if (confirmation.ToLower() == "no")
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
        else
        {
            if (flightModel != null)
            {
                Console.WriteLine($"\nYou have selected the following flight:\n");
                Console.WriteLine("{0,-20} {1,-35}", "Airline:", flightModel.Airline);
                Console.WriteLine("{0,-20} {1,-35}", "Departure Airport:", flightModel.DepartureAirport);
                Console.WriteLine("{0,-20} {1,-35}", "Arrival Destination:", flightModel.ArrivalDestination);
                Console.WriteLine("{0,-20} {1,-35}", "Flight Time:", flightModel.FlightTime);
                Console.WriteLine("{0,-20} {1,-35}", "Flight Date:", flightModel.DepartureDate);
                Console.WriteLine("{0,-20} {1,-35}", "Ticket Price:", flightModel.TicketPrice);
                Console.WriteLine("{0,-20} {1,-35}", "Available Seats:", flightModel.AvailableSeats);
                Console.WriteLine("{0,-20} {1,-35}", "Is Cancelled:", (flightModel.IsCancelled ? "Yes" : "No"));

                Console.Write("\nAre you sure you want to book this flight? (yes/no) ");
                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "yes")
                {
                    List<string> chosenSeats = new List<string>();
                    flightModel.Layout.PrintLayout();

                    while (true)
                    {
                        Console.Write("\nWhich seat do you want? (press Q to quit or Enter to confirm booking and keep choosing by seat number if you want more seats): ");
                        string seat = Console.ReadLine()?.ToUpper();

                        if (seat == "Q")
                        {
                            break;
                        }
                        else if (string.IsNullOrWhiteSpace(seat))
                        {
                            Console.WriteLine("Confirming your selected seats...");
                            flightModel.Layout.ConfirmBooking();
                            break;
                        }

                        // Get user initials
                        Console.Write("Enter your initials (2 characters): ");
                        string initials = Console.ReadLine()?.ToUpper() ?? "";
                        while (initials.Length != 2)
                        {
                            Console.Write("Please enter exactly 2 characters for your initials: ");
                            initials = Console.ReadLine()?.ToUpper() ?? "";
                        }

                        // Book the seat with initials
                        flightModel.Layout.BookFlight(seat, initials);

                        Console.Clear();
                        flightModel.Layout.PrintLayout();
                    }

                    List<BookedFlightsModel> bookedFlightModel = new List<BookedFlightsModel>
                        {
                            new BookedFlightsModel(flightModel.Id, flightModel.Layout.BookedSeats, false)
                        };

                    var index = allFlights.FindIndex(f => f.Id == flightModel.Id);
                    if (index >= 0)
                    {
                        allFlights[index] = flightModel;
                    }

                    // Write allFlights to the data store to persist changes
                    FlightsAccess.WriteAll(allFlights);
                    BookedFlightsAccess.WriteAll(currentAccount.EmailAddress, bookedFlightModel);
                }
                else if (confirmation.ToLower() == "no")
                {
                    Console.WriteLine("Booking flight is cancelled, choose a new flight.\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid input, try again.\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

    }


    public static void CancelBookedFlightMenu()
    {
        Console.WriteLine("=== Cancel Booked Flight ===\n");

        var currentAccount = AccountsLogic.CurrentAccount;
        var email = currentAccount.EmailAddress;

        if (!allBookedFlights.ContainsKey(email) || allBookedFlights[email].Count == 0)
        {
            Console.WriteLine("No booked tickets.");
            return;
        }

        var bookedFlightModels = new List<FlightModel>();
        var bookedFlights = allBookedFlights[email];
        int index = 0;

        foreach (var bookedFlightModel in bookedFlights)
        {
            var flightModel = BookFlightLogic.SearchFlightByID(bookedFlightModel.FlightID);
            if (flightModel != null)
            {
                bookedFlightModels.Add(flightModel);
            }
        }

        if (bookedFlightModels.Count > 0)
        {
            Console.WriteLine("Your booked flights:\n");
            Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-28} {6, -18}",
                              "ID", "Airline", "Departure Airport", "Arrival Destination",
                              "Flight Time", "Cancelled by airline", "Cancelled by customer");

            Console.WriteLine(new string('-', 220));

            foreach (var flight in bookedFlightModels)
            {
                Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-28} {6, -18}",
                                  flight.Id,
                                  flight.Airline,
                                  flight.DepartureAirport,
                                  flight.ArrivalDestination,
                                  flight.FlightTime,
                                  flight.IsCancelled ? "Yes" : "No",
                                  bookedFlights[index].IsCancelled ? "Yes" : "No");
                index++;
            }

            Console.Write("\nEnter the ID of the flight you wish to cancel (or 'Q' to quit): ");
            string input = Console.ReadLine();

            if (input.ToUpper() == "Q")
            {
                Console.WriteLine("Exiting cancellation process.");
                MenuLogic.PopMenu();
                return;
            }

            if (int.TryParse(input, out int flightToCancelId))
            {
                var bookedFlightToCancel = bookedFlights.FirstOrDefault(b => b.FlightID == flightToCancelId);
                if (bookedFlightToCancel != null)
                {
                    bookedFlightToCancel.IsCancelled = true;
                    Console.WriteLine($"Flight ID {flightToCancelId} has been successfully cancelled.");
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

