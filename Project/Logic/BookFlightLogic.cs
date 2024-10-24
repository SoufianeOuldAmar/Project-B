public static class BookFlightLogic
{
    // Method to search for a flight by its ID
    public static FlightModel SearchFlightByID(int id)
    {
        // Retrieve all flights
        var allFlights = BookFlightPresentation.allFlights;

        // Return the flight that matches the given ID, or null if not found
        return allFlights.FirstOrDefault(flight => flight.Id == id);
    }

    // public static void CancelBookedFlight(Dictionary<string, List<BookedFlightsModel>> allBookedFlights)
    // {
    //     Console.WriteLine("WAAAAAAAAAAAAAAAAAAAAAA");
    //     Thread.Sleep(1000);
    //     // Create a list to hold the keys of users whose flights will be modified
    //     var usersToUpdate = new List<string>();
    //     int index = 0;

    //     // Iterate through the dictionary of all booked flights
    //     foreach (var entry in allBookedFlights)
    //     {
    //         // Filter out only the flights that are cancelled
    //         var cancelledFlights = entry.Value.Where(flight => flight.IsCancelled).ToList();
    //         foreach (var item in cancelledFlights)
    //         {
    //             usersToUpdate.Add(item.BookedSeats[index]);
    //             index++;
    //         }
    //     }


    //     UpdateBookedSeats(allBookedFlights, usersToUpdate);

    //     // Remove users who no longer have any booked flights
    //     foreach (var user in usersToUpdate)
    //     {
    //         allBookedFlights.Remove(user);
    //     }

    //     Console.WriteLine("Cancelled flights have been removed.");
    // }

    // public static void UpdateBookedSeats(Dictionary<string, List<BookedFlightsModel>> allBookedFlights, List<string> usersToUpdate)
    // {
    //     Console.WriteLine("NOG EEN WAAAAAAAAAAAAAAAAAAAAA");
    //     Thread.Sleep(1000);
    //     // Get all flights from the JSON file
    //     var allFlights = FlightsAccess.ReadAll();

    //     // Loop through the users who need seat updates
    //     if (usersToUpdate.Count == 0)
    //     {
    //         Console.WriteLine("YOOOOOO LEEG");
    //         Thread.Sleep(1000);
    //     }
    //     else
    //     {
    //         Console.WriteLine("YOOOOOOOO NIET LEEG");
    //         Thread.Sleep(1000);
    //     }

    //     foreach (var user in usersToUpdate)
    //     {
    //         var userFlights = allBookedFlights[user];
    //         foreach (var bookedFlight in userFlights)
    //         {
    //             UpdateSeatsForFlight(bookedFlight, allFlights); // Pass allFlights to update the seats
    //         }
    //     }

    //     // After updating the seats, write the updated flights back to the JSON file
    // }

    // private static void UpdateSeatsForFlight(BookedFlightsModel bookedFlight, List<FlightModel> allFlights)
    // {
    //     Console.WriteLine("NOG EEN NOG EEN WAAAAAAAAAAAAAAAAAAAAA");
    //     Thread.Sleep(1000);

    //     foreach (var seat in bookedFlight.BookedSeats)
    //     {
    //         UpdateFlightSeats(seat, allFlights);
    //     }
    // }

    // private static void UpdateFlightSeats(string seat, List<FlightModel> allFlights)
    // {
    //     foreach (var flight in allFlights)
    //     {
    //         if (flight.Layout.BookedSeats.Contains(seat))
    //         {
    //             Console.WriteLine(seat + "POEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEP");
    //             Thread.Sleep(1000);
    //             // Remove seat from booked and add it back to available
    //             flight.Layout.BookedSeats.Remove(seat);
    //             flight.Layout.AvailableSeats.Add(seat);  // Move the seat back to available
    //         }
    //         else
    //         {
    //             Console.WriteLine(seat + "MISLUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUKT");
    //             Thread.Sleep(1000);
    //         }
    //     }

    //     FlightsAccess.WriteAll(allFlights);
    // }



}
