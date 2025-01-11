using DataModels;
using DataAccess;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Net;

public static class SearchFlightPresentation
{

    // Nieuwe methode voor het tonen van de layout en het kiezen van een stoel
    public static void DisplayFlightLayoutAndChooseSeat(FlightModel selectedFlight)
    {
        BookFlightPresentation.BookFlightMenu(true, selectedFlight);
    }
    public static void SearchFlightMenu()
    {
        Console.Clear();
        Console.WriteLine("=== 🔍 Search Flights ===\n");

        // Lees JSON-data in
        List<FlightModel> flights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");

    start:
        string departureAirport = string.Empty;
        while (true)
        {
            // Toon een lijst van unieke vertrekpunten
            var uniqueDepartures = flights.Select(f => f.DepartureAirport).Distinct().OrderBy(d => d).ToList();

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
                // MenuLogic.PopMenu();
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
            var validDestinations = string.IsNullOrEmpty(departureAirport)
                ? flights.Select(f => f.ArrivalDestination).Distinct().OrderBy(d => d).ToList()
                : flights.Where(f => f.DepartureAirport.Equals(departureAirport, StringComparison.OrdinalIgnoreCase))
                         .Select(f => f.ArrivalDestination)
                         .Distinct()
                         .OrderBy(d => d)
                         .ToList();

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

            Console.WriteLine("Do you want to proceed with a this flight? (Yes / No)");
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

        Dictionary<string, (TimeSpan Start, TimeSpan End)> timeOfDayMapping = new Dictionary<string, (TimeSpan Start, TimeSpan End)>
    {
        { "morning", (Start: new TimeSpan(6, 0, 0), End: new TimeSpan(11, 59, 59)) },
        { "midday", (Start: new TimeSpan(12, 0, 0), End: new TimeSpan(17, 59, 59)) },
        { "evening", (Start: new TimeSpan(18, 0, 0), End: new TimeSpan(23, 59, 59)) },
        { "night", (Start: new TimeSpan(0, 0, 0), End: new TimeSpan(5, 59, 59)) }
    };

        // Console.WriteLine($"Parameters: Departure Airport: {departureAirport}, Arrival Destination: {arrivalDestination}, Departure Date: {departureDateString}, Time of Day: {timeOfDay}, Seat Count: {seatCount}");
        var searchResults = flights.Where(flight =>
    !flight.HasTakenOff && // Exclude flights that have already taken off
    (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport.Contains(departureAirport, StringComparison.OrdinalIgnoreCase)) &&
    (string.IsNullOrEmpty(arrivalDestination) || flight.ArrivalDestination.Contains(arrivalDestination, StringComparison.OrdinalIgnoreCase)) &&
    (string.IsNullOrEmpty(departureDateString) || flight.DepartureDate.Contains(departureDateString)) &&
    (string.IsNullOrEmpty(timeOfDay) ||
        (timeOfDayMapping.TryGetValue(timeOfDay, out var timeRange) &&
        DateTime.TryParse(flight.FlightTime, out var flightTime) &&
        flightTime.TimeOfDay >= timeRange.Start && flightTime.TimeOfDay <= timeRange.End))
    &&
    (seatCount == 0 || flight.AvailableSeats >= seatCount)
).ToList();


        Console.Clear();

        Console.WriteLine("=== 🔍 Search results ===\n");
        if (searchResults.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No flights found matching the criteria.");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"{"#",-3} {"🌍 From",-30} {"🌍 To",-30} {"📅 Date",-16} {"⏰ Time",-10} {"💶 Price",-10} {"🪑 Seats"}");
            Console.WriteLine(new string('-', 110));
            for (int i = 0; i < searchResults.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(
                    $"{(i + 1),-3} {searchResults[i].DepartureAirport,-30} {searchResults[i].ArrivalDestination,-30} " +
                    $"{searchResults[i].DepartureDate,-16} {searchResults[i].FlightTime,-10} €{searchResults[i].TicketPrice,-9} {searchResults[i].AvailableSeats}");
            }
            Console.ResetColor();

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

    public static void SearchFlightsMenuBeforeLogIn()
    {
        Console.Clear();
        Console.WriteLine("=== 🔍 Search Flights ===\n");

        // Lees JSON-data in
        List<FlightModel> flights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");

    start:
        string departureAirport = string.Empty;
        while (true)
        {
            // Toon een lijst van unieke vertrekpunten
            var uniqueDepartures = flights.Select(f => f.DepartureAirport).Distinct().OrderBy(d => d).ToList();

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
                MenuLogic.PopMenu();
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
            var validDestinations = string.IsNullOrEmpty(departureAirport)
                ? flights.Select(f => f.ArrivalDestination).Distinct().OrderBy(d => d).ToList()
                : flights.Where(f => f.DepartureAirport.Equals(departureAirport, StringComparison.OrdinalIgnoreCase))
                         .Select(f => f.ArrivalDestination)
                         .Distinct()
                         .OrderBy(d => d)
                         .ToList();

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
                MenuLogic.PopMenu();
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

        Console.WriteLine($"Available flights on {departureDate:dd-MM-yyyy}:");
        foreach (var flight in flightsForThisDate)
        {
            Console.WriteLine($"{flight.Id}: {flight.Airline} to {flight.ArrivalDestination} at {flight.FlightTime}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();


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

            Console.WriteLine("Do you want to proceed with a this flight? (Yes / No)");
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


        Dictionary<string, (TimeSpan Start, TimeSpan End)> timeOfDayMapping = new Dictionary<string, (TimeSpan Start, TimeSpan End)>
        {
            { "morning", (Start: new TimeSpan(6, 0, 0), End: new TimeSpan(11, 59, 59)) },
            { "midday", (Start: new TimeSpan(12, 0, 0), End: new TimeSpan(17, 59, 59)) },
            { "evening", (Start: new TimeSpan(18, 0, 0), End: new TimeSpan(23, 59, 59)) },
            { "night", (Start: new TimeSpan(0, 0, 0), End: new TimeSpan(5, 59, 59)) }
        };



        var searchResults = flights.Where(flight =>
    !flight.HasTakenOff && // Exclude flights that have already taken off
    (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport.Contains(departureAirport, StringComparison.OrdinalIgnoreCase)) &&
    (string.IsNullOrEmpty(arrivalDestination) || flight.ArrivalDestination.Contains(arrivalDestination, StringComparison.OrdinalIgnoreCase)) &&
    (string.IsNullOrEmpty(departureDateString) || flight.DepartureDate.Contains(departureDateString)) &&
    (string.IsNullOrEmpty(timeOfDay) ||
        (timeOfDayMapping.TryGetValue(timeOfDay, out var timeRange) &&
        DateTime.TryParse(flight.FlightTime, out var flightTime) &&
        flightTime.TimeOfDay >= timeRange.Start && flightTime.TimeOfDay <= timeRange.End))
    &&
    (seatCount == 0 || flight.AvailableSeats >= seatCount)
).ToList();


        Console.WriteLine($"\nNumber of search results: {searchResults.Count}");
        if (searchResults.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No flights found matching the criteria.");
            Console.ResetColor();
        }


        Console.WriteLine("\nSearch results:");
        if (searchResults.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No flights found matching the criteria.");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"{"#",-3} {"✈️  Airline",-21} {"🌍 From",-46} {"🌍 To",-39} {"📅 Date",-18} {"⏰ Time",-15} {"💶 Price"}");

            Console.WriteLine(new string('-', 195));
            for (int i = 0; i < searchResults.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{(i + 1),-3}. {searchResults[i].Airline,-20} {searchResults[i].DepartureAirport,-46} {searchResults[i].ArrivalDestination,-38} {searchResults[i].DepartureDate,-18}  {searchResults[i].FlightTime,-16} €{searchResults[i].TicketPrice},-");
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nNote: You need to log in to book a flight.");
            Console.ResetColor();
        }

        MenuLogic.PopMenu();
    }
}