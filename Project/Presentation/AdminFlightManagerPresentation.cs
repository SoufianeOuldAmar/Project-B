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
            var allFlights = AdminFlightManagerLogic.GetAllFlights();
            if (allFlights == null || allFlights.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No flights found.");
                Console.ResetColor();
                return;
            }

            const int pageSize = 3;
            int currentPage = 0;
            int totalPages = (int)Math.Ceiling(allFlights.Count / (double)pageSize);

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Displaying page {currentPage + 1}/{totalPages}");
                Console.WriteLine("=====================================");
                Console.ResetColor();

                // Get flights for the current page
                var flightsToDisplay = allFlights
                    .Skip(currentPage * pageSize)
                    .Take(pageSize)
                    .ToList();

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

                if (input == "N" && currentPage < totalPages - 1)
                {
                    currentPage++;
                }
                else if (input == "B" && currentPage > 0)
                {
                    currentPage--;
                }
                else if (input == "E")
                {
                    Console.Write("Enter the Flight ID to edit: ");

                    if (int.TryParse(Console.ReadLine(), out int flightId))
                    {
                        var flight = allFlights.FirstOrDefault(f => f.Id == flightId);
                        if (flight != null)
                        {
                            EditFlightDetails(flight); // Call a method to edit the flight
                            // SaveChanges(flight);
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

            double ticketPrice;
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
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please Try Again.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please Try Again.");
                    }
                }
                else
                {
                    break;
                }
            }
            string newgateStr;
            while (true)
            {
                Console.Write("Enter new Gate (leave empty to keep current): ");
                string newGate = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newGate))
                {
                    if (AdminFlightManagerLogic.GateLogic(newGate))
                    {
                        // Substring(startIndex, length);
                        string letterPart = newGate.Substring(0, 1).ToUpper();
                        string numberPart = newGate.Substring(1);
                        newgateStr = $"{letterPart}{numberPart}";
                        flight.Gate = newgateStr;
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

            bool newIsCancelled;
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
                        Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-mm-dd format.");
                    }
                }
                else
                {
                    break;
                }

            }

            string newTime;
            while (true)
            {
                Console.Write("Enter new Flight Time (leave empty to keep current): ");
                string newFlightTime = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newFlightTime))
                {
                    if (!newFlightTime.Contains(":"))
                    {
                        Console.WriteLine("Invalid format. Please use ':' as a separator between hours and minutes.");
                        continue;
                    }
                    string[] flightParts = newFlightTime.Split(':');

                    string hourStr = flightParts[0];
                    string minStr = flightParts[1].PadLeft(2, '0');

                    if (int.TryParse(hourStr, out int hour) && hour >= 0 && hour <= 23 &&
                    int.TryParse(minStr, out int minute) && minute >= 0 && minute <= 59)
                    {
                        newTime = $"{hourStr}:{minStr}";
                        flight.FlightTime = newTime;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Time, Please try again.");
                    }
                }
                else
                {
                    break;
                }
            }
            SaveChanges(flight);
        }

        public static void SaveChanges(FlightModel flight)
        {
            while (true)
            {
                Console.WriteLine("Do you want to save the changes? (yes/no): ");
                string saveChoice = Console.ReadLine();
                if (saveChoice.ToLower() == "yes")
                {
                    AdminFlightManagerLogic.SaveChangesLogic(flight);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Flight details updated and saved.");
                    Console.ResetColor();
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

        public static void Exit()
        {
            Console.Clear();
        }
    }
}
