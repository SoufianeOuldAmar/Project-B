using DataAccess;
using DataModels;
using System.Collections.Generic;
using System.Linq;
public class AboutUsPres
{
    private static List<FlightModel> allFlights;
    public static void aboutUsMenu()
    {

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("✈️  Welcome to BOSST Airlines! ✈️");
            Console.ResetColor();
            Console.WriteLine("Get to know more about us through the options below:");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("1. 🌍 Network");
            Console.WriteLine("2. 🛩️  Our Fleet");
            Console.WriteLine("3. ⭐ What We Stand For");
            Console.WriteLine("4. 🔒 Personal Data");
            Console.WriteLine("5. 📞 Contact Information");
            Console.WriteLine("6. 🔙 Return to front page");
            Console.WriteLine("---------------------------------------------------");
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                allFlights = FlightsAccess.ReadAll();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("🌍 Our Network");
                Console.ResetColor();
                Console.WriteLine("Here at BOSST Airlines, we have a vast network of destinations.");
                var allDestinations = allFlights.Select(flight => flight.ArrivalDestination).Distinct().ToList();

                Console.WriteLine("Our destinations include:");
                foreach (var destination in allDestinations)
                {
                    Console.WriteLine($"- {destination}");
                }
            }
            else if (input == "2")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("🛩️ Our Fleet");
                Console.ResetColor();
                Console.WriteLine("Our fleet consists of state-of-the-art aircraft:");
                Console.WriteLine("Airbus A330 and Boeing 787, ensuring a comfortable and enjoyable journey. 🛫");
            }
            else if (input == "3")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⭐ What We Stand For");
                Console.ResetColor();
                Console.WriteLine("At BOSST Airlines, our customers always come first!");
                Console.WriteLine("We strive to make travel affordable and enjoyable, providing top-tier service to connect people to destinations around the globe.");
            }
            else if (input == "4")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("🔒 Personal Data");
                Console.ResetColor();
                Console.WriteLine("We value your privacy and only share your data with trusted partners when necessary.");
                Console.WriteLine("Your information is handled with the utmost care to provide the best possible service.");
            }
            else if (input == "5")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("📞 Contact Information");
                Console.ResetColor();
                Console.WriteLine("📍 Headquarters: Wijnhaven 107, Rotterdam");
                Console.WriteLine("✉️  Email: BOSST@gmail.com");
                Console.WriteLine("📱 Phone: 0612345678");
            }
            else if (input == "6")
            {
                // MenuPresentation.PressAnyKey();
                MenuLogic.PopMenu();
                return;
            }
            else
            {
                // Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n⚠️  Invalid input! Please try again.");
                Console.ResetColor();
            }

            MenuPresentation.PressAnyKey();
        }

    }
}