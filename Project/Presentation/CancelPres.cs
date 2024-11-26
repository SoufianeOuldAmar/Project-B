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
                string rescheduleFlightIDInput = Console.ReadLine();

                //if can't convert to an int 

                if (!int.TryParse(rescheduleFlightIDInput, out int flightIDToReschedule))
                {
                    Console.WriteLine("Invalid Flight ID. Please enter a valid number.");
                    break;
                }

                // Calling the RescheduleFlight method to get the available flights
                string rescheduleMessage = RescheduleLogic.RescheduleFlight(email, flightIDToReschedule);

                // no available flights message 
                Console.WriteLine(rescheduleMessage);


                if (rescheduleMessage.Contains("No available flights"))
                {
                    Console.WriteLine("Exiting rescheduling process.");
                    break; 
                }

                Console.WriteLine("Please enter the Flight ID of the flight you want to reschedule to:");
                string selectedFlightIDInput = Console.ReadLine();

                if (!int.TryParse(selectedFlightIDInput, out int selectedFlightID))
                {
                    Console.WriteLine("Invalid Flight ID. Please enter a valid number.");
                    break;
                }

                string rescheduleResult = RescheduleLogic.RescheduleFlight(email, flightIDToReschedule,selectedFlightID);

                // Output the result of the rescheduling
                Console.WriteLine(rescheduleResult);
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


