namespace DataAccess
{

    public static class AdminManageBookingLogic
    {
        // public static void LaodBookedPresentaion()
        // {
        //     Console.Clear();
        //     var flightDeatails = FlightsAccess.ReadAll();
        //     var BookdeFlight = BookedFlightsAccess.LoadAll();

        //     foreach (var emailBookingPair in BookdeFlight)
        //     {
        //         string email = emailBookingPair.Key;
        //         var bookings = emailBookingPair.Value;

        //         DisplayBookingDetails(bookings, flightDeatails, email);
        //     }
        // }
        // public static List<BookedFlightsModel> SearchBookedPresentaion(string email)
        // {
        //     var flightDeatails = FlightsAccess.ReadAll();
        //     var BookdeFlight = BookedFlightsAccess.LoadAll();
        //     List<BookedFlightsModel> bookings = new List<BookedFlightsModel>();

        //     if (BookdeFlight.ContainsKey(email))
        //     {
        //         bookings = BookdeFlight[email];
        //         DisplayBookingDetails(bookings, flightDeatails, email);
        //     }

        //     return bookings;
        // }


        public static bool SeatLogic(string newSeat)
        {
            if (newSeat.Length >= 2 && newSeat.Length <= 3 && "ABCDEF".Contains(char.ToUpper(newSeat[^1])) &&
            int.TryParse(newSeat.Substring(0, newSeat.Length - 1), out int number) &&
            number >= 1 && number <= 30)
            {
                return true;
            }
            return false;
        }
    }
}