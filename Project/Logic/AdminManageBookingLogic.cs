using DataModels;
using System.Text.Json;
namespace DataAccess
{

    public static class AdminManageBookingLogic
    {
        public static List<FlightModel> allFlights = FlightsAccess.ReadAll();
        public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();

        // public static void SaveChangesLogic(string email, Dictionary<string, List<BookedFlightsModel>> flights)
        // {
        //     Dictionary<string, List<BookedFlightsModel>> bookedFlights = LoadAll();
        // string filePath = "bookedflights.json";
        // try
        // {

        //     var options = new JsonSerializerOptions
        //     {
        //         WriteIndented = true
        //     };
        //     string jsonString = JsonSerializer.Serialize(flights, options);


        //     File.WriteAllText(filePath, jsonString);

        //     Console.WriteLine("Data successfully saved to the JSON file.");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"Error saving data: {ex.Message}");
        // }
        // }





        public static bool SeatLogic(string newSeat)
        {
            if (!(newSeat.Length >= 2 && newSeat.Length <= 3 && "ABCDEF".Contains(char.ToUpper(newSeat[^1])) &&
            int.TryParse(newSeat.Substring(0, newSeat.Length - 1), out int number) &&
            number >= 1 && number <= 30))
            {
                return false;
            }
            foreach (var seat in allBookedFlights)
            {
                foreach (var book in seat.Value)
                {
                    if (book.BookedSeats.Any(seat => string.Equals(seat, newSeat, StringComparison.OrdinalIgnoreCase)))
                    {

                        return false;

                    }
                }

            }
            return true;


        }


    }
}