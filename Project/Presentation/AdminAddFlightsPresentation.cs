using System.Text.RegularExpressions;
using DataModels;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace DataAccess;

public static class AdminAddFlightsPresentation
{
    public static void AddNewFlightsMenu()
    {
        Console.Clear();
        LayoutModel layout = LayoutLogic.CreateBoeing737Layout();

        Console.WriteLine("=== ➕ Add a new flight ===");

        double ticketPrice;
        while (true)
        {
            Console.Write("\nEnter Ticket Price (or 'q' to go back): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();
                    if (quitConfirmation == "yes")
                    {
                        return;
                    }
                    else if (quitConfirmation == "no")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, input either yes or no.");
                    }
                }

            }

            else if (AdminAddFlightsLogic.GetTicketPrice(input).Item1)
            {
                ticketPrice = AdminAddFlightsLogic.GetTicketPrice(input).Item2;
                break;
            }
            else
            {
                MenuPresentation.PrintColored("Invalid input. Please enter digits only and the digit must be above 0.", ConsoleColor.Red);
            }
        }

        string departureAirport;
        Console.WriteLine();

        int indexDep = 1;
        foreach (string departure in AdminAddFlightsLogic.GetAllDepartureAirports())
        {
            Console.WriteLine($"{indexDep}. {departure}");
            indexDep++;
        }

        while (true)
        {


            Console.Write("\nEnter Departure Airport by the index number (or 'q' to go back): ");
            string departureAirportIndex = Console.ReadLine();
            if (departureAirportIndex.ToLower() == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();
                    if (quitConfirmation == "yes")
                    {
                        return;
                    }
                    else if (quitConfirmation == "no")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, input either yes or no.");
                    }
                }
            }

            else if (AdminAddFlightsLogic.GetDepartureAirport(departureAirportIndex).Item1)
            {
                departureAirport = AdminAddFlightsLogic.GetDepartureAirport(departureAirportIndex).Item2;
                break;
            }
            else
            {
                MenuPresentation.PrintColored("\nInvalid index. Please choose the correct index.", ConsoleColor.Red);
            }
        }

        string arrivalDestination;

        int index = 1;
        foreach (var capital in AdminAddFlightsLogic.europeanCapitalsAirports)
        {
            Console.WriteLine($"{index}. {capital}");
            index++;
        }

        while (true)
        {


            Console.Write("\nEnter Arrival Destination (choose by index or 'q' to go back): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();
                    if (quitConfirmation == "yes")
                    {
                        return;
                    }
                    else if (quitConfirmation == "no")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, input either yes or no.");
                    }
                }
            }
            else if (AdminAddFlightsLogic.GetArrivalDestination(input).Item1)
            {
                arrivalDestination = AdminAddFlightsLogic.GetArrivalDestination(input).Item2;
                break;
            }
            else
            {
                MenuPresentation.PrintColored("\nInvalid input. Please enter a valid index number.", ConsoleColor.Red);
            }
        }


        string date;
        while (true)
        {
            Console.Write("\nEnter a Date (dd-mm-yyyy or 'q' to go back): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();
                    if (quitConfirmation == "yes")
                    {
                        return;
                    }
                    else if (quitConfirmation == "no")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, input either yes or no.");
                    }
                }

            }

            else if (AdminAddFlightsLogic.GetDate(input).Item1)
            {
                date = AdminAddFlightsLogic.GetDate(input).Item2;
                break;
            }
            else
            {
                MenuPresentation.PrintColored("\nInvalid date. Please enter a valid date.", ConsoleColor.Red);
            }
        }

        string time;
        while (true)
        {
            Console.Write("\nEnter Flight Time (HH:MM or 'q' to go back): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();
                    if (quitConfirmation == "yes")
                    {
                        return;
                    }
                    else if (quitConfirmation == "no")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, input either yes or no.");
                    }
                }



            }

            else if (AdminAddFlightsLogic.GetFlightTime(input).Item1)
            {
                time = AdminAddFlightsLogic.GetFlightTime(input).Item2;
                break;
            }
            else
            {
                MenuPresentation.PrintColored("\nInvalid time format. Please enter in HH:MM format.", ConsoleColor.Red);
            }
        }

        int totalPets = 0;

        string gateStr;
        while (true)
        {
            Console.Write("\nEnter Gate (letters A-Z and numbers 1-9) (or 'q' to go back): ");
            string gate = Console.ReadLine();
            if (gate.ToLower() == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();
                    if (quitConfirmation == "yes")
                    {
                        return;
                    }
                    else if (quitConfirmation == "no")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, input either yes or no.");
                    }
                }
            }


            else if (AdminAddFlightsLogic.GetGate(gate).Item1)
            {
                gateStr = AdminAddFlightsLogic.GetGate(gate).Item2;

                bool isConflict = AdminAddFlightsLogic.CheckConflict(gateStr, date, time);

                if (isConflict)
                {
                    Console.WriteLine("This gate is already occupied at the given time. Choose another gate.");
                }
                else
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid format. The string must be a letter followed by digit.");
            }
        }

        FlightModel newFlight = AdminAddFlightsLogic.CreateFlightModel(layout, ticketPrice, gateStr, departureAirport, arrivalDestination, date, time);

        FlightModel returnFlight = null;
        while (true)
        {
            Console.Write("\nAdd a return flight? (yes/no or 'q' to go back): ");
            string input = Console.ReadLine().ToLower();
            if (input == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();

                    if (quitConfirmation == "yes")
                    {
                        return;
                    }
                    else if (quitConfirmation == "no")
                    {
                        break;
                    }
                }
            }

            else if (input == "no") break;

            else if (input == "yes")
            {
                string returnDate;
                while (true)
                {
                    Console.Write("\nReturn Date (dd-mm-yyyy or 'q' to go back): ");
                    input = Console.ReadLine();
                    if (input.ToLower() == "q")
                    {

                        while (true)
                        {
                            Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                            string quitConfirmation = Console.ReadLine();

                            if (quitConfirmation == "yes")
                            {
                                return;
                            }
                            else if (quitConfirmation == "no")
                            {
                                break;
                            }
                        }

                    }

                    if (AdminAddFlightsLogic.GetDate(input).Item1)
                    {
                        returnDate = AdminAddFlightsLogic.GetDate(input).Item2;
                        break;
                    }
                    else
                    {
                        MenuPresentation.PrintColored("\nInvalid date. Please try again.", ConsoleColor.Red);
                    }
                }

                string returnTime;
                while (true)
                {
                    Console.Write("\nReturn Time (HH:MM or 'q' to go back): ");
                    input = Console.ReadLine();
                    if (input.ToLower() == "q")
                    {

                        while (true)
                        {
                            Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                            string quitConfirmation = Console.ReadLine();

                            if (quitConfirmation == "yes")
                            {
                                return;
                            }
                            else if (quitConfirmation == "no")
                            {
                                break;
                            }
                        }


                    }

                    else if (AdminAddFlightsLogic.GetFlightTime(input).Item1)
                    {
                        returnTime = AdminAddFlightsLogic.GetFlightTime(input).Item2;
                        break;
                    }
                    else
                    {
                        MenuPresentation.PrintColored("\nInvalid time format. Please try again.", ConsoleColor.Red);
                    }
                }

                string returnGate;
                while (true)
                {
                    Console.Write("\nEnter Return Gate (letters A-Z and numbers 1-9) (or 'q' to go back): ");
                    input = Console.ReadLine();
                    if (input.ToLower() == "q")
                    {

                        while (true)
                        {
                            Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                            string quitConfirmation = Console.ReadLine();


                            if (quitConfirmation == "yes")
                            {
                                return;
                            }
                            else if (quitConfirmation == "no")
                            {
                                break;
                            }
                        }

                    }

                    else if (AdminAddFlightsLogic.GetGate(input).Item1)
                    {
                        input = AdminAddFlightsLogic.GetGate(input).Item2;
                        string letterPart = input.Substring(0, 1).ToUpper();
                        string numberPart = input.Substring(1);
                        returnGate = $"{letterPart}{numberPart}";
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Please try again.");
                    }
                }

                returnFlight = FlightLogic.CreateReturnFlight(newFlight, returnDate, returnTime, returnGate);
                break;
            }
        }

        AdminAddFlightsLogic.CreateFlight(newFlight, returnFlight);

        MenuPresentation.PrintColored("\nNew flight added.", ConsoleColor.Green);

        MenuPresentation.PressAnyKey();
    }

}
