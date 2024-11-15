public class CancelPres
{
    public static void CancelMain(string email)
    {
        while (true)
        {
            Console.WriteLine("Do you want to Cancel or Reschedule your flight?");
            Console.WriteLine("1. Cancel a flight");
            Console.WriteLine("2. Reschedule a flight");
            Console.WriteLine("3. View Booked Flights");
            Console.WriteLine("4. View Canceled Flights");
            Console.WriteLine("5. Review policy");
            Console.WriteLine("6. Quit");
            Console.WriteLine("Please enter your choice (1, 2, 3, 4, 5 or 6): ");

            string userInput = Console.ReadLine();
            switch(userInput)
            {
                case "1":
                    // Cancel a flight
                    Console.WriteLine("Please enter the Flight ID of the flight you want to cancel:");
                    string cancelFlightID = Console.ReadLine();
                    try 
                    {
                        int flightID = Convert.ToInt32(cancelFlightID);
                        // Call cancel flight method 
                        string cancelResult = CancelLogic.CancelFlights(email, flightID);
                        Console.WriteLine(cancelResult);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid number for the Flight ID.");
                    }
                    break;

                case "2":
                    // Reschedule a flight
                    Console.WriteLine("Please enter the Flight ID of the flight you want to reschedule:");
                    string rescheduleFlightID = Console.ReadLine();
                    try
                    {
                        // Convert the flight ID input to an integer
                        int flightIDToReschedule = Convert.ToInt32(rescheduleFlightID);

                        // Ask for new departure and arrival details
                        Console.WriteLine("Please enter the new departure airport:");
                        string newDepartureAirport = Console.ReadLine();

                        Console.WriteLine("Please enter the new arrival destination:");
                        string newArrivalDestination = Console.ReadLine();

                        // Ask for the new departure date
                        Console.WriteLine("Please enter the new departure date (e.g., 2024-12-25):");
                        string newDepartureDate = Console.ReadLine();

                        // Ask for the new departure time
                        Console.WriteLine("Please enter the new departure time (e.g., 14:30):");
                        string newFlightTime = Console.ReadLine();

                        // Call RescheduleFlight from CancelLogic to attempt rescheduling
                        string rescheduleResult = RescheduleLogic.RescheduleFlight(email, newDepartureAirport, newArrivalDestination, newDepartureDate, newFlightTime);

                        // Output the result of the rescheduling 
                        Console.WriteLine(rescheduleResult);
                    }
                    catch (FormatException)
                    {
                        // Handle invalid input format (e.g., non-numeric input for flight ID)
                        Console.WriteLine("Invalid Flight ID. Please enter a valid number.");
                    }
                    break;

                case "3":
                    // View booked flights for the user
                    string bookedFlightsOverview = CancelLogic.BookedFlights(email);
                    Console.WriteLine("\nBooked Flights:\n" + bookedFlightsOverview);
                    break;

                case "4":
                    // View canceled flights for the user
                    string canceledFlightsOverview = CancelLogic.CancelledOverview(email);
                    Console.WriteLine("\nCanceled Flights:\n" + canceledFlightsOverview);
                    break;

                case "5":
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

