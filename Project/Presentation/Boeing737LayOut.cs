using System;
using System.Collections.Generic;

namespace AirplaneSeatReservation.PresentationLayer
{
    public class AirplaneLayoutUI
    {
        private readonly AirplaneLayoutLogic layoutLogic;

        public AirplaneLayoutUI()
        {
            layoutLogic = new AirplaneLayoutLogic();
        }

        public void DisplayLayout(int selectedRow = -1, int selectedSeat = -1)
        {
            var seats = layoutLogic.GetSeats();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nAirplane Layout (Boeing 737):\n");
            Console.WriteLine("     A B C   D E F");
            Console.WriteLine("     -------------");
            for (int row = 0; row < seats.GetLength(0); row++)
            {
                if (row < 9)
                {
                    Console.Write("");
                }

                if (row == 15 || row == 16)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (row == 5 || row == 25) // Example emergency exit rows
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                Console.Write($"{row + 1:D2}   ");

                for (int seat = 0; seat < seats.GetLength(1); seat++)
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
                    else if (row == 5 || row == 25)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
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
            Console.WriteLine("     -------------\n");
            Console.WriteLine("Use your arrow keys to select a seat. Press enter to reserve the seat.");
            Console.WriteLine("\nSeat Summary:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Yellow seats are Business class seats with the price €200.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Cyan seats are Economy class seats with the price €100 or €125.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Red rows indicate emergency exit rows.");
            Console.ResetColor();
        }

        public void SelectSeat(int row, char seatLetter)
        {
            bool success = layoutLogic.SelectSeat(row, seatLetter);
            if (success)
            {
                Console.WriteLine($"Seat {row}{seatLetter} has been successfully booked.");
            }
            else
            {
                Console.WriteLine($"Seat {row}{seatLetter} is already taken or invalid. Please choose another seat.");
            }
        }
    }

    public class AirplaneLayoutLogic
    {
        private const int Rows = 33;
        private const int SeatsPerRow = 6;
        private Seat[,] seats;

        public AirplaneLayoutLogic()
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

        public Seat[,] GetSeats()
        {
            return seats;
        }

        public bool SelectSeat(int row, char seatLetter)
        {
            int seatIndex = seatLetter - 'A';
            if (row < 1 || row > Rows || seatIndex < 0 || seatIndex >= SeatsPerRow)
            {
                return false;
            }

            if (seats[row - 1, seatIndex].IsAvailable)
            {
                seats[row - 1, seatIndex].IsAvailable = false;
                return true;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AirplaneLayoutUI layoutUI = new AirplaneLayoutUI();
            layoutUI.DisplayLayout();

            while (true)
            {
                Console.WriteLine("\nSelect a seat by entering row number and seat letter (e.g., 12C), or type 'exit' to quit:");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    break;
                }
                else if (int.TryParse(input.Substring(0, input.Length - 1), out int row) && char.TryParse(input.Substring(input.Length - 1), out char seatLetter))
                {
                    layoutUI.SelectSeat(row, seatLetter);
                    Console.WriteLine("\nUpdated Layout:");
                    layoutUI.DisplayLayout();
                }
                else
                {
                    Console.WriteLine("Invalid input format.");
                }
            }
        }
    }
}