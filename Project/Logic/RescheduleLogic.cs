using System.Net;
using System.Text.Json;
using DataModels;
public static class RescheduleLogic
{
    public static string fileName = "flights.json";
    public static void WriteJson(string fileName, List<FlightModel> allFlights)
    {
        string json = JsonSerializer.Serialize(allFlights, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public static List<FlightModel> LoadFlights(string fileName)
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);

            List<FlightModel> flights = JsonSerializer.Deserialize<List<FlightModel>>(json);


            if (flights != null)
            {
                return flights;
            }
            else
            {
                return new List<FlightModel>();
            }
        }
        else
        {
            return new List<FlightModel>();
        }
    }

    // See booked flights
    public static string BookedFlights(string email)
    {
        if (!BookFlightPresentation.allBookedFlights.ContainsKey(email))
        {
            return $"You have no flights booked";
        }
        if (BookFlightPresentation.allBookedFlights[email].Count == 0)
        {
            return $"You have no  flights booked";
        }
        
        string FlightDetails = "";
        foreach(var flight in BookFlightPresentation.allBookedFlights[email])
        {

            var neededflight = BookFlightPresentation.allFlights.Find(x => x.Id == flight.FlightID);
            // if flight not found by id 
            if (neededflight!= null)
            {
                FlightDetails+= $"Flight ID: {neededflight.Id}, Airline: {neededflight.Airline}, Ticket Price: {neededflight.TicketPrice:C}, Cancelled: {flight.IsCancelled}\n"; // cancelled is directly accessed from model class
            }
        }
        return FlightDetails;
    }

 

    public static string RescheduleFlight(string email, string currentDepartureAirport, string currentArrivalAirport, string newDepartureAirport, string newArrivalDestination)
    {
        // Check if the user has any bookings
        if (!BookFlightPresentation.allBookedFlights.ContainsKey(email))
        {
            return $"No booked flights for {email}.";
        }
        // Get the user's bookings from the list 
        var userBookings = BookFlightPresentation.allBookedFlights[email];
         // Find the specific booked flight matching the departure and arrival locations
        var bookedFlight = userBookings.Find(x => BookFlightLogic.SearchFlightByID(x.FlightID).DepartureAirport == currentDepartureAirport && BookFlightLogic.SearchFlightByID(x.FlightID).ArrivalDestination== currentArrivalAirport);
         // If the flight is not found, return null
        if (bookedFlight == null)
        {
            return $"Flight from {currentDepartureAirport} to {currentArrivalAirport} not found in bookings.";
        }
        // Find the new flight (use the FlightID from the booked flight) 
        //  Retrieve the full flight details (FlightModel) using the FlightID from the user's booked flight.
        var newFlight = BookFlightLogic.SearchFlightByID(bookedFlight.FlightID);

        // Check if there are available seats on the new flight
        if (BookFlightLogic.GetAvailableSeatsCount(newFlight) <= 0)
        {
            return "No available seats on the new flight.";
        }

        // Check if the new flight is cancelled if yes return message 
        if (newFlight.IsCancelled)
        {
            return "The new flight is cancelled and can't be booked.";
        }

        if (newFlight == null)
        {
            return "No matching new flight found from the selected departure to arrival airports.";
        }
        // Check if the new flight's date and time are in the future
        DateTime departureDateTime = DateTime.Parse($"{newFlight.DepartureDate} {newFlight.FlightTime}");
        if (departureDateTime <= DateTime.Now)
        {
            return "Incorrect date.";
        }

        // Retrieve the full FlightModel for the booked flight to access TicketPrice
        var retrieveTicketprice = BookFlightLogic.SearchFlightByID(bookedFlight.FlightID);



        // Calculate the fee and price difference for the rescheduling
        double priceDifference = (double)(newFlight.TicketPrice - retrieveTicketprice.TicketPrice);
        double fee50 = 50.00;  // Rescheduling fee
        double totalFee;

        if (priceDifference > 0)
        {
            totalFee = priceDifference + fee50;
        }
        else
        {
            totalFee = fee50;
        }


        // update the details to match the new one 
        newFlight.DepartureAirport = newFlight.DepartureAirport;
        newFlight.ArrivalDestination = newFlight.ArrivalDestination;
        newFlight.DepartureDate = newFlight.DepartureDate;
        newFlight.FlightTime = newFlight.FlightTime;
        // Save the updated flights list
        WriteJson(fileName, BookFlightPresentation.allFlights);

        return $"Flight successfully rescheduled. Additional fee: {totalFee:C}. New Flight Date: {newFlight.DepartureDate} at {newFlight.FlightTime}.";
    }


    // Show / Review Policy
    public static string Policy()
    {
        return "Cancellation policy: You can cancel your flight 24h before departure for a refund.\n" +
               "Rescheduling policy: Reschedule your flight with a €50 fee, plus any price difference for the new flight.";
    }


}
