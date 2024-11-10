public class LayoutModel
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<string> SeatArrangement { get; set; }
    public List<string> AvailableSeats { get; set; }
    public List<string> BookedSeats { get; set; }
    public List<string> ChosenSeats { get; set; } // Track temporarily chosen seats

    public LayoutModel(int rows, int columns, List<string> seatArrangement)
    {
        Rows = rows;
        Columns = columns;
        SeatArrangement = seatArrangement;
        AvailableSeats = new List<string>(seatArrangement); // Initially, all seats are available
        BookedSeats = new List<string>();
        ChosenSeats = new List<string>(); // Initially, no seats are chosen
    }

    // Print function to display the layout with different colors
    public void PrintLayout()
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

                if (j == 5)
                {
                    Console.Write(seatTypeHeaderEconomy);
                    Console.Write(seatTypeHeaderBusiness);
                }
            }
            Console.WriteLine();
        }
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

        for (int i = 1; i <= rows; i++)
        {
            string rowNumber = i < 10 ? $"0{i}" : $"{i}";

            if (i >= 1 && i <= 10)
            {
                seatArrangement.Add($"{rowNumber}A");
                seatArrangement.Add($"{rowNumber}B");
                seatArrangement.Add($"{rowNumber}D");
                seatArrangement.Add($"{rowNumber}E");
                seatArrangement.Add($"{rowNumber}F");
                seatArrangement.Add($"{rowNumber}G");
                seatArrangement.Add($"{rowNumber}J");
                seatArrangement.Add($"{rowNumber}K");
            }
            else
            {
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
        }

        return new LayoutModel(rows, columns, seatArrangement);
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

    // Book flight function (with chosen seats functionality)
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

    // Confirm booking of chosen seats
    public void ConfirmBooking()
    {
        foreach (var seat in ChosenSeats)
        {
            BookedSeats.Add(seat);
        }
        ChosenSeats.Clear();
        Console.WriteLine("Seats have been successfully booked.");
    }
}