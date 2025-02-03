using System.Collections.Generic;
using System.Threading;

public static class CancelPresentation
{
    public static void CancelMain(string email)
    {

        while (true)
        {
            Console.Clear();

            Console.WriteLine("=== 🧾 View History of Tickets ===\n");
            Console.WriteLine("1. ❌ Cancel a flight");
            Console.WriteLine("2. 🔄 Reschedule a flight");
            Console.WriteLine("3. 📋 View Booked Flights");
            Console.WriteLine("4. 📜 Review policy");
            Console.WriteLine("5. 🍴 Add food and drinks to a booked flight");
            Console.WriteLine("6. 🚪 Quit");
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
                    ReschedulePresentation.RescheduleFlightMenu();
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

                    string policy = "Cancellation policy: Tickets are non-refundable \n" +
               "Rescheduling policy: Reschedule your flight with a €50 fee, plus any price difference for the new flight.";
                    Console.WriteLine(policy);
                    MenuPresentation.PressAnyKey();
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("\nYour Booked Flights:\n");
                    BookedFlights(email);

                    Console.Write("\nPlease enter the Flight ID of the flight to which you want to add food and drinks (or enter 'Q' to quit): ");
                    string flightIdInput = Console.ReadLine();

                    if (flightIdInput.Trim().ToUpper() == "Q")
                    {
                        Console.WriteLine("Returning to the main menu...");
                        MenuPresentation.PressAnyKey();
                        break;
                    }

                    if (int.TryParse(flightIdInput, out int flightId))
                    {
                        // Zoek de geboekte vlucht met het opgegeven Flight ID
                        if (CancelLogic.CheckBookedFlights(email))
                        {
                            var selectedFlight = BookFlightLogic.SearchBookedFlightByFlightID(flightId, email);
                            if (selectedFlight != null)
                            {
                                // Voeg eten en drinken toe aan de geselecteerde vlucht
                                FoodAndDrinkPresentation.AddFoodAndDrinksToExistingBooking(selectedFlight);
                            }
                            else
                            {
                                Console.WriteLine("No booked flight found with the given Flight ID.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have no booked flights.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Flight ID. Please enter a valid number.");
                    }

                    MenuPresentation.PressAnyKey();
                    break;

                case "6":
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

        var allBookedFlights = BookFlightLogic.SearchByEmail(email);

        if (allBookedFlights.Count == 0)
        {
            Console.WriteLine("You have no flights booked.");
            return;
        }

        var bookedFlights = BookFlightLogic.SearchByEmail(email);

        const int tableWidth = 173;
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

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{"Tickets Bought On",-15}");
        Console.ResetColor();
        Console.Write(" | ");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{"Cancelled",-10}");
        Console.ResetColor();
        Console.WriteLine(" |");

        Console.WriteLine(separator);


        foreach (var bookedFlight in bookedFlights)
        {
            // Find the needed flight details
            var neededFlight = FlightLogic.SearchFlightByID(bookedFlight.FlightID);
            if (neededFlight == null)
            {
                continue;
            }

            double totalTicketPrice = CancelLogic.CalculateTotalCost(bookedFlight, neededFlight);

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
            Console.Write($"{FlightLogic.SearchFlightByID(neededFlight.Id).DepartureAirport,-30}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Cyan; // Color for Arrival Destination
            Console.Write($"{FlightLogic.SearchFlightByID(neededFlight.Id).ArrivalDestination,-37}");
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
            Console.ForegroundColor = ConsoleColor.White; // Color for Ticket Price
            Console.Write($"{bookedFlight.DateTicketsBought.ToString(),-17}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Red; // Color for Cancelled status
            string IsCancelled = bookedFlight.IsCancelled ? "Yes" : "No";

            Console.Write($"{IsCancelled,-10}");
            Console.ResetColor();

            Console.WriteLine(" |");

            Console.WriteLine(separator);

            // Pets Details
            if (bookedFlight.Pets?.Count > 0)
            {
                Console.WriteLine($"  Pets on this flight:");
                foreach (var pet in bookedFlight.Pets)
                {
                    Console.WriteLine($"    Animal: {pet.AnimalType}, Fee: €{pet.Fee}");
                }
            }
            else
            {
                Console.WriteLine($"  No pets booked for this flight.");
            }

            if (bookedFlight.FoodAndDrinkItems?.Count > 0)
            {
                Console.WriteLine($"  Food and Drinks on this flight:");
                foreach (var item in bookedFlight.FoodAndDrinkItems)
                {
                    Console.WriteLine($"    Item: {item.Name}, Price: €{item.Price:F2}");
                }
            }
            else
            {
                Console.WriteLine($"  No food and drinks added for this flight.");
            }

            // Baggage Details
            if (bookedFlight.BaggageInfo?.Count > 0)
            {
                Console.WriteLine($"  Baggage Details:");
                foreach (var baggage in bookedFlight.BaggageInfo)
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
            // BookedFlightsModel bookedFlights = allBo
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
            if (BookFlightLogic.SearchByEmail(email).Count == 0)
            {
                MenuPresentation.PrintColored("\nNo booked flights found for this user.", ConsoleColor.Red);
                MenuPresentation.PressAnyKey();
                break;  // Exit the loop if no flights are found
            }

            // Find the flight by ID
            BookedFlightsModel bookedFlight = BookFlightLogic.SearchBookedFlightByFlightID(flightID, email);

            if (bookedFlight == null)
            {
                MenuPresentation.PrintColored($"\nFlight with ID {flightID} not found.", ConsoleColor.Red);
                MenuPresentation.PressAnyKey();
                continue;  // Continue the loop if the flight is not found
            }

            // Check if the flight is already cancelled
            if (CancelLogic.IsBookedFlightCancelled(bookedFlight))
            {
                MenuPresentation.PrintColored($"You have already cancelled the flight with ID {flightID}.", ConsoleColor.Yellow);
                MenuPresentation.PressAnyKey();
                continue;  // Continue the loop if the flight is already cancelled
            }

            CancelLogic.CancelFlight(email, bookedFlight);

            MenuPresentation.PrintColored($"\nFlight with ID {flightID} has been cancelled.", ConsoleColor.Green);
            MenuPresentation.PressAnyKey();
            break;  // Exit the loop after successful cancellation
        }
    }
}
