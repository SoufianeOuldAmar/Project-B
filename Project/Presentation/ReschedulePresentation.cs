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
    public static string fileName = "DataSources/flights.json";
    public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = DataManagerLogic.LoadAll();
    public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");


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
            MenuPresentation.PrintColored("Invalid input! Enter an integer!", ConsoleColor.Red);
            goto ChooseFlightID;
        }

        var eligibleFlights = RescheduleLogic.GetEligibleFlights(chosenFlightIDInt);

        if (eligibleFlights.Count == 0)
        {
            Console.WriteLine("No eligible flights found for rescheduling.");
            MenuPresentation.PressAnyKey();
            return;
        }

        Console.WriteLine("Eligible flight(s) for rescheduling:");

        Console.Clear();

        foreach (var flight in eligibleFlights)
        {
            SearchFlightPresentation.PrintSearchResult(eligibleFlights);
        }

    ChooseNewFlight:
        Console.Write("\nEnter the index number of the flight you wish to reschedule to: ");
        string newChosenFlight = Console.ReadLine();

        if (!int.TryParse(newChosenFlight, out int newChosenFlightInt))
        {
            MenuPresentation.PrintColored("Invalid input! Enter an integer!", ConsoleColor.Red);
            goto ChooseNewFlight;
        }

        FlightModel newFlight = eligibleFlights[newChosenFlightInt - 1];
        List<string> occupiedSeats = RescheduleLogic.AreFormerSeatsTaken(newFlight.Id, chosenFlightIDInt);
        var layout = newFlight.Layout;

        if (occupiedSeats.Count == 0)
        {
            MenuPresentation.PrintColored($"Flight has been rescheduled to {newFlight.DepartureDate} {newFlight.FlightTime}.", ConsoleColor.Green);
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
        MenuPresentation.PressAnyKey();

    }
}
