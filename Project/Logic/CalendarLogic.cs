using DataAccess;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

public static class CalendarLogic
{
    // Read all flight data
    public static List<FlightModel> allFlights = FlightsAccess.ReadAll();

    public static string Calendar(int month, int year, string departureAirport, string destination)
    {
        // Get the first day and the total number of days in the month
        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);
        
        // Determine the day of the week the month starts on (e.g., Sunday = 0, Monday = 1)
        int startingDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

        // Prepare lines for the calendar display
        List<string> calendarLines = new List<string>();
        string daysLine = ""; 
        string dotsLine = ""; 

        // Add leading spaces for the first week to align with the starting day
        for (int i = 0; i < startingDayOfWeek; i++)
        {
            daysLine += "    "; 
            dotsLine += "    "; 
        }


        for (int day = 1; day <= daysInMonth; day++)
        {
            // Create the current date for comparison with flight dates 
            DateTime currentDate = new DateTime(year, month, day);

            // Checks if there is a flight on this date
            bool hasFlight = false;
            foreach (var flight in allFlights)
            {
                DateTime departureDate;
                if (DateTime.TryParse(flight.DepartureDate, out departureDate))
                {
                    // Check if the flight's date matches and if it's for the selected departure airport and destination
                    if (departureDate.Date == currentDate.Date &&
                        (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport == departureAirport) &&
                        (string.IsNullOrEmpty(destination) || flight.ArrivalDestination == destination) &&
                        !flight.IsCancelled)
                    {
                        hasFlight = true;
                        break;
                    }
                }
            }

            // Add the day number (adding a zero if its one number) 
            daysLine += day.ToString("D2") + "  ";

            // Add a dot if there's a flight 
            if (hasFlight)
            {
                dotsLine += " .  ";
            }
            else
            {
                dotsLine += "    ";
            }

            // Check if week is done or its the last day 
            startingDayOfWeek++;
            if (startingDayOfWeek == 7 || day == daysInMonth)
            {

                calendarLines.Add(daysLine.TrimEnd()); // Add the day numbers line
                calendarLines.Add(dotsLine.TrimEnd()); // Add the dots line

                // Reset the lines for the next week
                daysLine = "";
                dotsLine = "";
                startingDayOfWeek = 0; // Reset to the first day of the week
            }
        }

        // Join the lines into a single string with newlines in between
        string calendarOutput = string.Join("\n", calendarLines);
        return calendarOutput;
    }


    public static void PrintCalendar(int month, int year, int currentDay, string departureAirport, string destination)
    {
        var calendarData = Calendar(month, year, departureAirport, destination);
        Console.WriteLine($"{year}               {new DateTime(year, month, 1).ToString("MMMM")}");
        Console.WriteLine("Sun Mon Tue Wed Thu Fri Sat");

        string[] calendarLines = calendarData.Split('\n');
        for (int i = 0; i < calendarLines.Length; i += 2)
        {
            string line = calendarLines[i];
            string dotLine = calendarLines[i + 1];
            if (line.Contains($"{currentDay:D2} "))
            {
                line = line.Replace($"{currentDay:D2} ", $"[{currentDay:D2}] "); // Highlight current day
            }
            Console.WriteLine(line);
            Console.WriteLine(dotLine);
        }
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


















