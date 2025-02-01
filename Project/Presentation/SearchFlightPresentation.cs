using DataModels;
using DataAccess;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Net;
using System.Collections.Generic;

public static class SearchFlightPresentation
{

    // Nieuwe methode voor het tonen van de layout en het kiezen van een stoel
    public static void DisplayFlightLayoutAndChooseSeat(FlightModel selectedFlight)
    {
        BookFlightPresentation.BookFlightMenu(true, selectedFlight);
    }
    public static void SearchFlightMenu(bool beforeLogIn = false)
    {
        Console.Clear();
        Console.WriteLine("=== 🔍 Search Flights ===\n");

    // Lees JSON-data in

    start:
        string departureAirport = string.Empty;
        while (true)
        {
            // Toon een lijst van unieke vertrekpunten
            var uniqueDepartures = SearchFlightLogic.UniqueDepartures();

            Console.WriteLine("Available departure airports:");
            for (int i = 0; i < uniqueDepartures.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{i + 1}. {uniqueDepartures[i]}");
            }

            Console.ResetColor();
            Console.WriteLine("0. Leave blank for any");

            Console.Write("\nSelect the number of your departure choice (or Q to quit): ");
            string departureChoice = Console.ReadLine();
            Console.Clear();

            if (departureChoice.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting to the main menu...");
                if (MenuPresentation.currentAccount == null)
                {
                    MenuLogic.PopMenu();
                    return;
                }

                return;
            }

            if (departureChoice.Equals("0"))
            {
                departureAirport = string.Empty;
                break;
            }

            if (int.TryParse(departureChoice, out int departureIndex) && departureIndex >= 1 && departureIndex <= uniqueDepartures.Count)
            {
                departureAirport = uniqueDepartures[departureIndex - 1];
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Selected Departure: {departureAirport}");
                Console.ResetColor();
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice. Please select a valid departure airport.");
                Console.ResetColor();
            }
        }

    destination:
        string arrivalDestination = string.Empty;
        while (true)
        {
            Console.Clear();
            // Dynamische bestemmingen op basis van vertrekpunt
            var validDestinations = SearchFlightLogic.GetValidDestinations(departureAirport);

            Console.WriteLine("\nAvailable destinations:");
            for (int i = 0; i < validDestinations.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{i + 1}. {validDestinations[i]}");
            }

            Console.ResetColor();
            Console.WriteLine("0. Leave blank for any");

            Console.Write("\nSelect the number of your destination choice (or B to go back, Q to quit): ");
            string arrivalChoice = Console.ReadLine();
            Console.Clear();

            if (arrivalChoice.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting to the main menu...");
                if (MenuPresentation.currentAccount == null)
                {
                    MenuLogic.PopMenu();
                    return;
                }
                // MenuLogic.PopMenu();
                return;
            }

            if (arrivalChoice.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                goto start;
            }

            if (arrivalChoice.Equals("0"))
            {
                arrivalDestination = string.Empty;
                break;
            }

            if (int.TryParse(arrivalChoice, out int destinationIndex) && destinationIndex >= 1 && destinationIndex <= validDestinations.Count)
            {
                arrivalDestination = validDestinations[destinationIndex - 1];
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Selected Destination: {arrivalDestination}");
                Console.ResetColor();
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice. Please select a valid destination.");
                Console.ResetColor();
            }
        }

        // Laat overige filters en zoekresultaten intact
        Console.WriteLine($"Selected Departure: {departureAirport}");
        Console.WriteLine($"Selected Destination: {arrivalDestination}");

    departureDate:
        DateTime departureDate;
        string departureDateString;

        List<FlightModel> flightsForThisDate;

        while (true)
        {

            Console.Clear();

            departureDate = CalendarPresentation.RunCalendar(departureAirport, arrivalDestination);
            departureDateString = departureDate.ToString("dd-MM-yyyy");
            flightsForThisDate = CalendarLogic.GetFlightsByDate(departureDate, departureAirport, arrivalDestination);

            Console.WriteLine($"Selected departure date: {departureDate:dd-MM-yyyy}");

            if (!flightsForThisDate.Any())
            {
                Console.WriteLine($"No flights available on {departureDate:dd-MM-yyyy}.");
                Console.WriteLine("Press 'R' to retry date selection, or any other key to exit.");

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.R)
                {
                    continue; // Retry the date selection
                }
                else
                {
                    return; // Exit the method or go back to the previous menu
                }

            }
            break; // if available 

        }


    timeOfDay:
        string timeOfDay = string.Empty;
        while (true)
        {
            Console.Clear();

            Console.Write("Enter the time of day (Morning, Midday, Evening, Night) or leave blank for any (or press Q to quit, or B to go back): ");
            timeOfDay = Console.ReadLine()?.Trim().ToLower();
            Console.Clear();

            if (timeOfDay.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting to the main menu...");
                Console.ResetColor();

                if (MenuPresentation.currentAccount == null)
                {
                    MenuLogic.PopMenu();
                    return;
                }

                return;
            }

            if (timeOfDay.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                goto departureDate;
            }

            List<string> validTimesOfDay = new List<string> { "morning", "midday", "evening", "night" };

            if (string.IsNullOrWhiteSpace(timeOfDay) || validTimesOfDay.Contains(timeOfDay))
            {
                break; // Geldige invoer
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter 'Morning', 'Midday', 'Evening', 'Night', or leave blank.");
                Console.ResetColor();
                Console.Clear();
            }


            if (!flightsForThisDate.Any())
            {
                Console.WriteLine($"No flights available for {timeOfDay} on {departureDate:dd-MM-yyyy}.");
                Console.WriteLine("Press 'R' to reselect the time of day, or any other key to exit.");

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.R)
                {
                    continue;
                }
                else
                {
                    return;
                }
            }
            // available flights for certain time 
            Console.WriteLine($"Available flights for {timeOfDay} on {departureDate:dd-MM-yyyy}:");
            foreach (var flight in flightsForThisDate)
            {
                Console.WriteLine($"{flight.Id}: {flight.Airline} to {flight.ArrivalDestination} at {flight.FlightTime}");
            }

            Console.WriteLine("Do you want to proceed with this flight? (Yes / No)");
            string input = Console.ReadLine().ToLower();
            if (input == "yes")
            {
                Console.WriteLine("Enter the flight ID to confirm your booking:");
                // Convert the user input to an integer
                if (int.TryParse(Console.ReadLine(), out int selectedFlightID))
                {
                    // Find the flight with the matching ID
                    var selectedFlight = flightsForThisDate.FirstOrDefault(x => x.Id == selectedFlightID);

                    if (selectedFlight != null)
                    {
                        // Valid flight selected
                        Console.WriteLine($"You have selected Flight {selectedFlight.Id}: {selectedFlight.Airline} to {selectedFlight.ArrivalDestination} at {selectedFlight.FlightTime}.");
                        Console.WriteLine("Press any key to confirm...");
                        Console.ReadKey();
                    }
                    else
                    {
                        // Invalid flight ID
                        Console.WriteLine("Invalid flight ID.");
                    }
                }

                else
                {
                    Console.WriteLine("Invalid flight ID");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("\nReturning to time-of-day selection...");
                continue;
            }

        }

    seatcount:
        string seatInput = string.Empty;
        int seatCount = 0;
        while (true)
        {
            Console.Write("Enter the number of seats you want to book (or leave blank for any, or press Q to quit, or B to go back): ");
            seatInput = Console.ReadLine();

            if (seatInput.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting to the main menu...");
                Console.ResetColor();

                if (MenuPresentation.currentAccount == null)
                {
                    MenuLogic.PopMenu();
                    return;
                }

                return;
            }

            if (seatInput.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                goto timeOfDay;
            }

            if (string.IsNullOrWhiteSpace(seatInput) || int.TryParse(seatInput, out seatCount) && seatCount > 0)
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a positive number or leave blank.");
                Console.ResetColor();
            }
        }



        // Console.WriteLine($"Parameters: Departure Airport: {departureAirport}, Arrival Destination: {arrivalDestination}, Departure Date: {departureDateString}, Time of Day: {timeOfDay}, Seat Count: {seatCount}");

        var searchResults = SearchFlightLogic.FilterAvailableFlights(departureAirport, arrivalDestination, departureDateString, timeOfDay, seatCount);


        Console.Clear();

        Console.WriteLine("=== 🔍 Search results ===\n");
        if (searchResults.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No flights found matching the criteria.");
            Console.ResetColor();

            if (MenuPresentation.currentAccount == null)
            {
                MenuLogic.PopMenu();
                return;
            }

        }
        else
        {
            PrintSearchResult(searchResults);

            if (beforeLogIn)
            {
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNote: You need to log in to book a flight.");
                Console.ResetColor();
                MenuLogic.PopMenu();
                return;
            }

            while (true)
            {
                Console.Write("\nEnter the flight number to book a seat, 'B' to go back, or 'Q' to quit: ");
                string choice = Console.ReadLine();

                if (choice.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting to the main menu...");
                    // MenuLogic.PopMenu();
                    return;
                }

                if (choice.Equals("B", StringComparison.OrdinalIgnoreCase))
                {
                    // Keer terug naar de tijd van de dag selectie
                    goto seatcount;
                }

                if (int.TryParse(choice, out int flightIndex) && flightIndex >= 1 && flightIndex <= searchResults.Count)
                {
                    FlightModel selectedFlight = searchResults[flightIndex - 1];
                    DisplayFlightLayoutAndChooseSeat(selectedFlight);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please enter a valid flight number, 'B' to go back, or 'Q' to quit.");
                    Console.ResetColor();
                }
            }
        }

    }

    public static void PrintSearchResult(List<FlightModel> flights)
    {
        Console.WriteLine($"{"#",-3} {"🌍 From",-35} {"🌍 To",-35} {"📅 Date",-16} {"⏰ Time",-9} {"💶 Price",-9} {"🪑 Seats"}");
        Console.WriteLine(new string('-', 124));

        for (int i = 0; i < flights.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(
                $"{(i + 1),-3} {flights[i].DepartureAirport,-35} {flights[i].ArrivalDestination,-35} " +
                $"{flights[i].DepartureDate,-16} {flights[i].FlightTime,-10} €{flights[i].TicketPrice,-9} {flights[i].AvailableSeats}");
        }
        Console.ResetColor();
    }
}