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
        public static void LaodFlightPresentaion()
        {
            Console.Clear();
            var flights = AdminFlightManagerLogic.GetAllFlights();
            foreach (var flight in flights)
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
        }
        public static void UpdateDetailsPresentation()
        {

            Console.WriteLine("Enter the flight ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            var flight = AdminFlightManagerLogic.SearchFlightLogic(id);
            if (flight == null)
            {
                Console.WriteLine($"No flight founf with ID: {id}");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"1. Current Details of Flight ID: ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{id}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"2. Airline: ");


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.Airline}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"3. TicketPrice: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.TicketPrice}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"4. Gate: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.Gate}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"5. DepartureAirport: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.DepartureAirport}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"6. ArrivalDestination: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.ArrivalDestination}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"7. IsCancelled: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.IsCancelled}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"8. DepartureDate: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.DepartureDate}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"9. FlightTime: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{flight.FlightTime}");

            Console.ResetColor();

            // Console.WriteLine($"10. AvailableSeats: {flight.AvailableSeats}");
            Console.WriteLine();
            // double ticketPrice;
            // string newgateStr;
            // while (true)
            // {
            //     Console.WriteLine("Enter the number of the details that you want to change");
            //     int input = int.Parse(Console.ReadLine());
            //     if (input == 3)
            //     {
            //         Console.WriteLine("Enter new Ticket Price (leave empty to keep current):");
            //         string newTicketPrice = Console.ReadLine();
            //         if (!string.IsNullOrWhiteSpace(newTicketPrice))
            //         {

            //             if (double.TryParse(newTicketPrice, out ticketPrice))
            //             {
            //                 if (AdminFlightManagerLogic.TicketPriceLogic(ticketPrice))
            //                 {
            //                     flight.TicketPrice = ticketPrice;
            //                     Console.WriteLine("Do you want do change another thing? (y/n)");
            //                     string input1 = Console.ReadLine().ToLower();
            //                     if (input1 == "y")
            //                     {
            //                         input = 4;
            //                     }
            //                     else
            //                     {
            //                         break;
            //                     }
            //                 }
            //                 else
            //                 {
            //                     Console.WriteLine("Invalid input. Please Try Again.");
            //                 }
            //             }
            //             else
            //             {
            //                 Console.WriteLine("Invalid input. Please Try Again.");
            //             }
            //         }
            //         else
            //         {
            //             break;
            //         }
            //     }

            //     else if (input == 4)
            //     {
            //         Console.WriteLine("Enter new Gate (leave empty to keep current):");
            //         string newGate = Console.ReadLine();
            //         if (!string.IsNullOrWhiteSpace(newGate))
            //         {
            //             if (AdminFlightManagerLogic.GateLogic(newGate))
            //             {
            //                 // Substring(startIndex, length);
            //                 string letterPart = newGate.Substring(0, 1).ToUpper();
            //                 string numberPart = newGate.Substring(1);
            //                 newgateStr = $"{letterPart}{numberPart}";
            //                 flight.Gate = newgateStr;
            //                 break;
            //             }
            //             else
            //             {
            //                 Console.WriteLine("Invalid format. The string must be a letter followed by digit.");
            //             }
            //         }

            //         else
            //         {
            //             break;
            //         }

            //     }
            //     else if (input == 7)
            //     {

            //     }


            // }

            // while (true)
            // {

            //     Console.WriteLine("Enter new Airline (leave empty to keep current):");
            //     string newAirline = Console.ReadLine();
            //     if (!string.IsNullOrWhiteSpace(newAirline))
            //     {
            //         if (newAirline is string)
            //         {
            //             flight.Airline = newAirline;
            //             break;
            //         }
            //         else
            //         {
            //             Console.WriteLine("Invalid input. Please enter letters only.");
            //         }
            //     }
            //     else
            //     {
            //         break;
            //     }
            // }
            double ticketPrice;
            while (true)
            {
                Console.WriteLine("Enter new Ticket Price (leave empty to keep current):");
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
                Console.WriteLine("Enter new Gate (leave empty to keep current):");
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

            // string newdepartureAirport;
            // while (true)
            // {
            //     Console.WriteLine("Enter new Departure Airport (leave empty to keep current):");
            //     string newDepartureAirport = Console.ReadLine();
            //     if (!string.IsNullOrWhiteSpace(newDepartureAirport))
            //     {
            //         if (newDepartureAirport is string)
            //         {
            //             flight.DepartureAirport = newDepartureAirport;
            //             break;
            //         }
            //         else
            //         {
            //             Console.WriteLine("Invalid input. Please enter letters only.");
            //         }
            //     }
            //     else
            //     {
            //         break;
            //     }
            // }
            // string newArrivalDestination;
            // var europeanCapitalsAirports = new List<string>
            //     {
            //     "Amsterdam-Schiphol",
            //     "Athens-Eleftherios Venizelos",
            //     "Belgrade-Nikola Tesla",
            //     "Berlin-Brandenburg",
            //     "Brussels-Zaventem",
            //     "Bucharest-Henri Coandă",
            //     "Budapest-Ferenc Liszt",
            //     "Copenhagen-Kastrup",
            //     "Dublin-Dublin Airport",
            //     "Helsinki-Vantaa",
            //     "Lisbon-Humberto Delgado",
            //     "London-Heathrow",
            //     "Madrid-Barajas",
            //     "Oslo-Gardermoen",
            //     "Paris-Charles de Gaulle",
            //     "Prague-Václav Havel",
            //     "Rome-Fiumicino",
            //     "Stockholm-Arlanda",
            //     "Vienna-Schwechat",
            //     "Warsaw-Chopin"
            //     };
            // while (true)
            // {
            //     Console.WriteLine("Enter new Arrival Destination (leave empty to keep current):");
            //     newArrivalDestination = Console.ReadLine();
            //     if (!string.IsNullOrWhiteSpace(newArrivalDestination))
            //     {
            //         if (europeanCapitalsAirports.Contains(newArrivalDestination))
            //         {
            //             flight.ArrivalDestination = newArrivalDestination;
            //             break;
            //         }
            //         else
            //         {
            //             Console.WriteLine("Invalid input. Please enter letters only.");
            //         }
            //     }
            //     else
            //     {
            //         break;
            //     }
            // }

            bool newIsCancelled;
            while (true)
            {
                Console.WriteLine("Is the flight cancelled? (yes/no): ");
                newIsCancelled = Console.ReadLine().ToLower() == "yes";
                flight.IsCancelled = newIsCancelled;
                break;
            }
            while (true)
            {
                Console.WriteLine("Enter new Date (dd-mm-yyyy) (leave empty to keep current):");
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
                Console.WriteLine("Enter new Flight Time (leave empty to keep current):");
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
        // }
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
                    Console.WriteLine();
                    break;
                }
                else if (saveChoice.ToLower() == "no")
                {
                    Console.WriteLine("Changes not saved.");
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
            Console.WriteLine();
            Console.WriteLine("press any key...");
            Console.ReadKey();
            MenuLogic.PopMenu();

        }
    }
}
