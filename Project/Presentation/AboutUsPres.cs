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
            Console.WriteLine("Welcome :) get to know more about us");
            Console.WriteLine("(1) Network (2) Our Fleet (3) What we stand for (4) Personal Data (5) Contact information (6) Quit program");
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
                Console.WriteLine("Our fleet consist of AirbusA330 and Boeing787 ensuring you have a good and comfortable time traveling with us 🛫 ");
            }
            else if (Input == "3")
            {
                Console.WriteLine("We are a leading airline company that puts their costumers at number one. ");
                Console.WriteLine("Here at BOSST airlines we believe for making travel affordable for everyone and have a pleasant flying experience at the same time, that's why we made it our goal to get people to different destinations all around the world while providing amazing service.");
            }
            else if (Input == "4")
            {
                Console.WriteLine("Here at BOSST Airlines we care about your privacy that's why we only share your information with trusted third-party partners when necessary to provide you with the best possible service ");
            }
            else if (Input == "5")
            {
                Console.WriteLine(" Our headquarters are located at Wijnhaven 107, Rotterdam. You may contact us by email or phone for questions and help at BOSST@gmail.com and 0612345678");
            }
            else if (Input == "6")
            {
                Console.WriteLine("Return to main menu...");
                MenuLogic.PopMenu(); 
                return;
            }
            else 
            {
                Console.WriteLine("Invalid input,please re-enter.");
            }

        } 





    }
  










}