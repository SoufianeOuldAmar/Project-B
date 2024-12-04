using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DataModels;

namespace DataAccess
{
    public static class PassengerAccess
    {
        private static readonly string fileName = "DataSources/passengers.json";

        public static void SavePassengers(List<PassengerModel> passengers)
        {
            string json = JsonSerializer.Serialize(passengers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
        }

        public static List<PassengerModel> LoadPassengers()
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<List<PassengerModel>>(json) ?? new List<PassengerModel>();
            }
            return new List<PassengerModel>();
        }
    }
}
