using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using DataModels;

namespace DataAccess
{
    public static class AdminFlightManagerPresentation
    {
        public static void UpdateDetailsPresentation()
        {

            if (AdminFlightManagerLogic.CheckForFlights())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No flights found.");
                Console.ResetColor();
                return;
            }

            const int pageSize = 3;
            int currentPage = 0;
            int totalPages = AdminFlightManagerLogic.CalculatePages(pageSize);

            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== ✏️  Change current flight details ===\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Displaying page {currentPage + 1}/{totalPages}");
                Console.WriteLine("=====================================");
                Console.ResetColor();

                // Get flights for the current page
                var flightsToDisplay = AdminFlightManagerLogic.GetFlightsForPage(currentPage, pageSize);

                foreach (var flight in flightsToDisplay)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Flight ID: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.Id}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Airline: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.Airline}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Ticket Price: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.TicketPrice}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Gate: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.Gate}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Departure Airport: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.DepartureAirport}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Arrival Destination: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.ArrivalDestination}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Is Cancelled: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.IsCancelled}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Departure Date: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.DepartureDate}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Flight Time: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.FlightTime}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Available Seats: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.AvailableSeats}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Flight Points: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{flight.FlightPoints}");

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("-----------------------------------------");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nOptions:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("N - Next Page");
                Console.WriteLine("B - Previous Page");
                Console.WriteLine("E - Edit a flight by ID");
                Console.WriteLine("Q - Quit\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter your choice: ");
                Console.ResetColor();

                string input = Console.ReadLine().ToUpper();

                if (input == "E")
                {
                    Console.Write("Enter the Flight ID to edit: ");

                    if (int.TryParse(Console.ReadLine(), out int flightId))
                    {
                        FlightModel flight = FlightLogic.SearchFlightByID(flightId);
                        if (flight != null)
                        {
                            EditFlightDetails(flight);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Flight not found.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid ID.");
                        Console.ResetColor();
                    }
                }
                else if (input == "Q")
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option.");
                    Console.ResetColor();
                }

                currentPage = AdminFlightManagerLogic.ChangePage(currentPage, totalPages, input);
            }
        }


        public static void EditFlightDetails(FlightModel flight)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"1. Current Details of Flight ID: ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{flight.Id}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"2. TicketPrice: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.TicketPrice}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"3. Gate: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.Gate}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"4. IsCancelled: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.IsCancelled}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"5. DepartureDate: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.DepartureDate}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"6. FlightTime: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.FlightTime}");

            Console.ResetColor();

            // Console.WriteLine($"10. AvailableSeats: {flight.AvailableSeats}");
            Console.WriteLine();

            List<double> ticketPriceChange = new List<double> { flight.TicketPrice };
            List<string> gateChange = new List<string> { flight.Gate };
            List<string> newTimeChange = new List<string> { flight.FlightTime };


            double ticketPrice = 0;
            while (true)
            {
                Console.Write("Enter new Ticket Price (leave empty to keep current): ");
                string newTicketPrice = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(newTicketPrice))
                {

                    if (double.TryParse(newTicketPrice, out ticketPrice))
                    {
                        if (AdminFlightManagerLogic.TicketPriceLogic(ticketPrice))
                        {
                            flight.TicketPrice = ticketPrice;
                            ticketPriceChange.Add(ticketPrice);
                            break;
                        }
                        else
                        {
                            MenuPresentation.PrintColored("Invalid input. Please try Again.", ConsoleColor.Red);
                        }

                    }
                    else
                    {
                        MenuPresentation.PrintColored("Invalid input. Please try Again.", ConsoleColor.Red);
                    }
                }
                else
                {
                    break;
                }
            }

            string newgateStr = "";
            while (true)
            {
                Console.Write("Enter new Gate (leave empty to keep current): ");
                string newGate = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newGate))
                {
                    if (AdminFlightManagerLogic.GateLogic(newGate))
                    {
                        newgateStr = AdminFlightManagerLogic.CreateGate(newGate);
                        flight.Gate = newgateStr;
                        gateChange.Add(newgateStr);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. The string must be a letter followed by digit.");
                    }
                }

                else
                {
                    break;
                }
            }

            bool newIsCancelled = false;
            while (true)
            {
                Console.Write("Is the flight cancelled? (yes/no): ");
                newIsCancelled = Console.ReadLine().ToLower() == "yes";
                flight.IsCancelled = newIsCancelled;
                break;
            }
            while (true)
            {
                Console.Write("Enter new Date (dd-mm-yyyy) (leave empty to keep current): ");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (AdminFlightManagerLogic.Date(input) == true)
                    {
                        flight.DepartureDate = input;
                        break;
                    }
                    else
                    {
                        MenuPresentation.PrintColored("Invalid date format. Please enter a valid date in yyyy-mm-dd format.\n", ConsoleColor.Red);
                    }
                }
                else
                {
                    break;
                }

            }

            string newTime = "";
            while (true)
            {
                Console.Write("Enter new Flight Time (leave empty to keep current): ");
                string newFlightTime = Console.ReadLine();

                if (newFlightTime == "") break;

                if (AdminAddFlightsLogic.GetFlightTime(newFlightTime).Item1)
                {
                    flight.FlightTime = AdminAddFlightsLogic.GetFlightTime(newFlightTime).Item2;
                    break;
                }

                else
                {
                    MenuPresentation.PrintColored("\nInvalid Time, Please try again.\n", ConsoleColor.Red);
                }

            }
            SaveChanges(flight, ticketPriceChange, gateChange, newIsCancelled, newTimeChange);
        }

        public static void SaveChanges(FlightModel flight, List<double> ticketPriceChange, List<string> gateChange, bool isCancelled, List<string> newTimeChange)
        {
            while (true)
            {
                Console.Write("Do you want to save the changes? (yes/no): ");
                string saveChoice = Console.ReadLine();
                if (saveChoice.ToLower() == "yes")
                {
                    AdminFlightManagerLogic.SaveChangesLogic(flight);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    NotificationLogic.NotifyFlightModification(flight.Id, ticketPriceChange, gateChange, isCancelled, newTimeChange);
                    MenuPresentation.PrintColored("Flight details updated and saved.", ConsoleColor.Green);
                    MenuPresentation.PressAnyKey();
                    break;
                }
                else if (saveChoice.ToLower() == "no")
                {
                    Console.WriteLine("\nChanges not saved.");
                    MenuPresentation.PressAnyKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
            }
        }
    }
}
