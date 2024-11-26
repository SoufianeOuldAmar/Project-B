using DataAccess;
public class LayoutModel
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<string> SeatArrangement { get; set; }
    public List<string> AvailableSeats { get; set; }
    public List<string> BookedSeats { get; set; }
    public List<string> ChosenSeats { get; set; }
    public bool IsAirbusA330 { get; set; }
    public Dictionary<string, string> SeatInitials { get; set; } = new Dictionary<string, string>();

    public LayoutModel(int rows, int columns, List<string> seatArrangement, bool isAirbusA330 = false)
    {
        Rows = rows;
        Columns = columns;
        SeatArrangement = seatArrangement;
        AvailableSeats = new List<string>(seatArrangement);
        BookedSeats = new List<string>();
        ChosenSeats = new List<string>();
        IsAirbusA330 = isAirbusA330;
        SeatInitials = new Dictionary<string, string>();
    }

    public void PrintLayout()
    {
        if (IsAirbusA330)
        {
            PrintAirbusA330Layout();
        }
        else
        {
            PrintStandardLayout();
        }
    }

    private void PrintStandardLayout()
    {
        for (int i = 0; i < SeatArrangement.Count; i += Columns)
        {
            int currentRow = (i / Columns) + 1;
            string seatTypeHeaderBusiness = currentRow == 1 ? "Business Class ↓" : "";
            string seatTypeHeaderEconomy = currentRow == 10 ? "Economy Class ↓" : "";

            if (currentRow == 10)
            {
                Console.WriteLine();
                Console.WriteLine("                                     Exit row");
                Console.WriteLine();
            }

            for (int j = 0; j < Columns; j++)
            {
                string seat = SeatArrangement[i + j];
                if (BookedSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}  ");
                }
                else if (ChosenSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}  ");
                }
                else
                {
                    Console.Write($"{seat}  ");
                }
                Console.ResetColor();

                if (j == 2)
                {
                    Console.Write("       ");
                }
            }
            Console.WriteLine();
        }
    }

    private void PrintAirbusA330Layout()
    {
        int index = 0;
        for (int row = 1; row <= Rows; row++)
        {
            if (row == 1)
                Console.WriteLine("Business Class ↓");
            else if (row == 11)
            {
                Console.WriteLine("\n                                     Business/Economy Separator");
                Console.WriteLine("\nEconomy Class ↓");
            }

            string rowNum = row < 10 ? $"0{row}" : row.ToString();
            int seatsInRow = row <= 10 ? 8 : 10;

            for (int seatIndex = 0; seatIndex < seatsInRow; seatIndex++)
            {
                string seat = SeatArrangement[index + seatIndex];
                if (BookedSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}  ");
                }
                else if (ChosenSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}  ");
                }
                else
                {
                    Console.Write($"{seat}  ");
                }
                Console.ResetColor();

                if (row <= 10)
                {
                    if (seatIndex == 1 || seatIndex == 5)
                        Console.Write("    ");
                }
                else
                {
                    if (seatIndex == 2 || seatIndex == 6)
                        Console.Write("    ");
                }
            }
            Console.WriteLine();
            index += seatsInRow;
        }
    }

    public void BookFlight(string seat, string initials)
    {
        if (AvailableSeats.Contains(seat))
        {
            AvailableSeats.Remove(seat);
            ChosenSeats.Add(seat);
            SeatInitials[seat] = initials + " ";
            Console.WriteLine($"Seat {seat} is temporarily chosen by {initials}.");
        }
        else if (BookedSeats.Contains(seat))
        {
            Console.WriteLine($"Seat {seat} is already booked by {(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : "someone")}.");
        }
        else
        {
            Console.WriteLine($"Seat {seat} is not available.");
        }
    }

    public void ConfirmBooking()
    {
        foreach (var seat in ChosenSeats)
        {
            BookedSeats.Add(seat);
        }
        ChosenSeats.Clear();
        Console.WriteLine("Seats have been successfully booked.");
    }

    public void ResetAllSeats()
    {
        // Move all booked seats back to available seats
        var allFlights = FlightsAccess.ReadAll();
        string filePath = "DataSources/bookedflights.json";

        foreach (var flight in allFlights)
        {
            // Iterate over a copy of BookedSeats to avoid modifying the collection while iterating
            foreach (var seat in flight.Layout.BookedSeats.ToList())
            {
                // Remove seat from BookedSeats
                flight.Layout.BookedSeats.Remove(seat);

                // Add seat to AvailableSeats
                flight.Layout.AvailableSeats.Add(seat);
            }

            flight.Layout.SeatInitials.Clear();
        }

        try
        {
            // Check if the file exists before deleting
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                Console.WriteLine($"The file '{filePath}' does not exist.");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions such as UnauthorizedAccessException or IOException
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        FlightsAccess.WriteAll(allFlights);
        Console.WriteLine("Seats have been reset");


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

    public static LayoutModel CreateBoeing757Layout()
    {
        int rows = 40;
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
}