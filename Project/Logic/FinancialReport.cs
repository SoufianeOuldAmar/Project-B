using System;
using System.Collections.Generic;
using System.Text.Json;

public class FinancialReport
{
    public string Path = "FinancialReport.json";


    public List<Payement> LoadPayements()
    {
        if (!File.Exists(Path)) 
        {
            return new List<Payement>(); 
        }

        // If the file exists, read the data and deserialize it into a list of Payement objects.
        var jsonData = File.ReadAllText(Path);
        return JsonSerializer.Deserialize<List<Payement>>(jsonData) ?? new List<Payement>();
    }


    public void SavePayements(List<Payement> payements)
    {
        var existingPayments = LoadPayements();
        existingPayments.AddRange(payements);
        var jsonData = JsonSerializer.Serialize(existingPayments, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine($"Serialized data: {jsonData}");
        File.WriteAllText(Path, jsonData);
        Console.WriteLine("File written successfully.");
    }


    public void GenerateDataForReport(int year )
    {
        var payements = LoadPayements();
        var currentDate = DateTime.Now;
        var FilterByYear = payements.Where(x=> x.DatePayement.Year == year && x.DatePayement <= currentDate);

        if (!FilterByYear.Any())
        {
            Console.WriteLine($"No purchases made for the year {year}.");
            return;
        }

        Console.WriteLine($"Financial Report for {year} (up to {currentDate:dd-MM-yyyy}):");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("| Item Type        | Total Amount   | Amount of Transactions |");
        Console.WriteLine("---------------------------------------------------------------");


    // grouped by payement objects thatb have the same item type 
        var groupPerItem = FilterByYear.GroupBy(x=> x.ItemType).Select(group => new 
        {
            ItemType = group.Key,
            TotalAmount = group.Sum(p => p.Amount),
            Count = group.Count()
        }).ToList();

        foreach(var item in groupPerItem)
        {
            Console.WriteLine($"{item.ItemType}, Total Amount: {item.TotalAmount:C}, Amount of Transactions: {item.Count} ");
        }

        Console.WriteLine("---------------------------------------------------------------");


    






    }











}