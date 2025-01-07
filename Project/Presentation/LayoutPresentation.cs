namespace PresentationLayer
{
    public static class LayoutPresentation
    {
        public static void PrintLayout(LayoutModel layout)
        {
            if (layout.IsAirbusA330)
            {
                PrintAirbusA330Layout(layout);
            }
            else if (layout.IsBoeing787)
            {
                PrintBoeing787Layout(layout);
            }
            else
            {
                PrintStandardLayout(layout);
            }
        }

        private static void PrintStandardLayout(LayoutModel layout)
        {
            for (int i = 0; i < layout.SeatArrangement.Count; i += layout.Columns)
            {
                int currentRow = (i / layout.Columns) + 1;

                for (int j = 0; j < layout.Columns; j++)
                {
                    string seat = layout.SeatArrangement[i + j];

                    if (currentRow >= 1 && currentRow <= 9)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (currentRow >= 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                    if (layout.BookedSeats.Contains(seat))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        string display = layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat;
                        Console.Write($"{display,-4}");
                    }
                    else if (layout.ChosenSeats.Contains(seat))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        string display = layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat;
                        Console.Write($"{display,-4}");
                    }
                    else
                    {
                        Console.Write($"{seat,-4}");
                    }

                    Console.ResetColor();

                    if (j == 2)
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }
        }

        private static void PrintAirbusA330Layout(LayoutModel layout)
        {
            int index = 0;
            for (int row = 1; row <= layout.Rows; row++)
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
                    string seat = layout.SeatArrangement[index + seatIndex];
                    if (row <= 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                    if (layout.BookedSeats.Contains(seat))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        string display = layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat;
                        Console.Write($"{display}  ");
                    }
                    else if (layout.ChosenSeats.Contains(seat))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        string display = layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat;
                        Console.Write($"{display}  ");
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

        private static void PrintBoeing787Layout(LayoutModel layout)
        {
            int index = 0;
            for (int row = 1; row <= layout.Rows; row++)
            {
                if (row == 1)
                    Console.WriteLine("Business Class ↓");
                else if (row == 7)
                {
                    Console.WriteLine("\nEconomy Class ↓");
                }

                if (row <= 6)
                {
                    for (int seatIndex = 0; seatIndex < 6; seatIndex++)
                    {
                        string seat = layout.SeatArrangement[index + seatIndex];

                        if (layout.BookedSeats.Contains(seat))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            string display = layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat;
                            Console.Write($"{display}  ");
                        }
                        else if (layout.ChosenSeats.Contains(seat))
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            string display = layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat;
                            Console.Write($"{display}  ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"{seat}  ");
                        }

                        Console.ResetColor();

                        if (seatIndex == 1 || seatIndex == 3)
                            Console.Write("    ");
                    }
                    index += 6;
                }
                else if (row != 15 && row != 27)
                {
                    for (int seatIndex = 0; seatIndex < 9; seatIndex++)
                    {
                        string seat = layout.SeatArrangement[index + seatIndex];

                        if (layout.BookedSeats.Contains(seat))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            string display = layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat;
                            Console.Write($"{display}  ");
                        }
                        else if (layout.ChosenSeats.Contains(seat))
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            string display = layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat;
                            Console.Write($"{display}  ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
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

        private static void PrintSeat(LayoutModel layout, string seat)
        {
            if (layout.BookedSeats.Contains(seat))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"[{(layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat)}]");
            }
            else if (layout.ChosenSeats.Contains(seat))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[{(layout.SeatInitials.ContainsKey(seat) ? layout.SeatInitials[seat] : seat)}]");
            }
            else
            {
                Console.ResetColor();
                Console.Write($"[{seat}]");
            }
            Console.ResetColor();
        }

        public static void PrintBookingSuccess(string seat, string initials)
        {
            Console.WriteLine($"Seat {seat} is temporarily chosen by {initials}.");
        }

        public static void PrintSeatAlreadyBooked(string seat, string bookedBy)
        {
            Console.WriteLine($"Seat {seat} is already booked by {bookedBy}.");
        }

        public static void PrintSeatNotAvailable(string seat)
        {
            Console.WriteLine($"Seat {seat} is not available.");
        }

        public static void PrintBookingConfirmed()
        {
            Console.WriteLine("Seats have been successfully booked.");
        }
    }
}