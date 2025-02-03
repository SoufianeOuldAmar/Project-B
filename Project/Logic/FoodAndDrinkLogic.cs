using System.Collections.Generic;

public static class FoodAndDrinkLogic
{
    public static List<FoodAndDrinkItem> FoodAndDrinksMenu = new List<FoodAndDrinkItem>
    {
        new FoodAndDrinkItem("Rice With Chicken",           14.50),
        new FoodAndDrinkItem("Sandwhich Chicken Teryaki",    9.75),
        new FoodAndDrinkItem("Sandwich Healthy",             7.50),
        new FoodAndDrinkItem("Pringles Chips",               3.85),
        new FoodAndDrinkItem("Tuc Naturel",                  3.00),
        new FoodAndDrinkItem("Chocolate Bar",                2.85),
        new FoodAndDrinkItem("Fanta Orange",                 3.45),
        new FoodAndDrinkItem("Pepsi Cola",                   3.45),
        new FoodAndDrinkItem("Ice Tea Sparkling",            3.25),
        new FoodAndDrinkItem("Fristi",                       3.25),
        new FoodAndDrinkItem("Coffee",                       3.10),
        new FoodAndDrinkItem("Tea",                          2.85),
        new FoodAndDrinkItem("Water Bottle",                 2.10)
    };
    public static List<FoodAndDrinkItem> selectedItems = new List<FoodAndDrinkItem>();

    public static List<FoodAndDrinkItem> newItems = new List<FoodAndDrinkItem>();

    public static (double, string) CalculateCost(int index)
    {
        double additionalCost = 0;
        var selectedItem = FoodAndDrinksMenu[index - 1];
        newItems.Add(selectedItem);
        additionalCost += selectedItem.Price;
        return (additionalCost, selectedItem.Name);
    }

    public static (bool, BookedFlightsModel) CheckForFoodAndDrinks(BookedFlightsModel bookedFlight)
    {
        return (bookedFlight.FoodAndDrinkItems != null && bookedFlight.FoodAndDrinkItems.Count > 0, bookedFlight);
    }

    public static void AddFoodAndDrinksToExistingBooking(BookedFlightsModel bookedFlight, double additionalCost)
    {
        if (bookedFlight.FoodAndDrinkItems == null)
        {
            bookedFlight.FoodAndDrinkItems = new List<FoodAndDrinkItem>();
        }

        bookedFlight.FoodAndDrinkItems.AddRange(newItems);
        bookedFlight.TicketBill += additionalCost;

        BookedFlightsAccess.Save(bookedFlight.Email, bookedFlight);

    }

    public static (bool, int) IsValidIndex(string choice)
    {
        return (int.TryParse(choice, out int index) && index >= 1 && index <= FoodAndDrinksMenu.Count, index);
    }

}