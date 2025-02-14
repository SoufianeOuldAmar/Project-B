using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;
using DataAccess;
using System.Threading;
using PresentationLayer;

public static class BookFlightPresentation
{
    public static UserAccountModel currentAccount = UserAccountLogic.CurrentAccount;
    public static List<FoodAndDrinkItem> selectedItems = new List<FoodAndDrinkItem>();

    private static string GenerateInitials(PassengerModel passenger)
    {
        if (string.IsNullOrWhiteSpace(passenger.FirstName) || string.IsNullOrWhiteSpace(passenger.LastName))
        {
            throw new ArgumentException("First name and last name must not be empty");
        }

        return $"{passenger.FirstName[0]}{passenger.LastName[0]}".ToUpper();
    }

    private static void ProcessPassengerDetails(PassengerModel passenger, string seat, ref double totalPrice, double baseTicketPrice, string initials, FlightModel flight)
    {   
        // Add passenger and calculate price
        BookFlightLogic.passengers.Clear();
        BookFlightLogic.passengers.Add(passenger);
        BookFlightLogic.chosenSeats.Add(seat);

        totalPrice = BookFlightLogic.CalculateSeatPrice(passenger, baseTicketPrice, seat, BookFlightLogic.chosenSeats, totalPrice);

        double currentTotalWeight = 0;
        double carryOnWeight = 0;
        double totalFee = 0;

    AddingBaggage:
        Console.Write("Do you want to add baggage? (yes/no): ");
        string choice = Console.ReadLine().ToLower();

        while (choice != "yes" && choice != "no")
        {
            MenuPresentation.PrintColored("Invalid input. Enter either 'yes' or no.", ConsoleColor.Red);
            goto AddingBaggage;
        }

        if (choice == "no")
        {
            MenuPresentation.PrintColored("You have chosen not to add baggage to your booking.", ConsoleColor.Yellow);
        }

        if (choice == "yes")
        {
            Console.Write("Do you want to add a carry-on bag (15 EUR fee)? (yes/no): ");
            string carryOnChoice = Console.ReadLine().ToLower();

            while (carryOnChoice != "yes" && carryOnChoice != "no")
            {
                MenuPresentation.PrintColored("Invalid input. Please choose between yes or no", ConsoleColor.Red);
                Console.Write("Do you want to add a carry-on bag (15 EUR fee)? (yes/no): ");
                carryOnChoice = Console.ReadLine().ToLower();
            }

            if (carryOnChoice == "yes")
            {
                carryOnWeight = 10;
                totalFee += 15;
                MenuPresentation.PrintColored("Carry-on bag added with a weight of 10kg and a fee of 15 EUR.", ConsoleColor.Yellow);
            }

            // Ask about checked baggage only once
            Console.Write("Do you want to add checked baggage? (yes/no): ");
            string checkChecked = Console.ReadLine().ToLower();

            while (checkChecked != "yes" && checkChecked != "no")
            {
                MenuPresentation.PrintColored("Invalid input. Please enter 'yes' or 'no':", ConsoleColor.Red);
                Console.Write("Do you want to add checked baggage? (yes/no): ");
                checkChecked = Console.ReadLine().ToLower();
            }

            if (checkChecked == "yes")
            {
            BaggageWeightCheck:
                Console.Write("Do you want a 20kg or 25kg checked bag? Enter 20 or 25: ");
                string checkedWeightInput = Console.ReadLine();

                (bool, double) isValidBaggageWeightMethod = BookFlightLogic.IsValidBaggageWeight(checkedWeightInput);
                bool isValidBaggageWeight = isValidBaggageWeightMethod.Item1;
                double bagWeight = isValidBaggageWeightMethod.Item2;

                if (!isValidBaggageWeight)
                {
                    MenuPresentation.PrintColored("Invalid input. You can only choose between 20 or 25 kg.", ConsoleColor.Red);
                    goto BaggageWeightCheck;
                }
            MaxBagWeightCheck:
                Console.Write("How many of these bags do you want?: ");
                string bagCountInput = Console.ReadLine();

                int bagCount;
                if (!int.TryParse(bagCountInput, out bagCount) || bagCount <= 0)
                {
                    MenuPresentation.PrintColored("Invalid input. Please enter a positive number.", ConsoleColor.Red);
                    goto MaxBagWeightCheck;
                    return; // Exit if input is invalid
                }

                double totalBagWeight = BookFlightLogic.CalculateTotalBagWeight(bagWeight, bagCount);

                if (BookFlightLogic.IsValidMaxBaggageWeight(currentTotalWeight, totalBagWeight))
                {
                    MenuPresentation.PrintColored("Can't add bag(s). It exceeds the flight's baggage capacity.", ConsoleColor.Red);
                    goto MaxBagWeightCheck;
                }

                currentTotalWeight += totalBagWeight;
                totalFee = BookFlightLogic.CalculateTotalBagWeight(bagWeight, bagCount);

                string baggageType = "";

                baggageType = BookFlightLogic.DetermineBaggageType(carryOnChoice, checkChecked);

                Console.WriteLine($"{bagCount} checked bag(s) of {bagWeight}kg each added with a total fee of {bagWeight * bagCount} EUR. Current total weight: {currentTotalWeight}kg");

                BookFlightLogic.baggageInfo.Add(new BaggageModel(initials, baggageType, currentTotalWeight) { Fee = totalFee });

                MenuPresentation.PrintColored("Baggage summary:", ConsoleColor.Yellow);

                foreach (var bag in BookFlightLogic.baggageInfo)
                {
                    MenuPresentation.PrintColored($"Initials: {bag.Initials}, Type: {bag.BaggageType}, Weight: {bag.BaggageWeight}kg, Fee: {bag.Fee} EUR", ConsoleColor.Yellow);
                }

                MenuPresentation.PrintColored("Total weight of all baggage: " + currentTotalWeight + "kg", ConsoleColor.Yellow);
                MenuPresentation.PrintColored("Total fee for all baggage: " + totalFee + " EUR", ConsoleColor.Yellow);
            }
        }



    // Pet handling
    AddPet:
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

                    if (BookFlightLogic.IsValidPetType(petType))
                    {
                        var newPet = new PetModel(petType, petName) { Fee = 50.0 };
                        BookFlightLogic.petInfo.Add(newPet);
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
                        MenuPresentation.PrintColored("Invalid pet type. Please choose from (dog, cat, bunny, bird).", ConsoleColor.Red);
                    }
                }
            }
        }
        else if (userPet == "no")
        {
            MenuPresentation.PrintColored("You have chosen not to add a pet to your booking.", ConsoleColor.Yellow);
        }
        else
        {
            MenuPresentation.PrintColored("Invalid input! Enter either 'yes' or 'no'!", ConsoleColor.Red);
            goto AddPet;
        }

        MenuPresentation.PrintColored("\nBooking seat completed!", ConsoleColor.Green);
        MenuPresentation.PressAnyKey();
    }

    public static void BookFlightMenu(bool searchFlightFunction = false, FlightModel flightModel = null, bool showFoodAndDrinks = true)
    {   
        var currentAccount = UserAccountLogic.CurrentAccount;
        double totalPrice = 0;
        bool quit = false;

        FlightModel selectedFlight = flightModel;

    bookFlightConfirm:
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
                        if (BookFlightLogic.chosenSeats.Count == 0)
                        {
                            Console.WriteLine("Please select at least one seat.");
                            continue;
                        }
                        Console.WriteLine("Confirming your selected seats...");
                        break;
                    }

                    if (!LayoutLogic.TryBookSeat(selectedFlight.Layout, seat))
                    {
                        MenuPresentation.PrintColored("\nThis seat is already booked or invalid. Please choose another seat.", ConsoleColor.Red);
                        continue;
                    }

                    // selectedFlight.Layout.ChosenSeats.Add(seat);

                    PassengerModel passenger = new PassengerModel(PassengerLogic.GetPassengerID());
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
                        MenuPresentation.PrintColored("Invalid First Name. Please enter letters only (spaces are allowed).", ConsoleColor.Red);
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
                        MenuPresentation.PrintColored("Invalid Last Name. Please enter letters only (spaces are allowed).", ConsoleColor.Red);
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
                        MenuPresentation.PrintColored("Invalid Title. Please choose from (Mr, Ms, Dr).", ConsoleColor.Red);
                        Console.ResetColor();
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

                                    MenuPresentation.PrintColored("Invalid Date of Birth. Please enter a valid past date in format dd-MM-yyyy.", ConsoleColor.Red);
                                }
                            }
                            break;
                        }
                        MenuPresentation.PrintColored("Invalid Age Group. Please enter 'adult', 'child', or 'infant'.", ConsoleColor.Red);
                    }

                    Console.Write("Would you like to add food and drinks for this passenger? (yes/no): ");
                    string addFoodOption = Console.ReadLine()?.ToLower();

                    while (addFoodOption != "yes" && addFoodOption != "no")
                    {
                        MenuPresentation.PrintColored("Invalid input. Please enter 'yes' or 'no'", ConsoleColor.Red);
                        Console.Write("Would you like to add food and drinks for this passenger? (yes/no): ");

                        addFoodOption = Console.ReadLine()?.ToLower();
                    }

                    if (addFoodOption == "yes")
                    {
                        var allItems = FoodAndDrinkPresentation.AddFoodAndDrinksToBooking(selectedFlight);
                        double foodCost = allItems.Item1;
                        BookFlightLogic.selectedItems = allItems.Item2;

                        BookFlightLogic.foodAndDrinkCosts.Add(foodCost); // Voeg toe aan lijst met food and drink kosten
                        totalPrice += foodCost;

                        if (BookFlightLogic.selectedItems.Count > 0) Console.WriteLine($"Food and drinks have been added. Updated total price: €{totalPrice:F2}");
                    }
                    else
                    {
                        MenuPresentation.PrintColored("No food and drinks were added for this passenger.", ConsoleColor.Yellow);
                    }

                    // try
                    // {
                    string initials = GenerateInitials(passenger);
                    LayoutLogic.BookFlight(selectedFlight.Layout, seat, initials);
                    ProcessPassengerDetails(passenger, seat, ref totalPrice, selectedFlight.TicketPrice, initials, selectedFlight);

                    FinancialReportLogic.SavePayments(selectedFlight, seat, initials);

                    Console.Clear();
                    LayoutPresentation.PrintLayout(selectedFlight.Layout);
                }

                if (quit) return;

                // Show booking summary
                ConfirmOrder(selectedFlight, totalPrice);
            }
            else if (confirmation.ToLower() == "no")
            {
                Console.WriteLine("Booking flight is cancelled, choose a new flight.\n");
            }
            else
            {
                Console.WriteLine("Invalid input, try again.");
                goto bookFlightConfirm;
            }
        }
        else
        {
            Console.WriteLine("Invalid flight selection. Please try again.");
        }
    }

    public static void ConfirmOrder(FlightModel selectedFlight, double totalPrice)
    {
        var currentAccount = UserAccountLogic.CurrentAccount;

        Console.Clear();
        Console.WriteLine("\n=== Booking Summary ===");
        Console.WriteLine($"Flight: {selectedFlight.Airline}");
        Console.WriteLine($"Route: {selectedFlight.DepartureAirport} to {selectedFlight.ArrivalDestination}");
        Console.WriteLine($"Date: {selectedFlight.DepartureDate}, Time: {selectedFlight.FlightTime}");
        Console.WriteLine("\nPassenger Details:");
        
        for (int i = 0; i < BookFlightLogic.passengers.Count; i++)
        {
            var p = BookFlightLogic.passengers[i];
            Console.WriteLine($"{i + 1}. {p.Title} {p.FirstName} {p.LastName}");
            Console.WriteLine($"   Seat: {BookFlightLogic.chosenSeats[i]}");
            Console.WriteLine($"   Age Group: {p.AgeGroup}");
            if (p.DateOfBirth.HasValue)
            {
                Console.WriteLine($"   Date of Birth: {p.DateOfBirth.Value:dd-MM-yyyy}");
            }
        }

        // Calculate final price including fees
        double foodAndDrinkCost = BookFlightLogic.foodAndDrinkCosts.Sum();
        double baggageTotalFee = BookFlightLogic.baggageInfo.Sum(b => b.Fee);
        double petTotalFee = BookFlightLogic.petInfo.Sum(p => p.Fee);
        totalPrice += baggageTotalFee + petTotalFee;

        Console.WriteLine($"\nPrice Breakdown:");
        Console.WriteLine($"Ticket(s): {totalPrice - baggageTotalFee - petTotalFee - foodAndDrinkCost:C}");
        if (baggageTotalFee > 0) Console.WriteLine($"Baggage Fees: {baggageTotalFee:C}");
        if (petTotalFee > 0) Console.WriteLine($"Pet Fees: {petTotalFee:C}");
        if (foodAndDrinkCost > 0) Console.WriteLine($"Food and Drinks: {foodAndDrinkCost:C}");
        Console.WriteLine($"Total Price: {totalPrice:C}");

        int allFlightPoints = currentAccount.TotalFlightPoints;
        double discountToApply = 0;

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
                            discountToApply = FlightPointsLogic.CalculateFlightPoint(amountFlightPoints, totalPrice, allFlightPoints);

                            totalPrice -= discountToApply;


                            Console.WriteLine($"You used {discountToApply:C} worth of flight points.");
                            Console.WriteLine($"Updated Total Price: {totalPrice:C}");
                            break;
                        }
                        else if (amountFlightPointsStr.ToUpper() == "Q")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer.");
                        }
                    }

                    break;
                }
                else
                {
                    Console.WriteLine("You don't have enough flight points for a discount.");
                    break;
                }
            }
            else if (discountYesOrNo == "no")
            {
                MenuPresentation.PrintColored("You chose not to use flight points.", ConsoleColor.Yellow);
                break;
            }
            else
            {
                MenuPresentation.PrintColored("\nInvalid input. Please answer with 'yes' or 'no'.", ConsoleColor.Red);
            }
        }

        while (true)
        {
            Console.Write("\nConfirm booking? (yes/no): ");
            string finalConfirmation = Console.ReadLine().ToLower();

            if (finalConfirmation == "yes")
            {
                BookFlightLogic.SaveBooking(selectedFlight, discountToApply, totalPrice);

                MenuPresentation.PrintColored("\nBooking confirmed successfully!", ConsoleColor.Green);
                MenuPresentation.PrintColored("All passenger information has been saved.", ConsoleColor.Yellow);
                break;
            }
            else if (finalConfirmation == "no")
            {
                MenuPresentation.PrintColored("\nBooking cancelled.", ConsoleColor.Red);
                break;
            }
            else
            {
                MenuPresentation.PrintColored("\nInvalid input. Enter either 'yes or 'no'.'", ConsoleColor.Red);
            }
        }

    }
}
