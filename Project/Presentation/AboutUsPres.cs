using DataAccess;
using DataModels;
using System.Collections.Generic;
using System.Linq;
public class AboutUsPres
{
    private static List<FlightModel> allFlights;
    public static void Main()
    {
        

        Console.WriteLine("Welcome, get to know more about us");
        Console.WriteLine("(1) Network (2) Our Fleet (3) What we stand for (4) Personal Data ");
        string Input = Console.ReadLine();

        if (Input == "1")
        {
            allFlights = FlightsAccess.ReadAll(); 
            Console.WriteLine("Here in  BOSST Airlines we have a large network");
            var allDestinations = allFlights.Select(flight => flight.ArrivalDestination).Distinct().ToList(); // removes duplicates 

            Console.WriteLine("Our destinations include: ");
            foreach(var destination in allDestinations)
            {
                Console.WriteLine(destination);
            }
        }
        else if (Input == "2")
        {
            Console.WriteLine("Our fleet consist of AIRBUS,  ensuring you have a good and comfortable time 🛫 ");
        }
        else if (Input == "3")
        {
            Console.WriteLine("We stand for affordable but");
        }
        else 
        {
            Console.WriteLine("Invalid input,please re-enter.");
        }





    }
  










}