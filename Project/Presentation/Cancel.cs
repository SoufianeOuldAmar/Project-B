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

                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. See booked flights");
                Console.WriteLine("2. Cancel a flight");
                Console.WriteLine("3. See cancelled flights");
                Console.WriteLine("Q. Quit");

                string input = Console.ReadLine().ToUpper(); 

                if (input == "Q")
                {
                    Console.WriteLine("Quitting the program");
                    break; 
                }
                if (input== "1")
                {
                    Console.WriteLine("Booked Flights:");
                    Console.WriteLine(cancel.BookedFlights());
                }

                else if (input == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("Enter the index of the flight you want to cancel:");

                        string indexinput = Console.ReadLine();

                        int index = Convert.ToInt32(indexinput);

                        // invalid index 
                        if (index < 0 || index >= cancel.Booked.Count)
                        {
                            Console.WriteLine("Invalid index. Please re-enter index");
                            continue;
                        }
                        
                        
                        cancel.CancelFlights(index); 
                        Console.WriteLine("Flight succesfully cancelled");
                        break;
                    }

                }
                else if (input== "3")
                {
                    string cancelled = cancel.CancelledOverview();
                    if (cancelled.Length == 0)
                    {
                        Console.WriteLine("No cancelled flights.");
                    }
                    else
                    {
                        Console.WriteLine(cancelled);
                    }

                }
                else 
                {
                    Console.WriteLine("Invalid input");
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