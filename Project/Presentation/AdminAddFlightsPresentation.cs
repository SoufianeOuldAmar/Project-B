using System.Text.RegularExpressions;
using DataModels;
using System;
using System.Globalization;
using System.Collections.Generic;
namespace DataAccess;

public static class AdminAddFlightsPresentation
{
    public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = DataManagerLogic.LoadAll();
    public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");

    public static FlightModel AddNewFlights()
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
                        return null;
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

            else if (double.TryParse(input, out ticketPrice) && ticketPrice > 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter digits only.");
            }
        }

        string departureAirport;
        while (true)
        {
            Console.Write("\nEnter Departure Airport (or 'q' to go back): ");
            departureAirport = Console.ReadLine();
            if (departureAirport.ToLower() == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();
                    if (quitConfirmation == "yes")
                    {
                        return null;
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

            else if (!string.IsNullOrEmpty(departureAirport))
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
        "Amsterdam-Schiphol", "Athens-Eleftherios Venizelos", "Belgrade-Nikola Tesla",
        "Berlin-Brandenburg", "Brussels-Zaventem", "Bucharest-Henri Coandă",
        "Budapest-Ferenc Liszt", "Copenhagen-Kastrup", "Dublin-Dublin Airport",
        "Helsinki-Vantaa", "Lisbon-Humberto Delgado", "London-Heathrow",
        "Madrid-Barajas", "Oslo-Gardermoen", "Paris-Charles de Gaulle",
        "Prague-Václav Havel", "Rome-Fiumicino", "Stockholm-Arlanda",
        "Vienna-Schwechat", "Warsaw-Chopin"
    };

        int index = 1;
        foreach (var capital in europeanCapitalsAirports)
        {
            Console.WriteLine($"{index}. {capital}");
            index++;
        }

        string arrivalDestination;
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
                        return null;
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

            else if (int.TryParse(input, out int arrivalDestinationIndex) &&
                arrivalDestinationIndex >= 1 && arrivalDestinationIndex <= europeanCapitalsAirports.Count)
            {
                arrivalDestination = europeanCapitalsAirports[arrivalDestinationIndex - 1];
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
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
                        return null;
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

            else if (DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate) &&
                parsedDate >= DateTime.Now)
            {
                date = parsedDate.ToString("dd-MM-yyyy");
                break;
            }
            else
            {
                Console.WriteLine("Invalid date. Please enter a valid date.");
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
                        return null;
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

            else if (DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
            {
                time = parsedTime.ToString("HH:mm");
                break;
            }
            else
            {
                Console.WriteLine("Invalid time format. Please enter in HH:MM format.");
            }
        }

        int totalPets = 0;
        while (true)
        {
            Console.Write("\nEnter the current total number of pets on this flight (0-7 or 'q' to go back): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();
                    if (quitConfirmation == "yes")
                    {
                        return null;
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

            else if (int.TryParse(input, out totalPets) && totalPets >= 0 && totalPets <= 7)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. The number must be between 0 and 7.");
            }
        }
        string gateStr;
        while (true)
        {
            Console.Write("\nEnter Gate (or 'q' to go back): ");
            string gate = Console.ReadLine();
            if (gate.ToLower() == "q")
            {
                while (true)
                {
                    Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                    string quitConfirmation = Console.ReadLine();
                    if (quitConfirmation == "yes")
                    {
                        return null;
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


            else if (gate.Length >= 2 && gate.Length <= 3 && "ABCDEF".Contains(char.ToUpper(gate[0])) &&
                int.TryParse(gate.Substring(1), out int number) &&
                number >= 1 && number <= 30)
            {
                string letterPart = gate.Substring(0, 1).ToUpper();
                string numberPart = gate.Substring(1);
                gateStr = $"{letterPart}{numberPart}";

                bool isConflict = allFlights.Any(flight => flight.Gate == gateStr &&
                flight.DepartureDate == date && flight.TimeOfDay == time);

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

        int nextFlightId = allFlights.Count;
        FlightModel newFlight = new FlightModel(
            layout,
            ticketPrice,
            gateStr,
            departureAirport,
            arrivalDestination,
            false,
            date,
            time,
            totalPets
        )
        {
            Id = ++nextFlightId
        };

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
                        return null;
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
                                return null;
                            }
                            else if (quitConfirmation == "no")
                            {
                                break;
                            }
                        }

                    }

                    if (DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedReturnDate) &&
                        parsedReturnDate > DateTime.Now)
                    {
                        returnDate = parsedReturnDate.ToString("dd-MM-yyyy");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date. Please try again.");
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
                                return null;
                            }
                            else if (quitConfirmation == "no")
                            {
                                break;
                            }
                        }


                    }

                    else if (DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedReturnTime))
                    {
                        returnTime = parsedReturnTime.ToString("HH:mm");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid time format. Please try again.");
                    }
                }

                string returnGate;
                while (true)
                {
                    Console.Write("\nEnter Return Gate (or 'q' to go back): ");
                    input = Console.ReadLine();
                    if (input.ToLower() == "q")
                    {

                        while (true)
                        {
                            Console.Write("\nDo you really want to quit this operation? (yes/no): ");
                            string quitConfirmation = Console.ReadLine();


                            if (quitConfirmation == "yes")
                            {
                                return null;
                            }
                            else if (quitConfirmation == "no")
                            {
                                break;
                            }
                        }

                    }

                    else if (input.Length >= 2 && input.Length <= 3 && "ABCDEF".Contains(char.ToUpper(input[0])) &&
                        int.TryParse(input.Substring(1), out int number) && number >= 1 && number <= 30)
                    {
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

        newFlight.ReturnFlight = returnFlight;

        Console.WriteLine("New flight added.");
        return newFlight;
    }

}
