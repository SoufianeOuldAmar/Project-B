using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using DataModels;

namespace DataAccess
{
    // AdminFlightManagerPresentation.UpdateDetailsPresentation()
    public static class AdminFlightManagerPresentation
    {
        public static void UpdateDetailsPresentation(int id)
        {
            Console.Clear();
            var flight = AdminFlightManagerLogic.SearchFlightLogic(id);
            if (flight == null)
            {
                Console.WriteLine($"No flight founf with ID: {id}");
                return;
            }

            Console.WriteLine($"1. Current Details of Flight ID {id}:");
            Console.WriteLine($"2. Airline: {flight.Airline}");
            Console.WriteLine($"3. TicketPrice: {flight.TicketPrice}");
            Console.WriteLine($"4. Gate: {flight.Gate}");
            Console.WriteLine($"5. DepartureAirport: {flight.DepartureAirport}");
            Console.WriteLine($"6. ArrivalDestination: {flight.ArrivalDestination}");
            Console.WriteLine($"7. IsCancelled: {flight.IsCancelled}");
            Console.WriteLine($"8. DepartureDate: {flight.DepartureDate}");
            Console.WriteLine($"9. FlightTime: {flight.FlightTime}");
            // Console.WriteLine($"10. AvailableSeats: {flight.AvailableSeats}");

            while (true)
            {

                Console.WriteLine("Enter new Airline (leave empty to keep current):");
                string newAirline = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newAirline))
                {
                    if (newAirline is string)
                    {
                        flight.Airline = newAirline;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter letters only.");
                    }
                }
                else
                {
                    break;
                }
            }
            double ticketPrice;
            while (true)
            {
                Console.WriteLine("Enter new Ticket Price (leave empty to keep current):");
                string newTicketPrice = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTicketPrice))
                {

                    if (double.TryParse(newTicketPrice, out ticketPrice) && ticketPrice > 0)
                    {
                        flight.TicketPrice = ticketPrice;

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter letters only.");
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
                    if (newGate.Length >= 2 && newGate.Length <= 3 && "ABCDEF".Contains(char.ToUpper(newGate[0])) &&
                    int.TryParse(newGate.Substring(1), out int number) &&
                    number >= 1 && number <= 30)
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
            string newdepartureAirport;
            while (true)
            {
                Console.WriteLine("Enter new Departure Airport (leave empty to keep current):");
                string newDepartureAirport = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDepartureAirport))
                {
                    if (newDepartureAirport is string)
                    {
                        flight.DepartureAirport = newDepartureAirport;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter letters only.");
                    }
                }
                else
                {
                    break;
                }
            }
            string newArrivalDestination;
            while (true)
            {
                Console.WriteLine("Enter new Arrival Destination (leave empty to keep current):");
                newArrivalDestination = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newArrivalDestination))
                {
                    if (newArrivalDestination is string)
                    {
                        flight.ArrivalDestination = newArrivalDestination;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter letters only.");
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
                if (!string.IsNullOrWhiteSpace(newArrivalDestination))
                {
                    Console.WriteLine("Is the flight cancelled? (yes/no): ");
                    newIsCancelled = Console.ReadLine().ToLower() == "yes";
                }
                else
                {
                    break;
                }
            }

            string newDate;
            while (true)
            {
                Console.WriteLine("Enter new Date (dd-mm-yyyy) (leave empty to keep current):");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    string[] NewdateParts = input.Split('-');
                    if (NewdateParts.Length == 3)
                    {
                        string yearStr = NewdateParts[2];
                        string monthStr = NewdateParts[1].PadLeft(2, '0');
                        string dayStr = NewdateParts[0].PadLeft(2, '0');
                        newDate = $"{dayStr}-{monthStr}-{yearStr}";
                        if (yearStr.Length == 4 && monthStr.Length == 2 && dayStr.Length == 2)
                        {
                            int year = int.Parse(yearStr);
                            int month = int.Parse(monthStr);
                            int day = int.Parse(dayStr);
                            if ((month == 4 || month == 6 || month == 9 || month == 11) && day >= 1 && day <= 30 && year >= 2024)
                            {
                                flight.DepartureDate = newDate;
                                break;
                            }
                            else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && day >= 1 && day <= 31 && year >= 2024)
                            {
                                flight.DepartureDate = newDate;
                                break;
                            }
                            else if (month == 2 && day >= 1 && day <= 28 && year >= 2024)
                            {
                                flight.DepartureDate = newDate;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-mm-dd format.");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-mm-dd format.");
                        }
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


            while (true)
            {

                Console.WriteLine("Do you want to save the changes? (yes/no): ");
                string saveChoice = Console.ReadLine();
                if (saveChoice.ToLower() == "yes")
                {
                    var flights = FlightsAccess.ReadAll();
                    var flightToUpdate = flights.FirstOrDefault(f => f.Id == flight.Id);
                    if (flightToUpdate != null)
                    {
                        flightToUpdate.Airline = flight.Airline;
                        flightToUpdate.TicketPrice = flight.TicketPrice;
                        flightToUpdate.Gate = flight.Gate;
                        flightToUpdate.DepartureAirport = flight.DepartureAirport;
                        flightToUpdate.ArrivalDestination = flight.ArrivalDestination;
                        flightToUpdate.IsCancelled = flight.IsCancelled;
                        flightToUpdate.DepartureDate = flight.DepartureDate;
                        flightToUpdate.FlightTime = flight.FlightTime;
                        FlightsAccess.WriteAll(flights);
                        Console.WriteLine("Flight details updated and saved.");
                    }
                    else
                    {
                        Console.WriteLine("Flight not found in the list.");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Changes not saved.");
                    break;
                }
            }
        }
        public static void Exit()
        {
            Console.WriteLine("press any key...");
            Console.ReadKey();
            MenuLogic.PopMenu();

        }
    }
}
