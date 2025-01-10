using System;
using System.Collections.Generic;
using System.Linq;

public static class FinancialReportLogic
{

    public static List<Payement> GetPaymentsByYear(int year)
    {
        // var payements = FinancialReportAccess.LoadPayements();
        var payements = DataAccessClass.ReadList<Payement>("DataSources/FinancialReport.json");

        var currentDate = DateTime.Now;

        return payements
            .Where(x => x.DatePayement.Year == year && x.DatePayement <= currentDate)
            .ToList();
    }

    public static IEnumerable<dynamic> GroupPaymentsByItemType(List<Payement> payments)
    {
        return payments.GroupBy(x => x.ItemType).Select(group => new
        {
            ItemType = group.Key,
            TotalAmount = group.Sum(p => p.Amount),
            Count = group.Count()
        });
    }
}
