using DataAccess;
using PresentationLayer;
using DataModels;

public class LayoutModel
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<string> SeatArrangement { get; set; }
    public List<string> AvailableSeats { get; set; }
    public List<string> BookedSeats { get; set; }
    public List<string> ChosenSeats { get; set; }
    public bool IsAirbusA330 { get; set; }
    public bool IsBoeing787 { get; set; }
    public Dictionary<string, string> SeatInitials { get; set; } = new Dictionary<string, string>();

    public LayoutModel(int rows, int columns, List<string> seatArrangement, bool isAirbusA330 = false, bool isBoeing787 = false)
    {
        Rows = rows;
        Columns = columns;
        SeatArrangement = seatArrangement;
        AvailableSeats = new List<string>(seatArrangement);
        BookedSeats = new List<string>();
        ChosenSeats = new List<string>();
        IsAirbusA330 = isAirbusA330;
        IsBoeing787 = isBoeing787;
        SeatInitials = new Dictionary<string, string>();
    }

    public void BookFlight(string seat, string initials)
    {
        if (AvailableSeats.Contains(seat))
        {
            AvailableSeats.Remove(seat);
            ChosenSeats.Add(seat);
            SeatInitials[seat] = initials;
            LayoutPresentation.PrintBookingSuccess(seat, initials);
        }
        else if (BookedSeats.Contains(seat))
        {
            string bookedBy = SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : "someone";
            LayoutPresentation.PrintSeatAlreadyBooked(seat, bookedBy);
        }
        else
        {
            LayoutPresentation.PrintSeatNotAvailable(seat);
        }
    }

    public void ConfirmBooking()
    {
        foreach (var seat in ChosenSeats)
        {
            BookedSeats.Add(seat);
            if (!SeatInitials.ContainsKey(seat))
            {
                SeatInitials[seat] = seat;
            }
        }
        ChosenSeats.Clear();
        LayoutPresentation.PrintBookingConfirmed();
    }

    public bool TryBookSeat(FlightModel flight, string seat)
    {   
        if (!BookedSeats.Contains(seat))
        {
            BookedSeats.Add(seat);
            return true;
        }
        return false;
    }

    public void ResetAllSeats()
    {
        var allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");

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

        var allAccounts = DataAccessClass.ReadList<AccountModel>("DataSources/accounts.json");
        foreach (var account in allAccounts)
        {
            account.FlightPointsDataList.Clear();
        }

        DataAccessClass.WriteList<FlightModel>("DataSources/flights.json", allFlights);

        DataAccessClass.WriteList<AccountModel>("DataSources/accounts.json", allAccounts);
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