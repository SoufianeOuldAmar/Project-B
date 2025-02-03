using DataAccess;
using DataModels;
using System.Collections.Generic;
using System.Linq;
public class AboutUsPres
{
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

            string[] aboutUsMenuOptions = 
            {
                "🌍 Network",
                "🛩️  Our Fleet",
                "⭐ What We Stand For",
                "🔒 Personal Data",
                "📞 Contact Information",
                "🔙 Return to front page"
            };


            for (int i = 0; i < aboutUsMenuOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {aboutUsMenuOptions[i]}");
            }
            Console.WriteLine("---------------------------------------------------");

            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

 
            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 1: 
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("🌍 Our Network");
                        Console.ResetColor();
                        Console.WriteLine("Here at BOSST Airlines, we have a vast network of destinations.");
                        var allDestinations = FlightLogic.GetAllDestinations();
                        Console.WriteLine("Our destinations include:");
                        foreach (var destination in allDestinations)
                        {
                            Console.WriteLine($"- {destination}");
                        }
                        break;

                    case 2: 
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("🛩️ Our Fleet");
                        Console.ResetColor();
                        Console.WriteLine("Our fleet consists of state-of-the-art aircraft:");
                        Console.WriteLine("Airbus A330, Boeing 787 and Boeing 737, ensuring a comfortable and enjoyable journey. 🛫");
                        break;

                    case 3: 
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("⭐ What We Stand For");
                        Console.ResetColor();
                        Console.WriteLine("At BOSST Airlines, our customers always come first!");
                        Console.WriteLine("We strive to make travel affordable and enjoyable, providing top-tier service to connect people to destinations around the globe.");
                        break;

                    case 4: 
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("🔒 Personal Data");
                        Console.ResetColor();
                        Console.WriteLine("We value your privacy and only share your data with trusted partners when necessary.");
                        Console.WriteLine("Your information is handled with the utmost care to provide the best possible service.");
                        break;

                    case 5: 
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("📞 Contact Information");
                        Console.ResetColor();
                        Console.WriteLine("📍 Headquarters: Wijnhaven 107, Rotterdam");
                        Console.WriteLine("✉️  Email: BOSST@gmail.com");
                        Console.WriteLine("📱 Phone: 0612345678");
                        break;

                    case 6: 
                        MenuLogic.PopMenu();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n⚠️  Invalid option! Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
            else
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n⚠️  Invalid input! Please enter a number.");
                Console.ResetColor();
            }


            MenuPresentation.PressAnyKey();
        }

    }
}