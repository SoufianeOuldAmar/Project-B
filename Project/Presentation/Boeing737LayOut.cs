using System;
using System.Collections.Generic;

public class AirplaneLayout
{
    private const int Rows = 33;
    private const int SeatsPerRow = 6;
    private Seat[,] seats;

    public AirplaneLayout()
    {
        seats = new Seat[Rows, SeatsPerRow];
        InitializeSeats();
    }

    private void InitializeSeats()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int seat = 0; seat < SeatsPerRow; seat++)
            {
                if (row == 15 || row == 16)
                {
                    seats[row, seat] = new Seat(row + 1, (char)('A' + seat)) { Class = "Business", Price = 200m, IsAvailable = true };
                }
                else if (seat == 0 || seat == 5)
                {
                    seats[row, seat] = new Seat(row + 1, (char)('A' + seat)) { Class = "Economy", Price = 125m, IsAvailable = true };
                }
                else
                {
                    seats[row, seat] = new Seat(row + 1, (char)('A' + seat)) { Class = "Economy", Price = 100m, IsAvailable = true };
                }
            }
        }
    }

    public void DisplayLayout(int selectedRow = -1, int selectedSeat = -1)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nAirplane Layout (Boeing 737):\n");
        Console.WriteLine("      A  B  C    D  E  F");
        Console.WriteLine("     -------------------");
        for (int row = 0; row < Rows; row++)
        {
            if (row < 9)
            {
                Console.Write(" ");
            }

            if (row == 15 || row == 16)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            Console.Write($"{row + 1:D2}   ");

            for (int seat = 0; seat < SeatsPerRow; seat++)
            {
                if (row == selectedRow && seat == selectedSeat)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                if (row == 15 || row == 16)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                Console.Write(seats[row, seat].IsAvailable ? "O" : "X");

                if (seat == 2)
                {
                    Console.Write("  ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.ResetColor();
            }

            Console.WriteLine();
        }
        Console.WriteLine("     -------------------\n");
        Console.WriteLine("Use your arrow keys to select a seat. Press enter to reserve the seat.");
        Console.WriteLine("\nSeat Summary:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Yellow seats are Business class seats with the price €200.");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Cyan seats are Economy class seats with the price €100 or €125.");
        Console.ResetColor();
    }

    public void SelectSeat(int row, char seatLetter)
    {
        int seatIndex = seatLetter - 'A';
        if (row < 1 || row > Rows || seatIndex < 0 || seatIndex >= SeatsPerRow)
        {
            Console.WriteLine("Invalid seat selection. Please try again.");
            return;
        }

        if (seats[row - 1, seatIndex].IsAvailable)
        {
            seats[row - 1, seatIndex].IsAvailable = false;
            Console.WriteLine($"Seat {row}{seatLetter} has been successfully booked.");
        }
        else
        {
            Console.WriteLine($"Seat {row}{seatLetter} is already taken. Please choose another seat.");
        }
    }
}