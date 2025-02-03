using System.Text.RegularExpressions;
using DataModels;
using System;
using System.Globalization;
using System.Collections.Generic;
namespace DataAccess;

public static class AdminAddFlightsLogic
{
    public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");

    public static string[] europeanCapitalsAirports = new string[]
    {
        "Amsterdam-Schiphol", "Athens-Eleftherios Venizelos", "Belgrade-Nikola Tesla",
        "Berlin-Brandenburg", "Brussels-Zaventem", "Bucharest-Henri Coandă",
        "Budapest-Ferenc Liszt", "Copenhagen-Kastrup", "Dublin-Dublin Airport",
        "Helsinki-Vantaa", "Lisbon-Humberto Delgado", "London-Heathrow",
        "Madrid-Barajas", "Oslo-Gardermoen", "Paris-Charles de Gaulle",
        "Prague-Václav Havel", "Rome-Fiumicino", "Stockholm-Arlanda",
        "Vienna-Schwechat", "Warsaw-Chopin"
    };

    public static (bool, double) GetTicketPrice(string input)
    {
        return (double.TryParse(input, out double ticketPrice) && ticketPrice > 0, ticketPrice);
    }

    public static (bool, string) GetDepartureAirport(string departureIndex)
    {
        List<string> allDepartureAirports = GetAllDepartureAirports();

        if (int.TryParse(departureIndex, out int departureIndexInt) && departureIndexInt > 0 && departureIndexInt <= allDepartureAirports.Count)
        {
            return (true, allDepartureAirports[departureIndexInt - 1]);
        }
        else
        {
            return (false, "");
        }
    }

    public static List<string> GetAllDepartureAirports()
    {
        return allFlights
            .Select(flight => flight.DepartureAirport)
            .Distinct()
            .OrderBy(airport => airport)
            .ToList();
    }

    public static (bool, string) GetArrivalDestination(string arrivalIndex)
    {

        if (int.TryParse(arrivalIndex, out int arrivalIndexInt) && arrivalIndexInt > 0 && arrivalIndexInt <= europeanCapitalsAirports.Length)
        {
            return (true, europeanCapitalsAirports[arrivalIndexInt - 1]);
        }
        else
        {
            return (false, "");
        }
    }

    public static (bool, string) GetDate(string input)
    {
        return (DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate) &&
                parsedDate >= DateTime.Now, parsedDate.ToString("dd-MM-yyyy"));
    }

    public static (bool, string) GetFlightTime(string input)
    {
        return (DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime), parsedTime.ToString("HH:mm"));
    }

    public static (bool, string) GetGate(string gate)
    {
        bool isValidGate = gate.Length >= 2 && gate.Length <= 3 && "ABCDEF".Contains(char.ToUpper(gate[0])) &&
                int.TryParse(gate.Substring(1), out int number) &&
                number >= 1 && number <= 30;

        string letterPart = gate.Substring(0, 1).ToUpper();
        string numberPart = gate.Substring(1);
        string gateStr = $"{letterPart}{numberPart}";

        return (isValidGate, gateStr);
    }

    public static bool CheckConflict(string gateStr, string date, string time)
    {
        return allFlights.Any(flight => flight.Gate == gateStr &&
                flight.DepartureDate == date && flight.TimeOfDay == time);
    }

    public static void CreateFlight(FlightModel flight, FlightModel returnFlight)
    {
        flight.ReturnFlight = returnFlight;
        DataAccessClass.AddSingleFlight(flight);
    }

    public static FlightModel CreateFlightModel(LayoutModel layout, double ticketPrice, string gateStr, string departureAirport, string arrivalDestination, string date, string time)
    {
        int nextFlightId = allFlights.Count;
        FlightModel newFlight = new FlightModel(
            ++nextFlightId,
            layout,
            ticketPrice,
            gateStr,
            departureAirport,
            arrivalDestination,
            false,
            date,
            time,
            0
        );

        return newFlight;
    }

}