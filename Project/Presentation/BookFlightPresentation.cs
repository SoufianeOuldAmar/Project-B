using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;
using DataAccess;
using System.Threading;

public static class BookFlightPresentation
{
    public static List<FlightModel> allFlights = FlightsAccess.ReadAll();
    public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();
    public static void BookFlightMenu(bool searchFlightFunction = false, FlightModel flightModel = null)
    {
        var currentAccount = AccountsLogic.CurrentAccount;
        int totalFlightpoints = 0;

        List<BaggageLogic> baggageInfo = new List<BaggageLogic>();
        List<PetLogic> petInfo = new List<PetLogic>();

        if (!searchFlightFunction)
        {
            while (true)
            {
                Console.WriteLine("=== Book Ticket ===\n");
                if (allFlights.Count == 0)
                {
                    Console.WriteLine("No flights available.");
                    return;
                }

                Console.WriteLine("{0,-5} {1,-25} {2,-46} {3,-58} {4,-15} {5,-14} {6,-14} {7,-15} {8,-10}",
                  "ID", "Airline", "Departure Airport", "Arrival Destination",
                  "Flight Date", "Flight Time", "Ticket Price", "Return Flight", "Cancelled");
                Console.WriteLine(new string('-', 213));

                foreach (var flight in allFlights)
                {
                    Console.WriteLine("{0,-5} {1,-25} {2,-46} {3,-58} {4,-15} {5,-14} {6,-14} {7,-15} {8,-10}",
                                      flight.Id,
                                      flight.Airline,
                                      flight.DepartureAirport,
                                      flight.ArrivalDestination,
                                      flight.DepartureDate,
                                      flight.FlightTime,
                                      flight.TicketPrice,
                                      flight.ReturnFlight == null ? "No" : "Yes",
                                      flight.IsCancelled ? "Yes" : "No");
                }

                Console.WriteLine("\n" + new string('-', 213));
                Console.Write("\nEnter the ID of the flight you wish to book (or 'Q' to quit): ");


                var input = Console.ReadLine();
                if (input?.ToUpper() == "Q")
                {
                    Console.WriteLine("Booking canceled. Returning to main menu.");
                    MenuLogic.PopMenu();
                    break;
                }

                if (int.TryParse(input, out int selectedId))
                {
                    var selectedFlight = BookFlightLogic.SearchFlightByID(selectedId);
                    if (selectedFlight != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected the following flight:\n");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("{0,-20} {1,-35}", "Airline:", selectedFlight.Airline);
                        Console.WriteLine("{0,-20} {1,-35}", "Departure Airport:", selectedFlight.DepartureAirport);
                        Console.WriteLine("{0,-20} {1,-35}", "Arrival Destination:", selectedFlight.ArrivalDestination);
                        Console.WriteLine("{0,-20} {1,-35}", "Flight Date:", selectedFlight.DepartureDate);
                        Console.WriteLine("{0,-20} {1,-35}", "Flight Time:", selectedFlight.FlightTime);
                        Console.WriteLine("{0,-20} {1,-35}", "Ticket Price:", selectedFlight.TicketPrice);
                        Console.WriteLine("{0,-20} {1,-35}", "Available Seats:", selectedFlight.AvailableSeats);
                        Console.WriteLine("{0,-20} {1,-35}", "Is Cancelled:", (selectedFlight.IsCancelled ? "Yes" : "No"));
                        Console.ResetColor();

                        Console.Write("\nAre you sure you want to book this flight? (yes/no) ");
                        string confirmation = Console.ReadLine();
                        Console.Clear();

                        if (confirmation.ToLower() == "yes")
                        {
                            List<string> chosenSeats = new List<string>();
                            selectedFlight.Layout.PrintLayout();

                            while (true)
                            {
                                Console.Write("\nWhich seat do you want? (press Q to quit or Enter to confirm booking and keep choosing by seat number if you want more seats): ");
                                string seat = Console.ReadLine()?.ToUpper();

                                if (seat == "Q")
                                {
                                    break;
                                }
                                else if (string.IsNullOrWhiteSpace(seat))
                                {
                                    Console.WriteLine("Confirming your selected seats...");
                                    selectedFlight.Layout.ConfirmBooking();
                                    var FlightPointData = new FlightPoint(DateTime.Now, totalFlightpoints, selectedFlight.Id);
                                    currentAccount.FlightPointsDataList.Add(FlightPointData);

                                    AccountsAccess.WriteAll(AccountsLogic._accounts);
                                    break;
                                }

                                // Get user initials
                                Console.Write("Enter your initials (2 characters): ");
                                string initials = Console.ReadLine()?.ToUpper() ?? "";
                                while (initials.Length != 2)
                                {
                                    Console.Write("Please enter exactly 2 characters for your initials: ");
                                    initials = Console.ReadLine()?.ToUpper() ?? "";
                                }

                                // Book the seat with initials
                                selectedFlight.Layout.BookFlight(seat, initials);

                                // after booking seat ask user if they want baggage 
                                // Ask user if they want baggage
                                Console.WriteLine("Do you want to add baggage? (yes/no):");
                                string userBaggage = Console.ReadLine().ToLower();

                                while (userBaggage != "yes" && userBaggage != "no")
                                {
                                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no':");
                                    userBaggage = Console.ReadLine().ToLower();
                                }

                                if (userBaggage == "no")
                                {
                                    return;
                                }

                                if (userBaggage == "yes")
                                {
                                    double totalWeightSeat = 0; 
                                    double feeBaggage = 0; 
                                    double totalCarryOnWeight = 0;

                                    
                                    // Console.WriteLine("Do you want to add checked baggage? (yes/no):");
                                    Console.WriteLine("You get 25kg of baggage per seat (this includes both checked and carry-on) (enter yes to continue).");
                                    string addCheckedBaggage = Console.ReadLine().ToLower();

                                    while (addCheckedBaggage != "yes" && addCheckedBaggage != "no")
                                    {
                                        Console.WriteLine("Invalid input. Please enter 'yes' or 'no':");
                                        addCheckedBaggage = Console.ReadLine().ToLower();
                                    }



                                    if (addCheckedBaggage == "yes")
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine("Enter weight for checked baggage (choose 20kg or 25kg): ");
                                            double checkedWeight;

                                            // Ensure valid input for checked baggage weight
                                            if (double.TryParse(Console.ReadLine(), out checkedWeight))
                                            {
                                                if (checkedWeight == 20)
                                                {
                                                    feeBaggage += 45; 
                                                    totalWeightSeat += 20;
                                                    Console.WriteLine("Checked baggage of 20kg added for 45 EUR.");
                                                    break;
                                                }
                                                else if (checkedWeight == 25)
                                                {
                                                    feeBaggage += 50; 
                                                    totalWeightSeat += 25;
                                                    Console.WriteLine("Checked baggage of 25kg added for 50 EUR.");
                                                    Console.WriteLine("You have reached the 25kg per seat limit. No carry-on allowed.");
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid weight. Please enter either 20kg or 25kg.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid input. Please enter a numeric value.");
                                            }
                                        }
                                    }

                                    // Add Carry-On Baggage if Space Left
                                    if (totalWeightSeat < 25)
                                    {
                                        double remainingCarryOnWeight = 25 - totalWeightSeat;
                                        Console.WriteLine($"You have {remainingCarryOnWeight}kg left for carry-on baggage. Do you want to add carry-on baggage? (yes/no):");
                                        string addCarryOn = Console.ReadLine().ToLower();

                                        while (addCarryOn == "yes" && remainingCarryOnWeight > 0)
                                        {
                                            Console.WriteLine($"Enter weight for carry-on bag (max {Math.Min(remainingCarryOnWeight, 5)} per bag, left weight: {remainingCarryOnWeight} kg): ");
                                            double carryOnWeight;

                                            // Ensure valid input for carry-on weight
                                            if (double.TryParse(Console.ReadLine(), out carryOnWeight))
                                            {
                                                if (carryOnWeight > 5)
                                                {
                                                    Console.WriteLine("Carry-on weight cannot exceed 5kg per bag. Please enter a valid weight.");
                                                    continue;
                                                }

                                                if (totalWeightSeat + carryOnWeight > 25)
                                                {
                                                    Console.WriteLine("Adding this carry-on bag will exceed the 25kg limit. Please adjust the weight.");
                                                    continue;
                                                }

                                                totalWeightSeat += carryOnWeight;
                                                totalCarryOnWeight += carryOnWeight;
                                                remainingCarryOnWeight -= carryOnWeight;
                                                Console.WriteLine($"Added a carry-on bag of {carryOnWeight}kg for €15.");

                                                if (totalCarryOnWeight < 5)
                                                {
            
                                                    Console.WriteLine("Do you want to add another carry-on bag? (yes/no):");
                                                    addCarryOn = Console.ReadLine().ToLower();
                                                }
                                                else
                                                {
                                                    break; // No more carry-on weight available
                                                }


                                            }

                                            else
                                            {
                                                Console.WriteLine("Invalid input. Please enter a numeric value.");
                                            }
                                        }
                                    }

                                    if (totalCarryOnWeight > 0)
                                    {
                                        feeBaggage += 15;  
                                        Console.WriteLine($"Total carry-on baggage fee: €15.");
                                    }

                                   // confirmation message 
                                    Console.WriteLine($"Baggage added successfully. Total weight: {totalWeightSeat}kg, Total fee: €{feeBaggage}.");
                                    baggageInfo.Add(new BaggageLogic(initials, "checked + carry-on", totalWeightSeat) { Fee = feeBaggage });
                                }


                                Console.WriteLine("Do you want to add a pet? (yes/no):");
                                string userPet = Console.ReadLine()?.ToLower();

                                if (userPet == "yes")
                                {
                                    // Check current pet count for the flight
                                    if (selectedFlight.TotalPets >= 7)
                                    {

                                        Console.WriteLine("Sorry, no more pet space available on this flight.");
                                        break;
                                    }
                                    else
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine("What type of pet do you have? (dog, cat, bunny, bird): ");
                                            string petType = Console.ReadLine()?.ToLower();

                                            if (petType == "dog" || petType == "cat" || petType == "bunny" || petType == "bird")
                                            {
                                                var newPet = new PetLogic(petType) { Fee = 50.0 }; // Adds a new pet with a 50 EUR fee
                                                petInfo.Add(newPet);
                                                selectedFlight.TotalPets++; // Increment total pets for the flight
                                                Console.WriteLine($"Pet {petType} added. Fee: 50 EUR."); // Notify the user of the fee

                                                if (selectedFlight.TotalPets >= 7)
                                                {
                                                    Console.WriteLine("Maximum pet capacity reached for this flight.");
                                                    break;
                                                }

                                                Console.WriteLine("Do you want to add another pet? (yes/no):");
                                                string addAnother = Console.ReadLine()?.ToLower();
                                                if (addAnother != "yes")
                                                {
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid pet type. Please choose from (dog, cat, bunny, bird).");
                                            }
                                        }
                                    }
                                }


                                Console.Clear();
                                selectedFlight.Layout.PrintLayout();
                                totalFlightpoints += selectedFlight.FlightPoints;
                            }

                            // var bookedFlight = new BookedFlightsModel(selectedFlight.Id, selectedFlight.Layout.BookedSeats, false);1

                            List<BookedFlightsModel> bookedFlightModel = new List<BookedFlightsModel>
                        {
                            new BookedFlightsModel(selectedFlight.Id, selectedFlight.Layout.BookedSeats, baggageInfo, petInfo, false)

                        };
                            FlightsAccess.WriteAll(allFlights);
                            BookedFlightsAccess.WriteAll(currentAccount.EmailAddress, bookedFlightModel);
                        }
                        else if (confirmation.ToLower() == "no")
                        {
                            Console.WriteLine("Booking flight is cancelled, choose a new flight.\n");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input, try again.\n");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID selected. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric ID.");
                }
            }

        }
        else
        {
            if (flightModel != null)
            {
                Console.WriteLine($"\nYou have selected the following flight:\n");
                Console.WriteLine("{0,-20} {1,-35}", "Airline:", flightModel.Airline);
                Console.WriteLine("{0,-20} {1,-35}", "Departure Airport:", flightModel.DepartureAirport);
                Console.WriteLine("{0,-20} {1,-35}", "Arrival Destination:", flightModel.ArrivalDestination);
                Console.WriteLine("{0,-20} {1,-35}", "Flight Time:", flightModel.FlightTime);
                Console.WriteLine("{0,-20} {1,-35}", "Flight Date:", flightModel.DepartureDate);
                Console.WriteLine("{0,-20} {1,-35}", "Ticket Price:", flightModel.TicketPrice);
                Console.WriteLine("{0,-20} {1,-35}", "Available Seats:", flightModel.AvailableSeats);
                Console.WriteLine("{0,-20} {1,-35}", "Is Cancelled:", (flightModel.IsCancelled ? "Yes" : "No"));

                Console.Write("\nAre you sure you want to book this flight? (yes/no) ");
                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "yes")
                {
                    List<string> chosenSeats = new List<string>();
                    flightModel.Layout.PrintLayout();

                    while (true)
                    {
                        Console.Write("\nWhich seat do you want? (press Q to quit or Enter to confirm booking and keep choosing by seat number if you want more seats): ");
                        string seat = Console.ReadLine()?.ToUpper();

                        if (seat == "Q")
                        {
                            break;
                        }
                        else if (string.IsNullOrWhiteSpace(seat))
                        {
                            Console.WriteLine("Confirming your selected seats...");
                            flightModel.Layout.ConfirmBooking();
                            break;
                        }

                        // Get user initials
                        Console.Write("Enter your initials (2 characters): ");
                        string initials = Console.ReadLine()?.ToUpper() ?? "";
                        while (initials.Length != 2)
                        {
                            Console.Write("Please enter exactly 2 characters for your initials: ");
                            initials = Console.ReadLine()?.ToUpper() ?? "";
                        }

                        // Book the seat with initials
                        flightModel.Layout.BookFlight(seat, initials);

                        // Ask user if they want baggage
                        Console.WriteLine("Do you want to add baggage? (yes/no):");
                        string userBaggage = Console.ReadLine().ToLower();

                        while (userBaggage != "yes" && userBaggage != "no")
                        {
                            Console.WriteLine("Invalid input. Please enter 'yes' or 'no':");
                            userBaggage = Console.ReadLine().ToLower();
                        }

                        if (userBaggage == "no")
                        {
                            return;
                        }

                        if (userBaggage == "yes")
                        {
                            double totalWeightSeat = 0;
                            double feeBaggage = 0;
                            double totalCarryOnWeight = 0;

    
                            Console.WriteLine("You get 25kg of baggage per seat (this includes both checked and carry-on (enter yes to continue).");
                            string addCheckedBaggage = Console.ReadLine().ToLower();
                        

                        while (addCheckedBaggage != "yes" && addCheckedBaggage != "no")
                        {
                            Console.WriteLine("Invalid input. Please enter 'yes' or 'no':");
                            addCheckedBaggage = Console.ReadLine().ToLower();
                        }

                            if (addCheckedBaggage == "yes")
                            {
                                while (true)
                                {
                                    Console.WriteLine("Enter weight for checked baggage (choose 20kg or 25kg): ");
                                    double checkedWeight;

                                    // Ensure valid input for checked baggage weight
                                    if (double.TryParse(Console.ReadLine(), out checkedWeight))
                                    {
                                        if (checkedWeight == 20)
                                        {
                                            feeBaggage += 45;
                                            totalWeightSeat += 20;
                                            Console.WriteLine("Checked baggage of 20kg added for 45 EUR.");
                                            break;
                                        }
                                        else if (checkedWeight == 25)
                                        {
                                            feeBaggage += 50;
                                            totalWeightSeat += 25;

                                            Console.WriteLine("Checked baggage of 25kg added for 50 EUR.");
                                            Console.WriteLine("You have reached the 25kg per seat limit. No carry-on allowed.");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid weight. Please enter either 20kg or 25kg.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter a numeric value.");
                                    }
                                }
                            }

                            // Add Carry-On Baggage if Space Left
                            if (totalWeightSeat < 25)
                            {
                                double remainingCarryOnWeight = 25 - totalWeightSeat;
                                Console.WriteLine($"You have {remainingCarryOnWeight}kg left for carry-on baggage. Do you want to add carry-on baggage? (yes/no):");
                                string addCarryOn = Console.ReadLine().ToLower();

                                while (addCarryOn == "yes" && totalWeightSeat < 25)
                                {

                                    Console.WriteLine($"Enter weight for carry-on bag (max {Math.Min(remainingCarryOnWeight, 5)} per bag per bag, left weight: {25 - totalWeightSeat}kg): ");
                                    double carryOnWeight;

                                    // Ensure valid input for carry-on weight
                                    if (double.TryParse(Console.ReadLine(), out carryOnWeight))
                                    {
                                        if (carryOnWeight > 5)
                                        {
                                            Console.WriteLine("Carry-on weight cannot exceed 5kg per bag. Please enter a valid weight.");
                                            continue;
                                        }

                                        if (totalWeightSeat + carryOnWeight > 25)
                                        {
                                            Console.WriteLine("Adding this carry-on bag will exceed the 25kg limit. Please adjust the weight.");
                                            continue;
                                        }

                                        totalWeightSeat += carryOnWeight;
                                        totalCarryOnWeight += carryOnWeight;
                                        remainingCarryOnWeight -= carryOnWeight;
                                        Console.WriteLine($"Added a carry-on bag of {carryOnWeight}kg for €15.");

                                        if (totalCarryOnWeight < 5)
                                        {
                                            Console.WriteLine("Do you want to add another carry-on bag? (yes/no):");
                                            addCarryOn = Console.ReadLine().ToLower();
                                        }
                                        else
                                        {
                                            break; // No more carry-on weight available
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter a numeric value.");
                                    }
                                }
                            }

                                if (totalCarryOnWeight > 0)
                                {
                                    feeBaggage += 15;  
                                    Console.WriteLine($"Total carry-on baggage fee: €15.");
                                }

                            // Confirmation message
                            Console.WriteLine($"Baggage added successfully. Total weight: {totalWeightSeat}kg, Total fee: €{feeBaggage}.");
                            baggageInfo.Add(new BaggageLogic(initials, "checked + carry-on", totalWeightSeat) { Fee = feeBaggage });
                        }
  
        
                    

                        Console.WriteLine("Do you want to add a pet? max 7 pets (yes/no):");
                        string userPet = Console.ReadLine()?.ToLower();

                        if (userPet == "yes")
                        {
                            // Check current pet count for the flight
                            if (flightModel.TotalPets >= 7)
                            {
                                Console.WriteLine("Sorry, no more pet space available on this flight.");
                                break;
                            }
                            else
                            {
                                while (true)
                                {
                                    Console.WriteLine("What type of pet do you have? (dog, cat, bunny, bird): ");
                                    string petType = Console.ReadLine()?.ToLower();

                                    if (petType == "dog" || petType == "cat" || petType == "bunny" || petType == "bird")
                                    {
                                        var newPet = new PetLogic(petType) { Fee = 50.0 }; // Adds a new pet with a 50 EUR fee
                                        petInfo.Add(newPet);
                                        flightModel.TotalPets++; // Increment total pets for the flight
                                        Console.WriteLine($"Pet {petType} added. Fee: 50 EUR."); // Notify the user of the fee

                                        if (flightModel.TotalPets >= 7)
                                        {
                                            Console.WriteLine("Maximum pet capacity reached for this flight.");
                                            break;
                                        }

                                        Console.WriteLine("Do you want to add another pet? (yes/no):");
                                        string addAnother = Console.ReadLine()?.ToLower();
                                        if (addAnother != "yes")
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid pet type. Please choose from (dog, cat, bunny, bird).");
                                    }
                                }
                            }
                        }


                        Console.Clear();
                        flightModel.Layout.PrintLayout();
                    }

                    // var bookedFlight = new BookedFlightsModel(flightModel.Id, flightModel.Layout.BookedSeats, false);
                    // bookedFlight.FlightPoints += selecte

                    List<BookedFlightsModel> bookedFlightModel = new List<BookedFlightsModel>
                        {
                            new BookedFlightsModel(flightModel.Id, flightModel.Layout.BookedSeats, baggageInfo,petInfo, false)
                        };

                    var index = allFlights.FindIndex(f => f.Id == flightModel.Id);
                    if (index >= 0)
                    {
                        allFlights[index] = flightModel;
                    }

                    // Write allFlights to the data store to persist changes
                    FlightsAccess.WriteAll(allFlights);
                    BookedFlightsAccess.WriteAll(currentAccount.EmailAddress, bookedFlightModel);
                }
                else if (confirmation.ToLower() == "no")
                {
                    Console.WriteLine("Booking flight is cancelled, choose a new flight.\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid input, try again.\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

    }


    // public static void CancelBookedFlightMenu()
    // {
    //     Console.WriteLine("=== Cancel Booked Flight ===\n");

    //     var currentAccount = AccountsLogic.CurrentAccount;
    //     var email = currentAccount.EmailAddress;

    //     if (!allBookedFlights.ContainsKey(email) || allBookedFlights[email].Count == 0)
    //     {
    //         Console.WriteLine("No booked tickets.");
    //         return;
    //     }

    //     var bookedFlightModels = new List<FlightModel>();
    //     var bookedFlights = allBookedFlights[email];
    //     int index = 0;

    //     foreach (var bookedFlightModel in bookedFlights)
    //     {
    //         var flightModel = BookFlightLogic.SearchFlightByID(bookedFlightModel.FlightID);
    //         if (flightModel != null)
    //         {
    //             bookedFlightModels.Add(flightModel);
    //         }
    //     }

    //     if (bookedFlightModels.Count > 0)
    //     {
    //         Console.WriteLine("Your booked flights:\n");
    //         Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-28} {6, -18}",
    //                           "ID", "Airline", "Departure Airport", "Arrival Destination",
    //                           "Flight Time", "Cancelled by airline", "Cancelled by customer");

    //         Console.WriteLine(new string('-', 220));

    //         foreach (var flight in bookedFlightModels)
    //         {
    //             Console.WriteLine("{0,-5} {1,-25} {2,-55} {3,-60} {4,-15} {5,-28} {6, -18}",
    //                               flight.Id,
    //                               flight.Airline,
    //                               flight.DepartureAirport,
    //                               flight.ArrivalDestination,
    //                               flight.FlightTime,
    //                               flight.IsCancelled ? "Yes" : "No",
    //                               bookedFlights[index].IsCancelled ? "Yes" : "No");
    //             index++;
    //         }

    //         Console.Write("\nEnter the ID of the flight you wish to cancel (or 'Q' to quit): ");
    //         string input = Console.ReadLine();

    //         if (input.ToUpper() == "Q")
    //         {
    //             Console.WriteLine("Exiting cancellation process.");
    //             MenuLogic.PopMenu();
    //             return;
    //         }

    //         if (int.TryParse(input, out int flightToCancelId))
    //         {
    //             var bookedFlightToCancel = bookedFlights.FirstOrDefault(b => b.FlightID == flightToCancelId);
    //             if (bookedFlightToCancel != null)
    //             {
    //                 bookedFlightToCancel.IsCancelled = true;
    //                 Console.WriteLine($"Flight ID {flightToCancelId} has been successfully cancelled.");
    //             }
    //             else
    //             {
    //                 Console.WriteLine("Invalid Flight ID. Please try again.");
    //             }
    //         }
    //         else
    //         {
    //             Console.WriteLine("Invalid input. Please enter a numeric ID or 'Q' to quit.");
    //         }
    //     }
    //     else
    //     {
    //         Console.WriteLine("You have no booked flights.");
    //     }
    // }
}


