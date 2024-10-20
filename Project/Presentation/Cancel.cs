using System;

public static class Cancel 
{
    public static void Main()
    {
        CancelFlight cancel = new CancelFlight();

        while (true)
        {
            if (cancel.Booked.Count > 0) 
            {
                Console.WriteLine("Booked Flights:");
                Console.WriteLine(cancel.BookedFlights()); 

                Console.WriteLine("Enter 'C' to cancel a flight or 'Q' to quit:");
                string input = Console.ReadLine().ToUpper(); 

                if (input == "Q")
                {
                    Console.WriteLine("Quitting the program");
                    break; 
                }

                if (input == "C")
                {
                    Console.WriteLine("Enter the index of the flight you want to cancel:");

                    string indexinput = Console.ReadLine();

                    int index = Convert.ToInt32(indexinput);
                        
                    cancel.CancelFlights(index); 

                }
            }
            else 
            {
                Console.WriteLine("You have no flights booked.");
                break; 
            }
        }
    }
}