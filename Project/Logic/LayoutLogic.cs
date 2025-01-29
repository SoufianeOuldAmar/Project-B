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
            LayoutPresentation.PrintBookingSuccess(seat, initials);
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
        LayoutPresentation.PrintBookingConfirmed();
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
        var allBookedFlights = BookFlightLogic.GetAllBookedFlights();
        string filePath = "DataSources/bookedflights.json";

        foreach (var flight in allFlights)
        {
            foreach (var seat in flight.Layout.BookedSeats.ToList())
            {
                flight.Layout.BookedSeats.Remove(seat);
                flight.Layout.AvailableSeats.Add(seat);
            }
            flight.Layout.SeatInitials.Clear();
        }

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error resetting seats: {ex.Message}");
        }

        var allAccounts = DataAccessClass.ReadList<UserAccountModel>("DataSources/useraccounts.json");
        foreach (var account in allAccounts)
        {
            account.TotalFlightPoints = 0;
            account.Fee = 0;

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