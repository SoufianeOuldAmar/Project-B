// LayoutLogic.cs
using System;
using System.Collections.Generic;

public class LayoutLogic
{
    private LayoutModel _layoutModel;

    public LayoutLogic(LayoutModel layoutModel)
    {
        _layoutModel = layoutModel;
    }

    // Print function to display the layout with different colors
    public void PrintLayout()
    {
        for (int i = 0; i < _layoutModel.SeatArrangement.Count; i += _layoutModel.Columns)
        {
            int currentRow = (i / _layoutModel.Columns) + 1;

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

            for (int j = 0; j < _layoutModel.Columns; j++)
            {
                string seat = _layoutModel.SeatArrangement[i + j];

                // Print seat colors based on its status
                if (_layoutModel.BookedSeats.Contains(seat))
                {
                    // Change color to red for booked seats
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (_layoutModel.ChosenSeats.Contains(seat))
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

    // Book flight function (with chosen seats functionality)
    public void BookFlight(string seat)
    {
        if (_layoutModel.AvailableSeats.Contains(seat))
        {
            _layoutModel.AvailableSeats.Remove(seat);   // Remove from available
            _layoutModel.ChosenSeats.Add(seat);         // Add to chosen list (temporary before confirmation)
            Console.WriteLine($"Seat {seat} is temporarily chosen.");
        }
        else if (_layoutModel.BookedSeats.Contains(seat))
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
        foreach (var seat in _layoutModel.ChosenSeats)
        {
            _layoutModel.BookedSeats.Add(seat);  // Move chosen seats to booked seats
        }
        _layoutModel.ChosenSeats.Clear();         // Clear the chosen seats list
        Console.WriteLine("Seats have been successfully booked.");
    }
}
