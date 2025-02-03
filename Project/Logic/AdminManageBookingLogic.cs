using DataModels;
using System.Collections.Generic;
using System.Text.Json;
namespace DataAccess
{

    public static class AdminManageBookingLogic
    {
        public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
        public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();

        public static (bool, string) NewSeatLogic(string newSeat, BookedFlightsModel selectedBooking)
        {
            if (!(newSeat.Length >= 2 && newSeat.Length <= 3 && "ABCDEF".Contains(char.ToUpper(newSeat[^1])) &&
            int.TryParse(newSeat.Substring(0, newSeat.Length - 1), out int number) &&
            number >= 1 && number <= 30))
            {
                return (false, "");
            }
            foreach (var seat in allBookedFlights)
            {
                foreach (var book in seat.Value)
                {
                    if (book.BookedSeats.Any(seat => string.Equals(seat, newSeat, StringComparison.OrdinalIgnoreCase)))
                    {

                        return (false, "");

                    }
                }
            }

            string letterPart = newSeat.Substring(1).ToUpper();
            string numberPart = newSeat.Substring(0, 1);
            string seatStr = $"{numberPart}{letterPart}";
            selectedBooking.BookedSeats.Add(seatStr);
            NotificationLogic.newSeats.Add(seatStr);

            return (true, seatStr);
        }

        public static bool ChangeSeatLogic(string newSeat, BookedFlightsModel selectedBooking, int seatIndex)
        {
            if (NewSeatLogic(newSeat, selectedBooking).Item1)
            {
                string letterPart = newSeat.Substring(1).ToUpper();
                string numberPart = newSeat.Substring(0, 1);
                newSeat = $"{numberPart}{letterPart}";
                var oldSeat = selectedBooking.BookedSeats[seatIndex - 1];
                selectedBooking.BookedSeats[seatIndex - 1] = newSeat;

                NotificationLogic.seatChanges.Add(oldSeat);
                NotificationLogic.seatChanges.Add(newSeat);

                return true;
            }

            return false;

        }

        public static (bool, int) CheckValidSelection(string number, BookedFlightsModel selectedBooking)
        {
            return (int.TryParse(number, out int seatIndex) && seatIndex > 0 && seatIndex <= selectedBooking.BookedSeats.Count, seatIndex);
        }

        public static void SaveBookingData(string email, List<BookedFlightsModel> booking)
        {
            BookedFlightsAccess.Save(email, booking);
        }
    }
}