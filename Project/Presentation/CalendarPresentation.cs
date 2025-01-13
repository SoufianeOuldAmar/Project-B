public static class CalendarPresentation
{
    public static void PrintCalendar(int month, int year, int currentDay, string departureAirport, string destination)
    {
        string header = CalendarLogic.GetCalendarHeader(month, year);
        (List<string> calendarLines, _) = CalendarLogic.GenerateCalendarLines(month, year, departureAirport, destination);

        Console.WriteLine(header);

        for (int i = 0; i < calendarLines.Count; i += 2)
        {
            string line = calendarLines[i];
            string dotLine = calendarLines[i + 1];

            string plainCurrentDay = $"{currentDay:D2}";
            string coloredCurrentDay = $"\x1b[32m{plainCurrentDay}\x1b[0m";

            if (line.Contains(coloredCurrentDay))
            {
                line = line.Replace(coloredCurrentDay, $"[{plainCurrentDay}]");
            }
            else if (line.Contains(plainCurrentDay))
            {
                line = line.Replace(plainCurrentDay, $"[{plainCurrentDay}]");
            }

            Console.WriteLine(line);
            Console.WriteLine(dotLine);
        }
    }

    public static DateTime RunCalendar(string departureAirport, string destination)
    {
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        int currentDay = 1;

        const int minYear = 2023;
        const int maxYear = 2030;

        while (true)
        {
            Console.Clear();
            PrintCalendar(currentMonth, currentYear, currentDay, departureAirport, destination);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("→ : Next day");
            Console.WriteLine("← : Previous day");
            Console.WriteLine("↑ : Jump one week earlier");
            Console.WriteLine("↓ : Jump one week later");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Green colour : Flight available on this date");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Enter: Select the highlighted date\n");
            Console.ResetColor();

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.RightArrow)
            {
                (currentDay, currentMonth, currentYear) = CalendarLogic.NavigateDate(currentDay, currentMonth, currentYear, "Right", minYear, maxYear);
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                (currentDay, currentMonth, currentYear) = CalendarLogic.NavigateDate(currentDay, currentMonth, currentYear, "Left", minYear, maxYear);
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                (currentDay, currentMonth, currentYear) = CalendarLogic.NavigateDate(currentDay, currentMonth, currentYear, "Up", minYear, maxYear);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                (currentDay, currentMonth, currentYear) = CalendarLogic.NavigateDate(currentDay, currentMonth, currentYear, "Down", minYear, maxYear);
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                return new DateTime(currentYear, currentMonth, currentDay);
            }
        }
    }
}
