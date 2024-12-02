namespace DataAccess
{

    public static class AdminManageBookingLogic
    {
        public static List<BookedFlightsModel> AdminSearchBooking(string email)
        {
            var flightDeatails = FlightsAccess.ReadAll();
            var BookdeFlight = BookedFlightsAccess.LoadAll();
            if (BookdeFlight.ContainsKey(email))
            {
                foreach (var books in BookdeFlight[email])
                {
                    Console.WriteLine($"  FlightID: {books.FlightID}");
                    Console.WriteLine($"  Booked Seats: {string.Join(", ", books.BookedSeats)}");
                    Console.WriteLine($"  Pets: {string.Join(", ", books.Pets)}");
                    Console.WriteLine($"  Baggage Info: {string.Join(", ", books.BaggageInfo)}");
                    Console.WriteLine($"  Is Cancelled: {books.IsCancelled}");
                    var flight = flightDeatails.Find(f => f.Id == books.FlightID);
                    Console.WriteLine($"  Date: {flight.DepartureDate}");
                    Console.WriteLine($"  Time: {flight.FlightTime}");
                }

            }
            return null;
        }
    }
}