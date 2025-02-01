using System.Collections.Generic;
using DataModels;

public static class BookFlightPresentation2
{
    public static void BookFlightMenu()
    {

    }

    public static void ConfirmOrder(FlightModel selectedFlight, List<PassengerModel> passengers, List<string> chosenSeats, List<double> foodAndDrinkCosts, List<FoodAndDrinkItem> selectedItems, List<BaggageModel> baggageInfo, List<PetModel> petInfo, double totalPrice)
    {
        var currentAccount = UserAccountLogic.CurrentAccount;
        List<FlightModel> allFlights = DataManagerLogic.GetAll<FlightModel>("DataSources/flights.json");

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
                Console.WriteLine("You chose not to use flight points.");
                break;
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
            currentAccount.TotalFlightPoints -= (int)discountToApply;
            LayoutLogic.ConfirmBooking(selectedFlight.Layout);

            var existingPassengers = DataManagerLogic.GetAll<PassengerModel>("DataSources/passengers.json");

            existingPassengers.AddRange(passengers);
            DataManagerLogic.Save<PassengerModel>("DataSources/passengers.json", existingPassengers);
            DataManagerLogic.UpdateCurrentAccount(currentAccount);

            var bookedFlight1 = new BookedFlightsModel(selectedFlight.Id, selectedFlight.Layout.BookedSeats, baggageInfo, petInfo, false);
            bookedFlight1.DateTicketsBought = DateTime.Now.ToString("dd-MM-yyyy");


            bookedFlight1.TicketBill += totalPrice;
            bookedFlight1.FlightPoints = new FlightPoint(bookedFlight1.DateTicketsBought, 0, selectedFlight.Id);

            foreach (var seat in chosenSeats)
            {
                if (selectedFlight.Layout.SeatInitials.ContainsKey(seat))
                {
                    bookedFlight1.SeatInitials[seat] = selectedFlight.Layout.SeatInitials[seat];
                }
            }

            bookedFlight1.FoodAndDrinkItems = selectedItems;
            bookedFlight1.Email = currentAccount.EmailAddress;

            List<BookedFlightsModel> bookedFlightModel = new List<BookedFlightsModel>
                    {
                        bookedFlight1
                    };

            var existingBookings = DataManagerLogic.LoadByEmail(currentAccount.EmailAddress);

            existingBookings.RemoveAll(b => b.FlightID == selectedFlight.Id);

            existingBookings.Add(bookedFlight1);

            DataManagerLogic.Save<UserAccountModel>("DataSources/useraccounts.json", UserAccountLogic._accounts);
            DataAccessClass.UpdateCurrentAccount(currentAccount);

            foreach (var bookedFlight in bookedFlightModel)
            {
                BookFlightLogic.RemoveDuplicateSeats(bookedFlight);
            }

            DataManagerLogic.Save(currentAccount.EmailAddress, bookedFlight1);

            selectedFlight.Layout.BookedSeats = selectedFlight.Layout.BookedSeats.Distinct().ToList();

            for (int i = 0; i < allFlights.Count; i++)
            {
                if (allFlights[i].Id == selectedFlight.Id)
                {
                    allFlights[i] = selectedFlight;
                    break;
                }
            }


            DataManagerLogic.Save<FlightModel>("DataSources/flights.json", allFlights);

            Console.WriteLine("\nBooking confirmed successfully!");
            Console.WriteLine("All passenger information has been saved.");
        }
        else
        {
            Console.WriteLine("\nBooking cancelled.");
        }
    }
}