using System.Threading;

public class CancelPres
{
    public static void CancelMain(string email)
    {
        string bookedFlightsOverview = CancelLogic.BookedFlights(email);

        while (true)
        {
            Console.Clear();

            Console.WriteLine("Do you want to Cancel or Reschedule your flight?");
            Console.WriteLine("1. Cancel a flight");
            Console.WriteLine("2. Reschedule a flight");
            Console.WriteLine("3. View Booked Flights");
            Console.WriteLine("4. View Canceled Flights");
            Console.WriteLine("5. Review policy");
            Console.WriteLine("6. Quit");
            Console.Write("\nPlease enter your choice (1, 2, 3, 4, 5 or 6): ");

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    // Cancel a flight
                    Console.Clear();
                    Console.WriteLine("\nBooked Flights:\n" + bookedFlightsOverview);

                    Console.Write("Please enter the Flight ID of the flight you want to cancel (or enter 'Q' to quit the process): ");
                    string cancelFlightID = Console.ReadLine();

                    if (cancelFlightID.Trim().ToUpper() == "Q")
                    {
                        Console.WriteLine("Cancellation process aborted.");
                    }
                    else
                    {
                        try
                        {
                            int flightID = Convert.ToInt32(cancelFlightID);

                            // Call the cancel flight method
                            string cancelResult = CancelLogic.CancelFlights(email, flightID);
                            Console.WriteLine(cancelResult);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a numeric Flight ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                        }
                    }

                    break;

                case "2":
                    // Reschedule a flight
                    Console.Clear();

                    Console.WriteLine("\nBooked Flights:\n" + bookedFlightsOverview);

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
                    Console.WriteLine("\nBooked Flights:\n" + bookedFlightsOverview);
                    break;

                case "4":
                    Console.Clear();

                    // View canceled flights for the user
                    string canceledFlightsOverview = CancelLogic.CancelledOverview(email);
                    Console.WriteLine("\nCanceled Flights:\n" + canceledFlightsOverview);
                    break;

                case "5":
                    Console.Clear();

                    string policy = RescheduleLogic.Policy();
                    Console.WriteLine(policy);
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

}


