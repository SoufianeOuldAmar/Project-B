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

            Console.Write("\nAre you sure you want to book this flight? (yes/no): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "yes")
            {
                List<string> chosenSeats = new List<string>();
                flightModel.Layout.PrintLayout();

                while (true)
                {
                    Console.Clear();

                    flightModel.Layout.PrintLayout();

                    Console.Write("\nWhich seat do you want? (press Q to quit or Enter to confirm booking and keep choosing by seat number if you want more seats): ");
                    string seat = Console.ReadLine()?.ToUpper();

                    if (flightModel.Layout.BookedSeats.Contains(seat))
                    {
                        Console.WriteLine($"\nSeat ({seat}) is already booked. Choose another one");
                        MenuPresentation.PressAnyKey();
                        continue;
                    }

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

                    Console.Write("Do you want to add baggage (yes/no): ");
                    string userBaggage = Console.ReadLine().ToLower();

                    if (userBaggage == "yes")
                    {
                        Console.Write("Enter the number for the baggage (1) carry on, (2) checked or (3) both): ");
                        string baggageType = Console.ReadLine().ToLower();
                        double weightBaggage = 0;
                        double feeBaggage = 0;

                        if (baggageType == "1")
                        {
                            Console.Write("Enter weight for carry on (in kg): ");
                            weightBaggage = double.Parse(Console.ReadLine());

                            if (weightBaggage > 10)
                            {
                                feeBaggage = 50;
                                Console.WriteLine($"Your carry on goes over the 10kg limit. you'll have to pay a fee of {feeBaggage} EUR.");
                            }

                        }
                        else if (baggageType == "2" || baggageType == "3")
                        {
                            while (true)
                            {
                                Console.WriteLine("Enter weight for checked baggage (in kg) choose 20 or 25 : ");
                                weightBaggage = double.Parse(Console.ReadLine());

                                if (weightBaggage == 20)
                                {
                                    Console.WriteLine("Your checked baggage weight is 20 kg. No additional fee  required.");
                                    break;
                                }
                                else if (weightBaggage == 25)
                                {
                                    Console.WriteLine("Your checked baggage weight is 25 kg. No additional fee is required.");
                                    break;
                                }
                                else if (weightBaggage > 25)
                                {
                                    feeBaggage = 50;
                                    Console.WriteLine($"Your checked baggage weight exceeds the 25 kg limit. You'll have to pay a fee of {feeBaggage} EUR.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid weight. Please enter a valid weight for your checked baggage.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid baggage type. Please choose between carry on' or checked.");
                            continue;
                        }

                        baggageInfo.Add(new BaggageLogic(initials, baggageType, weightBaggage) { Fee = feeBaggage });


                    }
                    Console.Write("Do you want to add a pet? max 7 pets (yes/no): ");
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
                MenuPresentation.PressAnyKey();
            }
            else
            {
                Console.WriteLine("Invalid input, try again.\n");
                MenuPresentation.PressAnyKey();
            }
        }


    }

}

