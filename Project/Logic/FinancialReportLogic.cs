using System;
using System.Collections.Generic;
using System.Linq;

public static class FinancialReportLogic
{

    public static List<Payment> GetPaymentsByYear(int year)
    {
        // var Payments = FinancialReportAccess.LoadPayments();
        var Payments = DataAccessClass.ReadList<Payment>("DataSources/financialreports.json");

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
}
