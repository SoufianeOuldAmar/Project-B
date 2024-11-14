using System.Text.RegularExpressions;
using DataModels;

public class AdminAddFlightsPresentation
{
    public FlightModel AddNewFlights()
    {
        Console.Clear();
        LayoutModel layout = LayoutModel.CreateBoeing737Layout();
        string airline;
        while (true)
        {
            Console.WriteLine("Enter the Airline name: ");
            airline = Console.ReadLine();
            if (airline is string)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter letters only.");
            }

        }
        double ticketPrice;
        while (true)
        {
            Console.WriteLine("Enter Ticket Price: ");
            string input = Console.ReadLine();
            if (double.TryParse(input, out ticketPrice))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter letters only.");
            }

        }

        // Console.WriteLine("Enter Gate: ");
        // string gate = Console.ReadLine();
        string gateStr;
        while (true)
        {
            Console.WriteLine("Enter Gate: ");
            string gate = Console.ReadLine();
            if (gate.Length >= 2 && gate.Length <= 3 && "ABCDF".Contains(char.ToUpper(gate[0])) &&
            int.TryParse(gate.Substring(1), out int number) &&
            number >= 1 && number <= 30)
            {
                // Substring(startIndex, length);
                string letterPart = gate.Substring(0, 1).ToUpper();
                string numberPart = gate.Substring(1);
                gateStr = $"{letterPart}{numberPart}";
                break;
            }
            else
            {
                Console.WriteLine("Invalid format. The string must be a letter followed by digit.");
            }
        }
        string departureAirport;
        while (true)
        {
            Console.WriteLine("Enter Departure Airport: ");
            departureAirport = Console.ReadLine();
            if (departureAirport is string)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter letters only.");
            }
        }
        string arrivalDestination;
        while (true)
        {
            Console.WriteLine("Enter Arrival Destination: ");
            arrivalDestination = Console.ReadLine();
            if (arrivalDestination is string)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter letters only.");
            }
        }

        Console.WriteLine("Is the flight cancelled? (yes/no): ");
        bool IsCancelled = Console.ReadLine().ToLower() == "yes";

        string date;
        while (true)
        {
            Console.WriteLine("Enter a Date (yyyy-mm-dd):");
            string input = Console.ReadLine(); // yyyy-mm-dd
            string[] dateParts = input.Split('-');

            if (dateParts.Length == 3)
            {
                string yearStr = dateParts[0];
                string monthStr = dateParts[1].PadLeft(2, '0');
                string dayStr = dateParts[2].PadLeft(2, '0');
                date = $"{yearStr}-{monthStr}-{dayStr}";

                if (yearStr.Length == 4 && monthStr.Length == 2 && dayStr.Length == 2)
                {
                    int year = int.Parse(yearStr);
                    int month = int.Parse(monthStr);
                    int day = int.Parse(dayStr);

                    if ((month == 4 || month == 6 || month == 9 || month == 11) && day >= 1 && day <= 30 && year >= 2024)
                    {
                        break;
                    }
                    else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && day >= 1 && day <= 31 && year >= 2024)
                    {
                        break;
                    }
                    else if (month == 2 && day >= 1 && day <= 28 && year >= 2024)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date. Please enter a valid date.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-mm-dd format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-mm-dd format.");
            }
        }


        // Console.WriteLine("Enter Flight Time (HH:MM): ");
        // string flightTime = Console.ReadLine();
        string time;
        while (true)
        {
            Console.WriteLine("Enter Flight Time (HH:MM): ");
            string flightTime = Console.ReadLine();
            if (!flightTime.Contains(":"))
            {
                Console.WriteLine("Invalid format. Please use ':' as a separator between hours and minutes.");
                continue;
            }
            string[] flightParts = flightTime.Split(':');

            string hourStr = flightParts[0];
            string minStr = flightParts[1].PadLeft(2, '0');

            if (int.TryParse(hourStr, out int hour) && hour >= 0 && hour <= 23 &&
            int.TryParse(minStr, out int minute) && minute >= 0 && minute <= 59)
            {
                time = $"{hourStr}:{minStr}";
                break;
            }
            else
            {
                Console.WriteLine("Invalid Time, Please try again.");
            }

        }
        int nextFlightId = BookFlightPresentation.allFlights.Count;
        FlightModel newFight = new FlightModel(

            airline,
            layout,
            ticketPrice,
            gateStr,
            departureAirport,
            arrivalDestination,
            IsCancelled,
            date,
            time,
            0
        );

        nextFlightId++;
        newFight.Id = nextFlightId;


        Console.WriteLine("New flight added:");
        return newFight;
    }

}