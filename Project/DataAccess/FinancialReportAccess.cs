using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class FinancialReportAccess
{
    private static readonly string _path = "DataSources/FinancialReport.json";

    public static List<Payement> LoadPayements()
    {
        if (!File.Exists(_path))
        {
            return new List<Payement>();
        }

        var jsonData = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<Payement>>(jsonData) ?? new List<Payement>();
    }

    public static void SavePayements(List<Payement> payements)
    {
        var existingPayments = LoadPayements();
        existingPayments.AddRange(payements);

        var jsonData = JsonSerializer.Serialize(existingPayments, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_path, jsonData);
    }
}
