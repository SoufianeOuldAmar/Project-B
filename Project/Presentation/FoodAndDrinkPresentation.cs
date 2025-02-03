using System;
using System.Collections.Generic;
using System.Threading;
using DataModels;

public class FoodAndDrinkPresentation
{
    public static (double, List<FoodAndDrinkItem>) AddFoodAndDrinksToBooking(FlightModel flightModel)
    {
        Console.Clear();
        Console.WriteLine("=== 🥪 Food And Drinks 🍵 ===\n");
        Console.WriteLine("Select food and drinks to add to your booking:");

        for (int i = 0; i < FoodAndDrinkLogic.FoodAndDrinksMenu.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{i + 1}. {FoodAndDrinkLogic.FoodAndDrinksMenu[i].Name} - €{FoodAndDrinkLogic.FoodAndDrinksMenu[i].Price:F2}");
            Console.ResetColor();
        }

        Console.WriteLine("0. If you want to continue");

        double totalCost = 0;

        while (true)
        {
            Console.Write("\nSelect an option (or press 0 to finish): ");
            string choice = Console.ReadLine();
            if (choice == "0")
            {
                break;
            }

            if (int.TryParse(choice, out int index) && index >= 1 && index <= FoodAndDrinkLogic.FoodAndDrinksMenu.Count)
            {
                var selectedItem = FoodAndDrinkLogic.FoodAndDrinksMenu[index - 1];
                FoodAndDrinkLogic.selectedItems.Add(selectedItem);
                totalCost += selectedItem.Price;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Added {selectedItem.Name} to your booking.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice. Please try again.");
                Console.ResetColor();
            }
        }


        if (FoodAndDrinkLogic.selectedItems.Count > 0)
        {
            totalCost = ConfirmFoodAndDrinksOrder(flightModel);
        }
        else
        {
            MenuPresentation.PrintColored("No food or drinks added to your booking.", ConsoleColor.Yellow);
        }

        FinancialReportLogic.GenerateFinancialReportForFoodAndDrinks();


        return (totalCost, FoodAndDrinkLogic.selectedItems);
    }

    public static double ConfirmFoodAndDrinksOrder(FlightModel flightModel)
    {
        Console.Clear();
        Console.WriteLine("=== Confirm Your Order ===\n");
        Console.WriteLine("You have selected the following items:");

        double totalCost = 0;
        foreach (var item in FoodAndDrinkLogic.selectedItems)
        {
            Console.WriteLine($"- {item.Name}: €{item.Price:F2}");
            totalCost += item.Price;
        }

        Console.WriteLine($"\nTotal cost: €{totalCost:F2}");

        while (true)
        {
            Console.Write("\nAre you sure you want to confirm this order? This cannot be canceled. (yes/no): ");
            string confirmation = Console.ReadLine()?.ToLower();
            if (confirmation == "yes")
            {
                flightModel.TicketPrice += totalCost;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nOrder confirmed! Your flight total is now €{flightModel.TicketPrice:F2}.");
                Console.ResetColor();
                break;
            }
            else if (confirmation == "no")
            {
                MenuPresentation.PrintColored("\nOrder canceled. No changes made to your booking.", ConsoleColor.Yellow);
                totalCost = 0; // Reset cost if order is canceled
                break;
            }
            else
            {
                MenuPresentation.PrintColored("\nInvalid input. Enter either 'yes or 'no'.", ConsoleColor.Red);
            }
        }

        return totalCost;
    }

    public static void AddFoodAndDrinksToExistingBooking(BookedFlightsModel bookedFlight)
    {
        Console.Clear();
        Console.WriteLine("=== 🍴 Add Food And Drinks To Your Booked Flight ===\n");

        Console.WriteLine($"Current Total Price: €{bookedFlight.TicketBill:F2}");
        Console.WriteLine("\nPreviously Added Food and Drinks:");

        bool IsValidFlightID = FoodAndDrinkLogic.CheckForFoodAndDrinks(bookedFlight).Item1;
        // List<BookedFlightModel> bookedFlight = FoodAndDrinkLogic.CheckForFoodAndDrinks(bookedFlight).Item2;

        if (IsValidFlightID)
        {
            foreach (var item in bookedFlight.FoodAndDrinkItems)
            {
                Console.WriteLine($"- {item.Name}: €{item.Price:F2}");
            }
        }
        else
        {
            Console.WriteLine("No food and drinks were added yet.");
        }

        Console.WriteLine("\nSelect additional food and drinks to add:");

        for (int i = 0; i < FoodAndDrinkLogic.FoodAndDrinksMenu.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{i + 1}. {FoodAndDrinkLogic.FoodAndDrinksMenu[i].Name} - €{FoodAndDrinkLogic.FoodAndDrinksMenu[i].Price:F2}");
            Console.ResetColor();
        }

        Console.WriteLine("0. Finish adding food and drinks");

        double additionalCost = 0;

        while (true)
        {
            Console.Write("\nSelect an option (or press 0 to finish): ");
            string choice = Console.ReadLine();

            if (choice == "0")
            {
                break;
            }

            if (FoodAndDrinkLogic.IsValidIndex(choice).Item1)
            {
                int index = FoodAndDrinkLogic.IsValidIndex(choice).Item2;

                // Call CalculateCost only once
                var costResult = FoodAndDrinkLogic.CalculateCost(index);
                additionalCost = costResult.Item1;
                string addedItemName = costResult.Item2;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Added {addedItemName} to your booking.");
                Console.ResetColor();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice. Please try again.");
                Console.ResetColor();
            }
        }

        if (FoodAndDrinkLogic.newItems.Count > 0)
        {
            if (bookedFlight.FoodAndDrinkItems == null)
            {
                bookedFlight.FoodAndDrinkItems = new List<FoodAndDrinkItem>();
            }

            FoodAndDrinkLogic.AddFoodAndDrinksToExistingBooking(bookedFlight, additionalCost);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nFood and drinks added successfully! Updated Total Price: €{bookedFlight.TicketBill:F2}");
            Console.ResetColor();

            // Save the updated flight details
        }
        else
        {
            MenuPresentation.PrintColored("\nNo additional food and drinks were added.", ConsoleColor.Yellow);
        }
    }
}