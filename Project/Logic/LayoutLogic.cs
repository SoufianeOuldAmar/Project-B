using PresentationLayer;
using DataModels;

public static class LayoutLogic
{
    public static void BookFlight(LayoutModel layout, string seat, string initials)
    {
        if (layout.AvailableSeats.Contains(seat))
        {
            layout.AvailableSeats.Remove(seat);
            layout.ChosenSeats.Add(seat);
            layout.SeatInitials[seat] = initials;
            // LayoutPresentation.PrintBookingSuccess(seat, initials);
        }
        else if (layout.BookedSeats.Contains(seat))
        {
            string bookedBy = layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : "someone";
            LayoutPresentation.PrintSeatAlreadyBooked(seat, bookedBy);
        }
        else
        {
            LayoutPresentation.PrintSeatNotAvailable(seat);
        }
    }

    public static bool IsCurrentUsersSeat(string seat, List<BookedFlightsModel> currentUserBookings, int flightId)
    {
        var relevantBooking = currentUserBookings.FirstOrDefault(b => b.FlightID == flightId);
        if (relevantBooking != null && relevantBooking.SeatInitials != null)
        {
            return relevantBooking.SeatInitials.ContainsKey(seat);
        }
        return false;
    }

    public static string GetDisplayText(string seat, List<BookedFlightsModel> currentUserBookings, int flightId)
    {
        var booking = currentUserBookings.FirstOrDefault(b => b.FlightID == flightId);
        if (booking != null && booking.SeatInitials.ContainsKey(seat))
        {
            return booking.SeatInitials[seat];
        }
        return seat;
    }

    public static void ConfirmBooking(LayoutModel layout)
    {
        foreach (var seat in layout.ChosenSeats)
        {
            layout.BookedSeats.Add(seat);
            if (!layout.SeatInitials.ContainsKey(seat))
            {
                layout.SeatInitials[seat] = seat;
            }
        }
        layout.ChosenSeats.Clear();

    }

    public static void ArrangeNewSeat(LayoutModel layout, string seat, FlightModel flight)
    {
        layout.BookedSeats.Add(seat);
        if (!layout.SeatInitials.ContainsKey(seat))
        {
            layout.SeatInitials[seat] = seat;
        }
        layout.AvailableSeats.Remove(seat);

        var allNewFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
        var allBookedFlights = BookedFlightsAccess.LoadAll();

        for (int i = 0; i < allNewFlights.Count; i++)
        {
            if (allNewFlights[i].Id == flight.Id)
            {
                allNewFlights[i] = flight;
            }
        }

        if (allBookedFlights.TryGetValue(MenuPresentation.currentAccount.EmailAddress, out var bookedFlights))
        {
            foreach (var bookedFlight in bookedFlights)
            {
                if (bookedFlight.FlightID == flight.Id)
                {
                    string oldSeat = bookedFlight.BookedSeats.FirstOrDefault();
                    bookedFlight.BookedSeats.Clear();
                    bookedFlight.BookedSeats.Add(seat);

                    if (oldSeat != null && bookedFlight.SeatInitials.ContainsKey(oldSeat))
                    {
                        string seatInitials = bookedFlight.SeatInitials[oldSeat];
                        bookedFlight.SeatInitials.Remove(oldSeat);
                        bookedFlight.SeatInitials[seat] = seatInitials;
                    }
                }
            }
        }

        DataAccessClass.WriteList<FlightModel>("DataSources/flights.json", allNewFlights);
        BookedFlightsAccess.WriteAll(MenuPresentation.currentAccount.EmailAddress, allBookedFlights[MenuPresentation.currentAccount.EmailAddress]);

    }

    public static bool TryBookSeat(LayoutModel layout, string seat)
    {
        if (layout.SeatArrangement.Contains(seat) && !layout.BookedSeats.Contains(seat) && layout.AvailableSeats.Contains(seat))
        {
            return true;
        }
        return false;
    }

    public static void ResetAllSeats()
    {
        var allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
        var allBookedFlights = BookedFlightsAccess.LoadAll();
        string filePath = "DataSources/bookedflights.json";

        foreach (var flight in allFlights)
        {
            // Move all booked seats back to available seats
            foreach (var seat in flight.Layout.BookedSeats.ToList())
            {
                flight.Layout.BookedSeats.Remove(seat);
                flight.Layout.AvailableSeats.Add(seat);
            }

            // Clear seat initials
            flight.Layout.SeatInitials.Clear();

            flight.Layout.AvailableSeats = flight.Layout.AvailableSeats
                .OrderBy(seat => int.Parse(seat.Substring(0, seat.Length - 1))) // Extract number part
                .ThenBy(seat => seat.Last()) // Extract letter part
                .ToList();
        }


        DataAccessClass.DeleteBookedFlights(System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/bookedflights.json")));
        DataAccessClass.DeleteBookedFlights(System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/passengers.json")));
        DataAccessClass.DeleteBookedFlights(System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/financialreports.json.json")));

        var allAccounts = DataAccessClass.ReadList<UserAccountModel>("DataSources/useraccounts.json");
        foreach (var account in allAccounts)
        {
            account.TotalFlightPoints = 0;
            account.Fee = 0;
            account.Notifications.Clear();
        }

        DataAccessClass.WriteList<FlightModel>("DataSources/flights.json", allFlights);
        DataAccessClass.WriteList<UserAccountModel>("DataSources/useraccounts.json", allAccounts);
    }

    public static LayoutModel CreateBoeing737Layout()
    {
        int rows = 30;
        int columns = 6;
        List<string> seatArrangement = new List<string>();

        for (int i = 1; i <= rows; i++)
        {
            string rowNumber = i < 10 ? $"0{i}" : $"{i}";
            seatArrangement.Add($"{rowNumber}A");
            seatArrangement.Add($"{rowNumber}B");
            seatArrangement.Add($"{rowNumber}C");
            seatArrangement.Add($"{rowNumber}D");
            seatArrangement.Add($"{rowNumber}E");
            seatArrangement.Add($"{rowNumber}F");
        }

        return new LayoutModel(rows, columns, seatArrangement);
    }

    public static LayoutModel CreateAirbusA330200Layout()
    {
        int rows = 50;
        int columns = 10;
        List<string> seatArrangement = new List<string>();

        for (int i = 1; i <= 10; i++)
        {
            string rowNumber = i < 10 ? $"0{i}" : $"{i}";
            seatArrangement.Add($"{rowNumber}A");
            seatArrangement.Add($"{rowNumber}B");
            seatArrangement.Add($"{rowNumber}D");
            seatArrangement.Add($"{rowNumber}E");
            seatArrangement.Add($"{rowNumber}F");
            seatArrangement.Add($"{rowNumber}G");
            seatArrangement.Add($"{rowNumber}J");
            seatArrangement.Add($"{rowNumber}K");
        }

        for (int i = 11; i <= 50; i++)
        {
            string rowNumber = $"{i}";
            seatArrangement.Add($"{rowNumber}A");
            seatArrangement.Add($"{rowNumber}B");
            seatArrangement.Add($"{rowNumber}C");
            seatArrangement.Add($"{rowNumber}D");
            seatArrangement.Add($"{rowNumber}E");
            seatArrangement.Add($"{rowNumber}F");
            seatArrangement.Add($"{rowNumber}G");
            seatArrangement.Add($"{rowNumber}H");
            seatArrangement.Add($"{rowNumber}J");
            seatArrangement.Add($"{rowNumber}K");
        }

        return new LayoutModel(rows, columns, seatArrangement, true);
    }

    public static LayoutModel CreateBoeing787Layout()
    {
        int rows = 42;
        int columns = 9;
        List<string> seatArrangement = new List<string>();

        for (int i = 1; i <= 6; i++)
        {
            string rowNumber = i < 10 ? $"0{i}" : $"{i}";
            seatArrangement.Add($"{rowNumber}A");
            seatArrangement.Add($"{rowNumber}C");
            seatArrangement.Add($"{rowNumber}D");
            seatArrangement.Add($"{rowNumber}G");
            seatArrangement.Add($"{rowNumber}H");
            seatArrangement.Add($"{rowNumber}K");
        }

        for (int i = 7; i <= 42; i++)
        {
            if (i != 15 && i != 27)
            {
                string rowNumber = i < 10 ? $"0{i}" : $"{i}";
                seatArrangement.Add($"{rowNumber}A");
                seatArrangement.Add($"{rowNumber}B");
                seatArrangement.Add($"{rowNumber}C");
                seatArrangement.Add($"{rowNumber}D");
                seatArrangement.Add($"{rowNumber}E");
                seatArrangement.Add($"{rowNumber}F");
                seatArrangement.Add($"{rowNumber}G");
                seatArrangement.Add($"{rowNumber}H");
                seatArrangement.Add($"{rowNumber}K");
            }
        }

        return new LayoutModel(rows, columns, seatArrangement, false, true);
    }
}