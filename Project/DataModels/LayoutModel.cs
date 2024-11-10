public class LayoutModel
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<string> SeatArrangement { get; set; }
    public List<string> AvailableSeats { get; set; }
    public List<string> BookedSeats { get; set; }
    public List<string> ChosenSeats { get; set; }
    public bool IsAirbusA330 { get; set; }

    public LayoutModel(int rows, int columns, List<string> seatArrangement, bool isAirbusA330 = false)
    {
        Rows = rows;
        Columns = columns;
        SeatArrangement = seatArrangement;
        AvailableSeats = new List<string>(seatArrangement);
        BookedSeats = new List<string>();
        ChosenSeats = new List<string>();
        IsAirbusA330 = isAirbusA330;
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
                }
                else if (ChosenSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.Write($"{seat}  ");
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
            // Print business/economy class headers
            if (row == 1)
                Console.WriteLine("Business Class ↓");
            else if (row == 11)
            {
                Console.WriteLine("\n                                     Business/Economy Separator");
                Console.WriteLine("\nEconomy Class ↓");
            }

            string rowNum = row < 10 ? $"0{row}" : row.ToString();
            int seatsInRow = row <= 10 ? 8 : 10; // 8 seats for business, 10 for economy

            // Print each seat in the row
            for (int seatIndex = 0; seatIndex < seatsInRow; seatIndex++)
            {
                string seat = SeatArrangement[index + seatIndex];
                if (BookedSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (ChosenSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.Write($"{seat}  ");
                Console.ResetColor();

                // Add spacing between seat sections
                if (row <= 10) // Business class
                {
                    if (seatIndex == 1 || seatIndex == 5)
                        Console.Write("    ");
                }
                else // Economy class
                {
                    if (seatIndex == 2 || seatIndex == 6)
                        Console.Write("    ");
                }
            }
            Console.WriteLine();
            index += seatsInRow;
        }
    }

    public void BookFlight(string seat)
    {
        if (AvailableSeats.Contains(seat))
        {
            AvailableSeats.Remove(seat);
            ChosenSeats.Add(seat);
            Console.WriteLine($"Seat {seat} is temporarily chosen.");
        }
        else if (BookedSeats.Contains(seat))
        {
            Console.WriteLine($"Seat {seat} is already booked.");
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

    // Factory method to create a Boeing 737 layout
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

    // Factory method to create an Airbus A330-200 layout
    public static LayoutModel CreateAirbusA330200Layout()
    {
        int rows = 50;
        int columns = 10;
        List<string> seatArrangement = new List<string>();

        // Business Class (First 10 rows)
        for (int i = 1; i <= 10; i++)
        {
            string rowNumber = i < 10 ? $"0{i}" : $"{i}";
            // Left section (AB)
            seatArrangement.Add($"{rowNumber}A");
            seatArrangement.Add($"{rowNumber}B");
            // Middle section (DEFG)
            seatArrangement.Add($"{rowNumber}D");
            seatArrangement.Add($"{rowNumber}E");
            seatArrangement.Add($"{rowNumber}F");
            seatArrangement.Add($"{rowNumber}G");
            // Right section (JK)
            seatArrangement.Add($"{rowNumber}J");
            seatArrangement.Add($"{rowNumber}K");
        }

        // Economy Class (Remaining rows)
        for (int i = 11; i <= 50; i++)
        {
            string rowNumber = $"{i}";
            // Left section (ABC)
            seatArrangement.Add($"{rowNumber}A");
            seatArrangement.Add($"{rowNumber}B");
            seatArrangement.Add($"{rowNumber}C");
            // Middle section (DEFG)
            seatArrangement.Add($"{rowNumber}D");
            seatArrangement.Add($"{rowNumber}E");
            seatArrangement.Add($"{rowNumber}F");
            seatArrangement.Add($"{rowNumber}G");
            // Right section (HJK)
            seatArrangement.Add($"{rowNumber}H");
            seatArrangement.Add($"{rowNumber}J");
            seatArrangement.Add($"{rowNumber}K");
        }

        return new LayoutModel(rows, columns, seatArrangement, true);
    }

    // Factory method to create a Boeing 757 layout
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