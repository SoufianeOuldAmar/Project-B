using DataAccess;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

public static class CalendarLogic
{
    // Read all flight data
    public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");

    public static string GetCalendarHeader(int month, int year)
    {
        return $"{year}               {new DateTime(year, month, 1).ToString("MMMM")}\nSun Mon Tue Wed Thu Fri Sat";
    }

    public static (List<string> calendarLines, int startingDayOfWeek) GenerateCalendarLines(int month, int year, string departureAirport, string destination)
    {
        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);
        int startingDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

        List<string> calendarLines = new List<string>();
        string daysLine = "";
        string dotsLine = "";

        for (int i = 0; i < startingDayOfWeek; i++)
        {
            daysLine += "    ";
            dotsLine += "    ";
        }

        for (int day = 1; day <= daysInMonth; day++)
        {
            DateTime currentDate = new DateTime(year, month, day);
            bool hasFlight = allFlights.Any(flight =>
                DateTime.TryParse(flight.DepartureDate, out DateTime departureDate) &&
                departureDate.Date == currentDate.Date &&
                (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport == departureAirport) &&
                (string.IsNullOrEmpty(destination) || flight.ArrivalDestination == destination) &&
                !flight.IsCancelled);

            if (hasFlight)
            {
                daysLine += $"\x1b[32m{day:D2}\x1b[0m  ";
                dotsLine += "   ";
            }
            else
            {
                daysLine += $"{day:D2}  ";
                dotsLine += "    ";
            }

            startingDayOfWeek++;
            if (startingDayOfWeek == 7 || day == daysInMonth)
            {
                calendarLines.Add(daysLine.TrimEnd());
                calendarLines.Add(dotsLine.TrimEnd());
                daysLine = "";
                dotsLine = "";
                startingDayOfWeek = 0;
            }
        }

        return (calendarLines, startingDayOfWeek);
    }


    public static (int, int, int) NavigateDate(int currentDay, int currentMonth, int currentYear, string direction, int minYear, int maxYear)
    {
        if (direction == "Right")
        {
            currentDay++;
            if (currentDay > DateTime.DaysInMonth(currentYear, currentMonth))
            {
                currentDay = 1;
                currentMonth++;
                if (currentMonth > 12)
                {
                    currentMonth = 1;
                    currentYear++;
                }
            }
        }
        else if (direction == "Left")
        {
            currentDay--;
            if (currentDay < 1)
            {
                currentMonth--;
                if (currentMonth < 1)
                {
                    currentMonth = 12;
                    currentYear--;
                }
                currentDay = DateTime.DaysInMonth(currentYear, currentMonth);
            }
        }
        else if (direction == "Up")
        {
            currentDay -= 7;
            if (currentDay < 1)
            {
                currentMonth--;
                if (currentMonth < 1)
                {
                    currentMonth = 12;
                    currentYear--;
                }
                currentDay = DateTime.DaysInMonth(currentYear, currentMonth) + currentDay;
            }
        }
        else if (direction == "Down")
        {
            currentDay += 7;
            if (currentDay > DateTime.DaysInMonth(currentYear, currentMonth))
            {
                currentDay -= DateTime.DaysInMonth(currentYear, currentMonth);
                currentMonth++;
                if (currentMonth > 12)
                {
                    currentMonth = 1;
                    currentYear++;
                }
            }
        }

        if (currentYear < minYear) currentYear = minYear;
        if (currentYear > maxYear) currentYear = maxYear;

        return (currentDay, currentMonth, currentYear);
    }

    // public static List<FlightModel> GetFlightsByDate(DateTime date)
    // {
    //     return allFlights
    //         .Where(flight => DateTime.TryParse(flight.DepartureDate, out DateTime departureDate) && departureDate.Date == date.Date && !flight.IsCancelled).ToList();
    // }
    public static List<FlightModel> GetFlightsByDate(DateTime date, string departureAirport, string destination)
    {
        return allFlights
            .Where(flight =>
                (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport == departureAirport) &&  // Check if departure is given, or ignore 
                (string.IsNullOrEmpty(destination) || flight.ArrivalDestination == destination) &&  // Check if destination is given, or ignore 
                DateTime.TryParse(flight.DepartureDate, out DateTime departureDate) &&
                departureDate.Date == date.Date && // Check if flight's departure date matches the selected date
                !flight.IsCancelled)
            .ToList();
    }


}
