using System.Threading;

public class CancelPres
{
    public static void CancelMain(string email)
    {

        while (true)
        {
            Console.Clear();

            Console.WriteLine("Do you want to Cancel or Reschedule your flight?");
            Console.WriteLine("1. Cancel a flight");
            Console.WriteLine("2. Reschedule a flight");
            Console.WriteLine("3. View Booked Flights");
            Console.WriteLine("4. Review policy");
            Console.WriteLine("5. Quit");
            Console.Write("\nPlease enter your choice (1, 2, 3, 4, 5 or 6): ");

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    // Cancel a flight

                    CancelFlights(email);

                    break;

                case "2":
                    // Reschedule a flight
                    Console.Clear();

                    Console.WriteLine("\nBooked Flights:\n");
                    BookedFlights(email);

                    Console.Write("\nPlease enter the Flight ID of the flight you want to reschedule (or enter 'Q' to quit): ");
                    string rescheduleFlightIDInput = Console.ReadLine();

                    // Check for quit option
                    if (rescheduleFlightIDInput.Trim().ToUpper() == "Q")
                    {
                        Console.WriteLine("Rescheduling process aborted.");
                        break;
                    }

                    // Validate input
                    if (!int.TryParse(rescheduleFlightIDInput, out int flightIDToReschedule))
                    {
                        Console.WriteLine("Invalid Flight ID. Please enter a valid number.");
                        Thread.Sleep(5000);
                        break;
                    }

                    // Call the RescheduleFlight method to get the available flights
                    string rescheduleMessage = RescheduleLogic.RescheduleFlight(email, flightIDToReschedule);

                    // No available flights message
                    Console.WriteLine(rescheduleMessage);
                    if (rescheduleMessage.Contains("No available flights"))
                    {
                        Console.WriteLine("Exiting rescheduling process.");
                        Thread.Sleep(5000);
                        break;
                    }

                    Console.Write("Please enter the Flight ID of the flight you want to reschedule to (or enter 'Q' to quit): ");
                    string selectedFlightIDInput = Console.ReadLine();

                    // Check for quit option
                    if (selectedFlightIDInput.Trim().ToUpper() == "Q")
                    {
                        Console.WriteLine("Rescheduling process aborted.");
                        break;
                    }

                    // Validate input
                    if (!int.TryParse(selectedFlightIDInput, out int selectedFlightID))
                    {
                        Console.WriteLine("Invalid Flight ID. Please enter a valid number.");
                        break;
                    }

                    // Call the reschedule logic to reschedule the flight
                    string rescheduleResult = RescheduleLogic.RescheduleFlight(email, flightIDToReschedule, selectedFlightID);

                    // Output the result of the rescheduling
                    Console.WriteLine(rescheduleResult);
                    break;

                case "3":
                    Console.Clear();

                    // View booked flights for the user
                    Console.WriteLine("\nBooked Flights:\n");
                    BookedFlights(email);
                    MenuPresentation.PressAnyKey();
                    break;

                case "4":
                    Console.Clear();

                    string policy = RescheduleLogic.Policy();
                    Console.WriteLine(policy);
                    MenuPresentation.PressAnyKey();
                    break;

                case "5":
                    // Quit the program
                    MenuLogic.PopMenu();
                    return;

                default:
                    // Invalid option
                    Console.WriteLine("Invalid choice. Please select a valid option (1, 2, 3, 4, or 5).");
                    break;
            }
        }
    }

    public static void BookedFlights(string email)
    {
        if (!BookFlightPresentation.allBookedFlights.ContainsKey(email) || BookFlightPresentation.allBookedFlights[email].Count == 0)
        {
            Console.WriteLine("You have no flights booked.");
        }

        Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();
        var bookedFlights = allBookedFlights[email];

        const int tableWidth = 153;
        string separator = new string('-', tableWidth);

        // Header
        Console.WriteLine(separator);

        // Print each part of the header with specific coloring
        Console.Write("| ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"{"Flight ID",-12}");
        Console.ResetColor();
        Console.Write(" | ");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{"Airline",-15}");
        Console.ResetColor();
        Console.Write(" | ");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"{"Departure",-30}");
        Console.ResetColor();
        Console.Write(" | ");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"{"Arrival",-37}");
        Console.ResetColor();
        Console.Write(" | ");

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write($"{"Date",-12}");
        Console.ResetColor();
        Console.Write(" | ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{"Ticket Price",-15}");
        Console.ResetColor();
        Console.Write(" | ");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{"Cancelled",-10}");
        Console.ResetColor();
        Console.WriteLine(" |");

        Console.WriteLine(separator);


        foreach (var flight in bookedFlights)
        {
            // Find the needed flight details
            var neededFlight = BookFlightPresentation.allFlights.Find(x => x.Id == flight.FlightID);
            if (neededFlight == null)
            {
                continue;
            }

            string returnFlightAvailable = neededFlight.ReturnFlight != null ? "Yes" : "No";
            double totalPetFee = flight.Pets?.Sum(pet => pet.Fee) ?? 0;
            double totalBaggageFee = flight.BaggageInfo?.Sum(bag => bag.Fee) ?? 0;
            double totalTicketPrice = neededFlight.TicketPrice + totalPetFee + totalBaggageFee;

            Console.Write("| ");
            Console.ForegroundColor = ConsoleColor.Blue; // Color for Flight ID
            Console.Write($"{neededFlight.Id,-12}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Yellow; // Color for Airline
            Console.Write($"{neededFlight.Airline,-15}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Cyan; // Color for Departure Airport
            Console.Write($"{BookFlightLogic.SearchFlightByID(neededFlight.Id).DepartureAirport,-30}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Cyan; // Color for Arrival Destination
            Console.Write($"{BookFlightLogic.SearchFlightByID(neededFlight.Id).ArrivalDestination,-37}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Magenta; // Color for Departure Date
            Console.Write($"{neededFlight.DepartureDate,-12}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.White; // Color for Ticket Price
            Console.Write($"€{totalTicketPrice,-14}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Red; // Color for Cancelled status
            string IsCancelled = flight.IsCancelled ? "Yes" : "No";

            Console.Write($"{IsCancelled,-10}");
            Console.ResetColor();

            Console.WriteLine(" |");

            Console.WriteLine(separator);

            // Pets Details
            if (flight.Pets?.Count > 0)
            {
                Console.WriteLine($"  Pets on this flight:");
                foreach (var pet in flight.Pets)
                {
                    Console.WriteLine($"    Animal: {pet.AnimalType}, Fee: €{pet.Fee}");
                }
            }
            else
            {
                Console.WriteLine($"  No pets booked for this flight.");
            }

            // Baggage Details
            if (flight.BaggageInfo?.Count > 0)
            {
                Console.WriteLine($"  Baggage Details:");
                foreach (var baggage in flight.BaggageInfo)
                {
                    string baggageType = baggage.BaggageType switch
                    {
                        "1" => "Carry On",
                        "2" => "Checked",
                        "3" => "Both Carry On and Checked",
                        _ => "Unknown Type"
                    };

                    if (baggage.BaggageType == "3")
                    {
                        baggage.BaggageWeight += 10; // Add weight for combined baggage
                    }

                    Console.WriteLine($"    Type: {baggageType}, Weight: {baggage.BaggageWeight}kg, Fee: €{baggage.Fee}");
                }
            }
            else
            {
                Console.WriteLine($"  No baggage added for this flight.");
            }

            Console.WriteLine(separator);
        }

    }

    public static void CancelFlights(string email)
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nBooked Flights:\n");
            BookedFlights(email);  // Display the user's booked flights

            Console.Write("Please enter the Flight ID of the flight you want to cancel (or enter 'Q' to quit the process): ");
            string cancelFlightID = Console.ReadLine().Trim();

            if (cancelFlightID.ToUpper() == "Q")
            {
                Console.WriteLine("Cancellation process aborted.");
                MenuPresentation.PressAnyKey();
                break;  // Exit the loop if the user wants to quit
            }

            // Try to convert the input to an integer
            int flightID;
            if (!int.TryParse(cancelFlightID, out flightID))
            {
                Console.WriteLine("Invalid input. Please enter a numeric Flight ID.");
                MenuPresentation.PressAnyKey();
                continue;  // Repeat the loop if the input is not a valid number
            }

            // Proceed with cancellation logic
            if (!BookFlightPresentation.allBookedFlights.ContainsKey(email))
            {
                Console.WriteLine("No booked flights found for this user.");
                MenuPresentation.PressAnyKey();
                break;  // Exit the loop if no flights are found
            }

            // Get user's booked flights
            var bookedFlights = BookFlightPresentation.allBookedFlights[email];

            // Find the flight by ID
            var bookedFlight = bookedFlights.FirstOrDefault(x => x.FlightID == flightID);

            if (bookedFlight == null)
            {
                Console.WriteLine($"Flight with ID {flightID} not found.");
                MenuPresentation.PressAnyKey();
                continue;  // Continue the loop if the flight is not found
            }

            // Check if the flight is already cancelled
            if (bookedFlight.IsCancelled)
            {
                Console.WriteLine($"You have already cancelled the flight with ID {flightID}.");
                MenuPresentation.PressAnyKey();
                continue;  // Continue the loop if the flight is already cancelled
            }

            // Set the flight as cancelled
            bookedFlight.IsCancelled = true;

            // Save changes to the JSON file
            BookedFlightsAccess.WriteAll(email, bookedFlights);  // Pass updated list of booked flights

            Console.WriteLine($"Flight with ID {flightID} has been cancelled.");
            break;  // Exit the loop after successful cancellation
        }
    }
}


