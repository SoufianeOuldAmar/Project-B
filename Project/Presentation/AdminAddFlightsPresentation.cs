using System.Text.RegularExpressions;
using DataModels;
using System;
using System.Globalization;


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
            if (double.TryParse(input, out ticketPrice) && ticketPrice > 0)
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
            if (gate.Length >= 2 && gate.Length <= 3 && "ABCDEF".Contains(char.ToUpper(gate[0])) &&
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
        var europeanCapitalsAirports = new List<string>
            {
            "Amsterdam-Schiphol",
            "Athens-Eleftherios Venizelos",
            "Belgrade-Nikola Tesla",
            "Berlin-Brandenburg",
            "Brussels-Zaventem",
            "Bucharest-Henri Coandă",
            "Budapest-Ferenc Liszt",
            "Copenhagen-Kastrup",
            "Dublin-Dublin Airport",
            "Helsinki-Vantaa",
            "Lisbon-Humberto Delgado",
            "London-Heathrow",
            "Madrid-Barajas",
            "Oslo-Gardermoen",
            "Paris-Charles de Gaulle",
            "Prague-Václav Havel",
            "Rome-Fiumicino",
            "Stockholm-Arlanda",
            "Vienna-Schwechat",
            "Warsaw-Chopin"
            };
        string arrivalDestination;

        foreach(var capital in europeanCapitalsAirports)
        {
            Console.WriteLine($"- {capital}");
        }

        while (true)
        {
            Console.WriteLine("Enter Arrival Destination: ");
            arrivalDestination = Console.ReadLine();
            if (europeanCapitalsAirports.Contains(arrivalDestination))
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
            Console.WriteLine("Enter a Date (dd-mm-yyyy):");
            string input = Console.ReadLine(); // dd-mm-yyyy
            string[] dateParts = input.Split('-');

            if (dateParts.Length == 3)
            {
                string yearStr = dateParts[2];
                string monthStr = dateParts[1].PadLeft(2, '0');
                string dayStr = dateParts[0].PadLeft(2, '0');
                date = $"{dayStr}-{monthStr}-{yearStr}";

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
        int totalPets = 0;

        while (true)
        {
            Console.WriteLine("Enter the current total number of pets on this flight (0-7): ");
            string input = Console.ReadLine();

            // Check if input is a valid number
            if (int.TryParse(input, out totalPets))
            {

                if (totalPets >= 0 && totalPets <= 7)
                {
                    break; // Valid input exit
                }
                else
                {
                    Console.WriteLine("The number must be between 0 and 7."); 
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number."); 
            }
        }



        int nextFlightId = BookFlightPresentation.allFlights.Count;
        FlightModel newFlight = new FlightModel(

            airline,
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

        nextFlightId++;
        newFlight.Id = nextFlightId;
        newFlight.TotalPets = totalPets;

        FlightModel returnFlight = null;
        string returnDate;
        string returnTime;
        string returnGate;
        while (true)
        {
            Console.WriteLine("Add a return flight? (yes/no):");
            string returnFlightYesNo = Console.ReadLine().ToLower();

            if (returnFlightYesNo == "no")
            {
                break;
            }
            else if (returnFlightYesNo == "yes")
            {
                while (true)
                {
                    Console.WriteLine("Return Date (dd-mm-yyyy):");
                    string dateString = Console.ReadLine();

                    if (DateTime.TryParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        if (parsedDate <= DateTime.Now)
                        {
                            Console.WriteLine("The date is before the current time. Try again.");
                            continue;
                        }
                        else
                        {
                            returnDate = parsedDate.ToString("dd-MM-yyyy");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Try again.");
                        continue;
                    }

                }

                while (true)
                {
                    Console.WriteLine("Return Time (HH:MM)");
                    string timeStr = Console.ReadLine();

                    if (DateTime.TryParseExact(timeStr, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
                    {
                        returnTime = parsedTime.ToString("HH:mm");
                        break; // Exit the loop if valid
                    }
                    else
                    {
                        Console.WriteLine("Invalid time format. Please enter in HH:MM format.");
                        continue;
                    }
                }

                while (true)
                {
                    Console.WriteLine("Enter Gate: ");
                    string gate = Console.ReadLine();
                    if (gate.Length >= 2 && gate.Length <= 3 && "ABCDEF".Contains(char.ToUpper(gate[0])) &&
                    int.TryParse(gate.Substring(1), out int number) &&
                    number >= 1 && number <= 30)
                    {
                        // Substring(startIndex, length);
                        string letterPart = gate.Substring(0, 1).ToUpper();
                        string numberPart = gate.Substring(1);
                        returnGate = $"{letterPart}{numberPart}";
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. The string must be a letter followed by digit.");
                        continue;
                    }
                }

                returnFlight = newFlight.CreateReturnFlight(returnDate, returnTime, returnGate);
                break;
            }

        }

        newFlight.ReturnFlight = returnFlight;



        Console.WriteLine("New flight added:");
        return newFlight;
    }
    public void Exit()
    {
        Console.WriteLine("press any key...");
        Console.ReadKey();
        MenuLogic.PopMenu();
        // Console.Clear();
        // MenuPresentation.AuthenticateAccountMenu();


    }

}