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

            // Determine the seat type headers based on the current row
            string seatTypeHeaderBusiness = currentRow == 1 ? "Business Class ↓" : "";
            string seatTypeHeaderEconomy = currentRow == 10 ? "Economy Class ↓" : "";

            // Add a space before row 10 (for exit row formatting)
            if (currentRow == 10)
            {
                Console.WriteLine(); // Empty line
                Console.WriteLine("                                     Exit row");
                Console.WriteLine();
            }

            for (int j = 0; j < Columns; j++)
            {
                string seat = SeatArrangement[i + j];

                // Print seat colors based on its status
                if (BookedSeats.Contains(seat))
                {
                    // Change color to red for booked seats
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (ChosenSeats.Contains(seat))
                {
                    // Change color to yellow for temporarily chosen seats
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                // Print seat
                Console.Write($"{seat}  ");

                // Reset color to default
                Console.ResetColor();

                // Add space between seats C and D
                if (j == 2)
                {
                    Console.Write("       "); // Space for walking
                }

                if (j == 5)
                {
                    Console.Write(seatTypeHeaderEconomy);
                    Console.Write(seatTypeHeaderBusiness);
                }
            }
            Console.WriteLine(); // Move to the next line after printing one row
        }
    }

    // Factory method to create a Boeing 737 layout
    public static LayoutModel CreateBoeing737Layout()
    {
        int rows = 30;  // typical number of rows in a 737
        int columns = 6;  // 6 seats per row (A-F)

        List<string> seatArrangement = new List<string>();
        for (int i = 1; i <= rows; i++)
        {
            // Add leading zero for single-digit row numbers
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

    // Factory method to create a Boeing 757 layout
    public static LayoutModel CreateBoeing757Layout()
    {
        int rows = 40;  // typical number of rows in a 757
        int columns = 6;  // 6 seats per row (A-F)

        List<string> seatArrangement = new List<string>();
        for (int i = 1; i <= rows; i++)
        {
            // Add leading zero for single-digit row numbers
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
            AvailableSeats.Remove(seat);   // Remove from available
            ChosenSeats.Add(seat);         // Add to chosen list (temporary before confirmation)
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

        Console.WriteLine("Available seats after booking:");
        foreach (var availableSeat in AvailableSeats)
        {
            Console.WriteLine(availableSeat);
        }
    }

    // Confirm booking of chosen seats
    public void ConfirmBooking()
    {
        foreach (var seat in ChosenSeats)
        {
            BookedSeats.Add(seat);  // Move chosen seats to booked seats
        }
        ChosenSeats.Clear();         // Clear the chosen seats list
        Console.WriteLine("Seats have been successfully booked.");

    }
}
