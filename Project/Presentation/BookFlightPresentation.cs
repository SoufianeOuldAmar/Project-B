using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;
using DataAccess;
using System.Threading;
using PresentationLayer;

public static class BookFlightPresentation
{
    public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
    public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();
    public static AccountModel currentAccount = AccountsLogic.CurrentAccount;
    public static List<FoodAndDrinkItem> selectedItems = new List<FoodAndDrinkItem>();

    public static List<BaggageLogic> baggageInfo = new List<BaggageLogic>();

    private static string GenerateInitials(PassengerModel passenger)
    {
        if (string.IsNullOrWhiteSpace(passenger.FirstName) || string.IsNullOrWhiteSpace(passenger.LastName))
        {
            throw new ArgumentException("First name and last name must not be empty");
        }

        return $"{passenger.FirstName[0]}{passenger.LastName[0]}".ToUpper();
    }

    private static void ProcessPassengerDetails(PassengerModel passenger, string seat, ref double totalPrice, double baseTicketPrice, string initials, List<PassengerModel> passengers, List<string> chosenSeats, List<BaggageLogic> baggageInfo, List<PetLogic> petInfo, FlightModel flight)
    {


        // Add passenger and calculate price
        passengers.Add(passenger);
        chosenSeats.Add(seat);

        double seatPrice = baseTicketPrice;
        if (passenger.AgeGroup == "child")
        {
            seatPrice *= 0.75; // 25% discount for children
        }
        else if (passenger.AgeGroup == "infant")
        {
            seatPrice *= 0.1; // 90% discount for infants
        }
        totalPrice += seatPrice;

        double maxBaggageCapacity = 2500;
        double currentTotalWeight = 0;
        double carryOnWeight = 0;
        double totalFee = 0;


        Console.Write("Do you want to add baggage? (yes/no): ");
        string choice = Console.ReadLine().ToLower();

        while (choice != "yes" && choice != "no")
        {
            continue;
        }

        if (choice == "no")
        {
            return;
        }

        if (choice == "yes")
        {
            Console.Write("Do you want to add a carry-on bag (15 EUR fee)? (yes/no): ");
            string carryOnChoice = Console.ReadLine().ToLower();

            while (carryOnChoice != "yes" && carryOnChoice != "no")
            {
                Console.WriteLine("Invalid input. Please choose between yes or no");
                carryOnChoice = Console.ReadLine().ToLower();
            }

            if (carryOnChoice == "yes")
            {
                carryOnWeight = 10;
                totalFee += 15;
                Console.WriteLine("Carry-on bag added with a weight of 10kg and a fee of 15 EUR.");
            }

            // Ask about checked baggage only once
            Console.Write("Do you want to add checked baggage? (yes/no): ");
            string checkChecked = Console.ReadLine().ToLower();

            while (checkChecked != "yes" && checkChecked != "no")
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no':");
                checkChecked = Console.ReadLine().ToLower();
            }

            if (checkChecked == "yes")
            {
                Console.Write("Do you want a 20kg or 25kg checked bag? Enter 20 or 25: ");
                string checkedWeightInput = Console.ReadLine();

                double bagWeight;
                if (!double.TryParse(checkedWeightInput, out bagWeight) || (bagWeight != 20 && bagWeight != 25))
                {
                    Console.WriteLine("Invalid input. You can only choose between 20 or 25 kg.");
                    return; // Exit if input is invalid
                }

                Console.Write("How many of these bags do you want?: ");
                string bagCountInput = Console.ReadLine();

                int bagCount;
                if (!int.TryParse(bagCountInput, out bagCount) || bagCount <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a positive number.");
                    return; // Exit if input is invalid
                }

                double totalBagWeight = bagWeight * bagCount;

                if (currentTotalWeight + totalBagWeight > maxBaggageCapacity)
                {
                    Console.WriteLine("Can't add bag(s). It exceeds the flight's baggage capacity.");
                    return; // Exit if capacity is exceeded
                }

                currentTotalWeight += totalBagWeight;
                totalFee += bagWeight * bagCount;

                Console.WriteLine($"{bagCount} checked bag(s) of {bagWeight}kg each added with a total fee of {bagWeight * bagCount} EUR. Current total weight: {currentTotalWeight}kg");

                baggageInfo.Add(new BaggageLogic(initials, "Checked + Carry-On", currentTotalWeight) { Fee = totalFee });

                Console.WriteLine("Baggage summary:");
                foreach (var bag in baggageInfo)
                {
                    Console.WriteLine($"Initials: {bag.Initials}, Type: {bag.BaggageType}, Weight: {bag.BaggageWeight}kg, Fee: {bag.Fee} EUR");
                }

                Console.WriteLine("Total weight of all baggage: " + currentTotalWeight + "kg");
                Console.WriteLine("Total fee for all baggage: " + totalFee + " EUR");
            }
        }



        // Pet handling
        Console.Write("Do you want to add a pet? (yes/no): ");
        string userPet = Console.ReadLine()?.ToLower();

        if (userPet == "yes")
        {
            if (flight.TotalPets >= 7)
            {
                Console.WriteLine("Sorry, no more pet space available on this flight.");
            }
            else
            {
                while (true)
                {
                    Console.Write("What type of pet do you have? (dog, cat, bunny, bird): ");
                    string petType = Console.ReadLine()?.ToLower();

                    Console.Write("Enter your animal's name: ");
                    string petName = Console.ReadLine();

                    if (petType == "dog" || petType == "cat" || petType == "bunny" || petType == "bird")
                    {
                        var newPet = new PetLogic(petType, petName) { Fee = 50.0 };
                        petInfo.Add(newPet);
                        flight.TotalPets++;
                        Console.WriteLine($"Pet {petType} named {petName} added. Fee: 50 EUR.");

                        if (flight.TotalPets >= 7)
                        {
                            Console.WriteLine("Maximum pet capacity reached for this flight.");
                            break;
                        }

                        Console.Write("Do you want to add another pet? (yes/no): ");
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
    }

    public static void BookFlightMenu(bool searchFlightFunction = false, FlightModel flightModel = null, bool showFoodAndDrinks = true)
    {

        var currentAccount = AccountsLogic.CurrentAccount;
        List<BaggageLogic> baggageInfo = new List<BaggageLogic>();
        List<PetLogic> petInfo = new List<PetLogic>();
        List<PassengerModel> passengers = new List<PassengerModel>();
        double totalPrice = 0;
        bool quit = false;

        FlightModel selectedFlight = flightModel;

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

            Console.Write("\nAre you sure you want to book this flight? (yes/no): ");
            string confirmation = Console.ReadLine();
            Console.Clear();

            if (confirmation.ToLower() == "yes")
            {
                List<string> chosenSeats = new List<string>();
                List<double> foodAndDrinkCosts = new List<double>(); // Houd kosten per passagier bij

                // if (searchFlightFunction)
                // {
                //     BookFlightLogic.LoadExistingBookings(selectedFlight, currentAccount.EmailAddress);
                // }



                LayoutPresentation.PrintLayout(selectedFlight.Layout);
                while (true)
                {
                    if (quit) break;

                    Console.Write("\nWhich seat do you want? (press Q to quit or Enter to confirm booking and keep choosing by seat number if you want more seats): ");
                    string seat = Console.ReadLine()?.ToUpper();

                    if (seat == "Q")
                    {
                        while (true)
                        {
                            Console.Write("Are you sure you want to quit. All your progress will get lost (yes/no): ");
                            string quitYesNo = Console.ReadLine();

                            if (quitYesNo == "no")
                            {
                                continue;
                            }
                            else if (quitYesNo == "yes")
                            {
                                quit = true;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong input, try again.");
                            }
                        }

                    }
                    else if (string.IsNullOrWhiteSpace(seat))
                    {
                        if (chosenSeats.Count == 0)
                        {
                            Console.WriteLine("Please select at least one seat.");
                            continue;
                        }
                        Console.WriteLine("Confirming your selected seats...");
                        break;
                    }

                    if (!selectedFlight.Layout.TryBookSeat(seat))
                    {
                        Console.WriteLine("This seat is already booked or invalid. Please choose another seat.");
                        continue;
                    }

                    // selectedFlight.Layout.ChosenSeats.Add(seat);


                    PassengerModel passenger = new PassengerModel();
                    Console.WriteLine($"\nEnter Passenger Information for seat {seat}:");

                    while (true)
                    {
                        Console.Write("First Name: ");
                        string firstName = Console.ReadLine();
                        // Allow letters and spaces, but ensure it's not just spaces
                        if (!string.IsNullOrWhiteSpace(firstName) && firstName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && firstName.Trim().Length > 0)
                        {
                            passenger.FirstName = firstName;
                            break;
                        }
                        Console.WriteLine("Invalid First Name. Please enter letters only (spaces are allowed).");
                    }

                    while (true)
                    {
                        Console.Write("Last Name: ");
                        string lastName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(lastName) && lastName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && lastName.Trim().Length > 0)
                        {
                            passenger.LastName = lastName;
                            break;
                        }
                        Console.WriteLine("Invalid Last Name. Please enter letters only (spaces are allowed).");
                    }

                    while (true)
                    {
                        Console.Write("Title (Mr/Ms/Dr): ");
                        string title = Console.ReadLine();
                        if (new[] { "Mr", "Ms", "Dr" }.Contains(title))
                        {
                            passenger.Title = title;
                            break;
                        }
                        Console.WriteLine("Invalid Title. Please choose from (Mr, Ms, Dr).");
                    }

                    while (true)
                    {
                        Console.Write("Age Group (adult/child/infant): ");
                        string ageGroup = Console.ReadLine().ToLower();
                        if (new[] { "adult", "child", "infant" }.Contains(ageGroup))
                        {
                            passenger.AgeGroup = ageGroup;

                            if (ageGroup == "infant")
                            {
                                while (true)
                                {
                                    Console.Write("Date of Birth (dd-MM-yyyy): ");
                                    string dobInput = Console.ReadLine();
                                    if (DateTime.TryParseExact(dobInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dob)
                                        && dob <= DateTime.Now)
                                    {
                                        passenger.DateOfBirth = dob;
                                        break;
                                    }
                                    Console.WriteLine("Invalid Date of Birth. Please enter a valid past date in format dd-MM-yyyy.");
                                }
                            }
                            break;
                        }
                        Console.WriteLine("Invalid Age Group. Please enter 'adult', 'child', or 'infant'.");
                    }

                    Console.Write("Would you like to add food and drinks for this passenger? (yes/no): ");
                    string addFoodOption = Console.ReadLine()?.ToLower();

                    while (addFoodOption != "yes" && addFoodOption != "no")
                    {
                        Console.WriteLine("Invalid input. Please enter 'yes' or 'no': ");
                        addFoodOption = Console.ReadLine()?.ToLower();
                    }

                    if (addFoodOption == "yes")
                    {
                        var allItems = FoodAndDrinkPresentation.AddFoodAndDrinksToBooking(selectedFlight);
                        double foodCost = allItems.Item1;
                        selectedItems = allItems.Item2;

                        foodAndDrinkCosts.Add(foodCost); // Voeg toe aan lijst met food and drink kosten
                        totalPrice += foodCost;
                        Console.WriteLine($"Food and drinks have been added. Updated total price: €{totalPrice:F2}");
                    }
                    else
                    {
                        Console.WriteLine("No food and drinks were added for this passenger.");
                    }

                    // try
                    // {
                    string initials = GenerateInitials(passenger);
                    selectedFlight.Layout.BookFlight(seat, initials);
                    ProcessPassengerDetails(passenger, seat, ref totalPrice, selectedFlight.TicketPrice, initials, passengers, chosenSeats, baggageInfo, petInfo, selectedFlight);

                    // list of all payments to save
                    List<Payement> allPayments = new List<Payement>();

                    // Add the ticket payment
                    Payement ticketPayment = new Payement("Ticket", selectedFlight.TicketPrice, DateTime.Now);
                    allPayments.Add(ticketPayment);

                    // Add the baggage payments
                    foreach (var baggage in baggageInfo)
                    {
                        Payement baggagePayment = new Payement("Baggage", baggage.Fee, DateTime.Now);
                        allPayments.Add(baggagePayment);
                        selectedFlight.Layout.BookFlight(seat, initials);
                        // ProcessPassengerDetails(passenger, seat, ref totalPrice, selectedFlight.TicketPrice, initials, passengers, chosenSeats, baggageInfo, petInfo, selectedFlight);
                    }

                    // Add the pet payments
                    foreach (var pet in petInfo)
                    {
                        Payement petPayment = new Payement("Pet", pet.Fee, DateTime.Now);
                        allPayments.Add(petPayment);
                    }

                    // Save all payments at once
                    // FinancialReportAccess.SavePayements(allPayments);
                    DataAccessClass.WriteList<Payement>("DataSources/FinancialReport.json", allPayments);

                    Console.Clear();
                    LayoutPresentation.PrintLayout(selectedFlight.Layout);
                }

                if (quit) return;

                // Show booking summary
                Console.Clear();
                Console.WriteLine("\n=== Booking Summary ===");
                Console.WriteLine($"Flight: {selectedFlight.Airline}");
                Console.WriteLine($"Route: {selectedFlight.DepartureAirport} to {selectedFlight.ArrivalDestination}");
                Console.WriteLine($"Date: {selectedFlight.DepartureDate}, Time: {selectedFlight.FlightTime}");

                Console.WriteLine("\nPassenger Details:");
                for (int i = 0; i < passengers.Count; i++)
                {
                    var p = passengers[i];
                    Console.WriteLine($"{i + 1}. {p.Title} {p.FirstName} {p.LastName}");
                    Console.WriteLine($"   Seat: {chosenSeats[i]}");
                    Console.WriteLine($"   Age Group: {p.AgeGroup}");
                    if (p.DateOfBirth.HasValue)
                    {
                        Console.WriteLine($"   Date of Birth: {p.DateOfBirth.Value:dd-MM-yyyy}");
                    }
                }

                // Calculate final price including fees
                double foodAndDrinkCost = foodAndDrinkCosts.Sum();
                double baggageTotalFee = baggageInfo.Sum(b => b.Fee);
                double petTotalFee = petInfo.Sum(p => p.Fee);
                totalPrice += baggageTotalFee + petTotalFee;

                Console.WriteLine($"\nPrice Breakdown:");
                Console.WriteLine($"Ticket(s): {totalPrice - baggageTotalFee - petTotalFee - foodAndDrinkCost:C}");
                if (baggageTotalFee > 0) Console.WriteLine($"Baggage Fees: {baggageTotalFee:C}");
                if (petTotalFee > 0) Console.WriteLine($"Pet Fees: {petTotalFee:C}");
                if (foodAndDrinkCost > 0) Console.WriteLine($"Food and Drinks: {foodAndDrinkCost:C}");
                Console.WriteLine($"Total Price: {totalPrice:C}");

                var sss = BookedFlightsAccess.LoadAll();
                int allFlightPoints = currentAccount.TotalFlightPoints;

                while (true)
                {
                    Console.Write($"\nBefore confirming your booking, do you want to use your flight points for a discount? You have {allFlightPoints} points. (yes/no): ");
                    string discountYesOrNo = Console.ReadLine()?.Trim().ToLower();

                    if (discountYesOrNo == "yes")
                    {
                        if (allFlightPoints > 0)
                        {
                            while (true)
                            {
                                Console.Write("How many points would you like to use? (1 point equals 1 euro, and you can use your points for up to a 20% discount on the price.) (Enter 'Q' to quit.): ");
                                string amountFlightPointsStr = Console.ReadLine();

                                if (int.TryParse(amountFlightPointsStr, out int amountFlightPoints) && amountFlightPoints >= 0)
                                {
                                    double discountToApply = FlightPointsLogic.CalculateFlightPoint(amountFlightPoints, totalPrice, allFlightPoints);

                                    totalPrice -= discountToApply;
                                    currentAccount.TotalFlightPoints -= (int)discountToApply;

                                    Console.WriteLine($"You used {discountToApply:C} worth of flight points.");
                                    Console.WriteLine($"Updated Total Price: {totalPrice:C}");
                                    break; // Exit the inner loop after successfully applying the discount
                                }
                                else if (amountFlightPointsStr.ToUpper() == "Q")
                                {
                                    break; // Exit the inner loop if user quits
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                                }
                            }

                            break; // Exit the outer loop after processing the discount
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough flight points for a discount.");
                            break; // Exit the outer loop if the user doesn't have enough points
                        }
                    }
                    else if (discountYesOrNo == "no")
                    {
                        Console.WriteLine("You chose not to use flight points.");
                        break; // Exit the outer loop if the user doesn't want to use points
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please answer with 'yes' or 'no'.");
                    }
                }




                Console.Write("\nConfirm booking? (yes/no): ");
                string finalConfirmation = Console.ReadLine().ToLower();
                if (finalConfirmation == "yes")
                {
                    selectedFlight.Layout.ConfirmBooking();

                    // var existingPassengers = PassengerAccess.LoadPassengers();
                    var existingPassengers = DataAccessClass.ReadList<PassengerModel>("DataSources/passengers.json");

                    existingPassengers.AddRange(passengers);
                    // PassengerAccess.SavePassengers(existingPassengers);
                    DataAccessClass.WriteList<PassengerModel>("DataSources/passengers.json", existingPassengers);

                    var bookedFlight1 = new BookedFlightsModel(selectedFlight.Id, selectedFlight.Layout.BookedSeats, baggageInfo, petInfo, false);
                    bookedFlight1.TicketBill += totalPrice;

                    foreach (var seat in chosenSeats)
                    {
                        if (selectedFlight.Layout.SeatInitials.ContainsKey(seat))
                        {
                            bookedFlight1.SeatInitials[seat] = selectedFlight.Layout.SeatInitials[seat];
                        }
                    }

                    bookedFlight1.Email = currentAccount.EmailAddress;

                    List<BookedFlightsModel> bookedFlightModel = new List<BookedFlightsModel>
    {
        bookedFlight1
    };

                    var existingBookings = BookedFlightsAccess.LoadByEmail(currentAccount.EmailAddress);

                    existingBookings.RemoveAll(b => b.FlightID == selectedFlight.Id);

                    existingBookings.Add(bookedFlight1);

                    DataAccessClass.WriteList<AccountModel>("DataSources/accounts.json", AccountsLogic._accounts);
                    DataAccessClass.UpdateCurrentAccount(currentAccount);

                    foreach (var bookedFlight in bookedFlightModel)
                    {
                        BookFlightLogic.RemoveDuplicateSeats(bookedFlight);
                    }

                    BookedFlightsAccess.SaveSingle(currentAccount.EmailAddress, bookedFlight1);
                    // BookedFlightsAccess.WriteAll(currentAccount.EmailAddress, existingBookings);


                    selectedFlight.Layout.BookedSeats = selectedFlight.Layout.BookedSeats.Distinct().ToList();

                    for (int i = 0; i < allFlights.Count; i++)
                    {
                        if (allFlights[i].Id == selectedFlight.Id)
                        {
                            allFlights[i] = selectedFlight;
                            break;
                        }
                    }


                    DataAccessClass.WriteList<FlightModel>("DataSources/flights.json", allFlights);

                    Console.WriteLine("\nBooking confirmed successfully!");
                    Console.WriteLine("All passenger information has been saved.");
                }
                else
                {
                    Console.WriteLine("\nBooking cancelled.");
                }
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
        else
        {
            Console.WriteLine("Invalid flight selection. Please try again.");
        }
    }
}