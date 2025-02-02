using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;

public static class FinancialReportLogic
{
    public static List<Payment> Payments = DataAccessClass.ReadList<Payment>("DataSources/financialreports.json");

    public static List<Payment> GetPaymentsByYear(int year)
    {
        // var Payments = FinancialReportAccess.LoadPayments();
        var currentDate = DateTime.Now;

        return Payments
            .Where(x => x.DatePayment.Year == year && x.DatePayment <= currentDate)
            .ToList();
    }

    public static IEnumerable<dynamic> GroupPaymentsByItemType(List<Payment> payments)
    {
        return payments.GroupBy(x => x.ItemType).Select(group => new
        {
            ItemType = group.Key,
            TotalAmount = group.Sum(p => p.Amount),
            Count = group.Count()
        });
    }

    public static void GenerateFinancialReportForFoodAndDrinks()
    {
        List<Payment> allPayments = new List<Payment>();
        var paymentsID = FinancialReportLogic.GetPaymentID();

        foreach (var item in FoodAndDrinkLogic.selectedItems)
        {
            Payment foodAndDrinkPayment = new Payment(paymentsID + allPayments.Count + 1, "FoodAndDrink", item.Price, DateTime.Now);
            allPayments.Add(foodAndDrinkPayment);
        }

        // FinancialReportAccess.SavePayments(allPayments);
        DataAccessClass.WriteList<Payment>("DataSources/financialreports.json", allPayments);
    }

    public static int GetPaymentID()
    {
        return DataAccessClass.ReadList<Payment>("DataSources/financialreports.json").Count + 1;
    }

    public static void SavePayments(FlightModel selectedFlight, string seat, string initials)
    {
        int paymentID = FinancialReportLogic.GetPaymentID();

        // Add the ticket payment
        Payment ticketPayment = new Payment(paymentID, "Ticket", selectedFlight.TicketPrice, DateTime.Now);
        BookFlightLogic.allPayments.Add(ticketPayment);

        // Add the baggage payments
        foreach (var baggage in BookFlightLogic.baggageInfo)
        {
            Payment baggagePayment = new Payment(paymentID + BookFlightLogic.allPayments.Count + 1, "Baggage", baggage.Fee, DateTime.Now);
            BookFlightLogic.allPayments.Add(baggagePayment);
            LayoutLogic.BookFlight(selectedFlight.Layout, seat, initials);
        }

        // Add the pet payments
        foreach (var pet in BookFlightLogic.petInfo)
        {
            Payment petPayment = new Payment(paymentID + BookFlightLogic.allPayments.Count + 1, "Pet", pet.Fee, DateTime.Now);
            BookFlightLogic.allPayments.Add(petPayment);
        }

        DataAccessClass.SavePayments(BookFlightLogic.allPayments);
    }
}
