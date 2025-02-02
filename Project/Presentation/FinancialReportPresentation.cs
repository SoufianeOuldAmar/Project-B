using System;
using System.Collections.Generic;

public static class FinancialReportPresentation
{

    public static void GenerateDataForReport(int year)
    {
        var payments = FinancialReportLogic.GetPaymentsByYear(year);

        if (payments.Count == 0)
        {
            Console.WriteLine($"No ❌ purchases made for the year {year}.");
            return;
        }

        Console.WriteLine($"Financial Report 🏦 for {year} (up to {DateTime.Now:dd-MM-yyyy}):");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("| Item Type        | Total Amount   | Amount of Transactions |");
        Console.WriteLine("---------------------------------------------------------------");

        var groupedPayments = FinancialReportLogic.GroupPaymentsByItemType(payments);

        foreach (var item in groupedPayments)
        {
            Console.WriteLine($"{item.ItemType,-16} {item.TotalAmount,15:C} {item.Count,25}");
        }

        Console.WriteLine("---------------------------------------------------------------");
    }
}
