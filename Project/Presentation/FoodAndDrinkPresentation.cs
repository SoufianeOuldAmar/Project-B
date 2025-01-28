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

        List<FoodAndDrinkItem> selectedItems = new List<FoodAndDrinkItem>();
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
                selectedItems.Add(selectedItem);
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


        if (selectedItems.Count > 0)
        {
            totalCost = ConfirmFoodAndDrinksOrder(selectedItems, flightModel);
        }
        else
        {
            Console.WriteLine("No food or drinks added to your booking.");
        }

        List<Payment> allPayments = new List<Payment>();
        var paymentsList = DataManagerLogic.GetAll<Payment>("DataSources/financialreports.json");

        foreach (var item in selectedItems)
        {
            Payment foodAndDrinkPayment = new Payment(paymentsList.Count + allPayments.Count + 1, "FoodAndDrink", item.Price, DateTime.Now);
            allPayments.Add(foodAndDrinkPayment);
        }

        // FinancialReportAccess.SavePayments(allPayments);
        DataManagerLogic.Save<Payment>("DataSources/financialreports.json", allPayments);


        return (totalCost, selectedItems);
    }

    public static double ConfirmFoodAndDrinksOrder(List<FoodAndDrinkItem> selectedItems, FlightModel flightModel)
    {
        Console.Clear();
        Console.WriteLine("=== Confirm Your Order ===\n");
        Console.WriteLine("You have selected the following items:");

        double totalCost = 0;
        foreach (var item in selectedItems)
        {
            Console.WriteLine($"- {item.Name}: €{item.Price:F2}");
            totalCost += item.Price;
        }

        Console.WriteLine($"\nTotal cost: €{totalCost:F2}");
        Console.Write("\nAre you sure you want to confirm this order? This cannot be canceled. (yes/no): ");

        string confirmation = Console.ReadLine()?.ToLower();
        if (confirmation == "yes")
        {
            flightModel.TicketPrice += totalCost;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nOrder confirmed! Your flight total is now €{flightModel.TicketPrice:F2}.");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("\nOrder canceled. No changes made to your booking.");
            totalCost = 0; // Reset cost if order is canceled
        }

        return totalCost;
    }

    public static void AddFoodAndDrinksToExistingBooking(BookedFlightsModel bookedFlight)
    {
        Console.Clear();
        Console.WriteLine("=== 🍴 Add Food And Drinks To Your Booked Flight ===\n");

        Console.WriteLine($"Current Total Price: €{bookedFlight.TicketBill:F2}");
        Console.WriteLine("\nPreviously Added Food and Drinks:");

        if (bookedFlight.FoodAndDrinkItems != null && bookedFlight.FoodAndDrinkItems.Count > 0)
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

        List<FoodAndDrinkItem> newItems = new List<FoodAndDrinkItem>();
        double additionalCost = 0;

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
                newItems.Add(selectedItem);
                additionalCost += selectedItem.Price;
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

        if (newItems.Count > 0)
        {
            if (bookedFlight.FoodAndDrinkItems == null)
            {
                bookedFlight.FoodAndDrinkItems = new List<FoodAndDrinkItem>();
            }

            bookedFlight.FoodAndDrinkItems.AddRange(newItems);
            bookedFlight.TicketBill += additionalCost;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nFood and drinks added successfully! Updated Total Price: €{bookedFlight.TicketBill:F2}");
            Console.ResetColor();

            // Save the updated flight details
            DataManagerLogic.Save(bookedFlight.Email, bookedFlight);
        }
        else
        {
            Console.WriteLine("\nNo additional food and drinks were added.");
        }
    }
}