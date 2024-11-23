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

    public void PrintLayout()
    {
        if (IsAirbusA330)
        {
            PrintAirbusA330Layout();
        }
        else if (IsBoeing787)
        {
            PrintBoeing787Layout();
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

            for (int j = 0; j < Columns; j++)
            {
                string seat = SeatArrangement[i + j];

                // Business sınıfı 1-9. satırlar
                if (currentRow >= 1 && currentRow <= 9)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Yellow colour (Business)
                }
                // Ekonomi sınıfı 10. satırdan itibaren
                else if (currentRow >= 10)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; // Cyan colour (Economy)
                }

                if (BookedSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Red colour (Booked)
                    Console.Write($"{seat}  ");
                }
                else if (ChosenSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta; // Purple colour (Selected)
                    Console.Write($"{seat}  ");
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
                if (row <= 10)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Yellow colour (Business)
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; // Cyan colour (Economy)
                }

                if (BookedSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Red colour (Booked)
                    Console.Write($"{seat}  ");
                }
                else if (ChosenSeats.Contains(seat))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta; // Purple colour (Selected)
                    Console.Write($"{seat}  ");
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

    private void PrintBoeing787Layout()
    {
        int index = 0;
        for (int row = 1; row <= Rows; row++)
        {
            if (row == 1)
                Console.WriteLine("Business Class ↓");
            else if (row == 7)
            {
                Console.WriteLine("\nEconomy Class ↓");
            }

            if (row <= 6) // Business Class
            {
                for (int seatIndex = 0; seatIndex < 6; seatIndex++)
                {
                    string seat = SeatArrangement[index + seatIndex];

                    if (BookedSeats.Contains(seat))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}  ");
                    }
                    else if (ChosenSeats.Contains(seat))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}  ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; // Business Class color
                        Console.Write($"{seat}  ");
                    }

                    Console.ResetColor();

                    if (seatIndex == 1 || seatIndex == 3)
                        Console.Write("    ");
                }
                index += 6;
            }
            else if (row != 15 && row != 27) // Economy Class (excluding special rows)
            {
                for (int seatIndex = 0; seatIndex < 9; seatIndex++)
                {
                    string seat = SeatArrangement[index + seatIndex];

                    if (BookedSeats.Contains(seat))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}  ");
                    }
                    else if (ChosenSeats.Contains(seat))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}  ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Economy Class color
                        Console.Write($"{seat}  ");
                    }

                    Console.ResetColor();

                    if (seatIndex == 2 || seatIndex == 5)
                        Console.Write("    ");
                }
                index += 9;
            }
            else if (row == 15)
            {
                Console.Write("       [EXIT]         [EXIT]          [EXIT]");
            }
            else if (row == 27)
            {
                Console.Write("        [LAV]          [GAL]          [LAV]");
            }
            Console.WriteLine();
        }
    }

    private void PrintSeat(string seat)
    {
        if (BookedSeats.Contains(seat))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}]");
        }
        else if (ChosenSeats.Contains(seat))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[{(SeatInitials.ContainsKey(seat) ? SeatInitials[seat] : seat)}]");
        }
        else
        {
            Console.ResetColor();
            Console.Write($"[{seat}]");
        }
        Console.ResetColor();
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
        int rows = 42;  // Total rows including business and economy
        int columns = 9; // Maximum columns in economy section
        List<string> seatArrangement = new List<string>();

        // Business Class (Rows 1-6)
        for (int i = 1; i <= 6; i++)
        {
            string rowNumber = i < 10 ? $"0{i}" : $"{i}";
            // Skip B, E, F, J seats in business class for 2-2-2 configuration
            seatArrangement.Add($"{rowNumber}A");
            seatArrangement.Add($"{rowNumber}C");
            seatArrangement.Add($"{rowNumber}D");
            seatArrangement.Add($"{rowNumber}G");
            seatArrangement.Add($"{rowNumber}H");
            seatArrangement.Add($"{rowNumber}K");
        }

        // Economy Class (Rows 7-42)
        for (int i = 7; i <= 42; i++)
        {
            // Skip row 15 (emergency exit) and row 27 (lavatory/galley)
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