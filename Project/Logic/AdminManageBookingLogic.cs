using DataModels;
using System.Collections.Generic;
using System.Text.Json;
namespace DataAccess
{

    public static class AdminManageBookingLogic
    {
        public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
        public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();

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

        public static void SaveBookingData(string email, List<BookedFlightsModel> booking)
        {
            BookedFlightsAccess.Save(email, booking);
        }
    }
}