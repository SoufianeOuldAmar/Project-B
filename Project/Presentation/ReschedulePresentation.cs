using System.Net;
using System.Text;
using System.Text.Json;
using DataModels;
using DataAccess;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using PresentationLayer;
public static class ReschedulePresentation
{
    public static void RescheduleFlightMenu()
    {

    ChooseFlightID:
        Console.Write("\nPlease enter the Flight ID of the flight you want to reschedule (or enter 'Q' to quit): ");
        string chosenFlightID = Console.ReadLine();

        if (chosenFlightID.ToLower() == "q")
        {
            MenuPresentation.PressAnyKey();
            return;
        }


        if (!int.TryParse(chosenFlightID, out int chosenFlightIDInt))
        {
            MenuPresentation.PrintColored("\nInvalid input! Enter an integer!", ConsoleColor.Red);
            goto ChooseFlightID;
        }

        var eligibleFlights = RescheduleLogic.GetEligibleFlights(chosenFlightIDInt);

        if (eligibleFlights.Count == 0)
        {
            MenuPresentation.PrintColored("\nNo eligible flights found for rescheduling.", ConsoleColor.Red);
            MenuPresentation.PressAnyKey();
            return;
        }

        Console.WriteLine("Eligible flight(s) for rescheduling:");

        Console.Clear();

    ChooseNewFlight:
        foreach (var flight in eligibleFlights)
        {
            SearchFlightPresentation.PrintSearchResult(eligibleFlights);
        }
        Console.Write("\nEnter the index number of the flight you wish to reschedule to (or enter 'Q' to quit to operation): ");
        string newChosenFlight = Console.ReadLine();

        if (newChosenFlight.ToUpper() == "Q")
        {
            MenuPresentation.PrintColored("'\nYou decided to quit the operation", ConsoleColor.Red);
        }

        if (!int.TryParse(newChosenFlight, out int newChosenFlightInt))
        {
            MenuPresentation.PrintColored("\nInvalid input! Enter an integer!", ConsoleColor.Red);
            MenuPresentation.PressAnyKey();
            Console.Clear();
            goto ChooseNewFlight;
        }

        FlightModel newFlight;

        try
        {
            newFlight = eligibleFlights[newChosenFlightInt - 1];
        }
        catch (ArgumentOutOfRangeException)
        {
            MenuPresentation.PrintColored("\nInvalid flight selection. Please choose a valid flight number.", ConsoleColor.Red);
            MenuPresentation.PressAnyKey();
            Console.Clear();
            goto ChooseNewFlight;
        }

        List<string> occupiedSeats = RescheduleLogic.AreFormerSeatsTaken(newFlight.Id, chosenFlightIDInt).Item1;
        bool isFee = RescheduleLogic.AreFormerSeatsTaken(newFlight.Id, chosenFlightIDInt).Item2;

        var layout = newFlight.Layout;

        if (occupiedSeats.Count == 0)
        {   

            MenuPresentation.PrintColored($"\nFlight has been rescheduled to {newFlight.DepartureDate} {newFlight.FlightTime}.", ConsoleColor.Green);
        }
        else
        {
            foreach (string seat in occupiedSeats)
            {

            ChoosingNewSeat:
                LayoutPresentation.PrintLayout(layout);
                Console.Write($"Seat {seat} has been already taken. Choose another seat: ");
                string newSeat = Console.ReadLine();

                if (!LayoutLogic.TryBookSeat(layout, newSeat))
                {
                    MenuPresentation.PrintColored("This seat is already booked or invalid. Please choose another seat.", ConsoleColor.Red);
                    goto ChoosingNewSeat;
                }

                LayoutLogic.ArrangeNewSeat(layout, seat, newFlight);

            }
        }

        if (isFee) Console.WriteLine("You got a 50 euro fee because the ticket price of your new flight is lower");

        MenuPresentation.PressAnyKey();

    }
}
