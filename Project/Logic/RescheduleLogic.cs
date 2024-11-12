using System.Net;
using System.Text.Json;
public static class Reschedule 
{
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
            return $"You have no flights booked";
        }
        
        string FlightDetails = "";
        foreach(var flight in BookFlightPresentation.allBookedFlights[email])
        {
            // find flight by ID in Allflights list of BookFlightPresentation
            var neededflight = BookFlightPresentation.allFlights.Find(x => x.Id == flight.FlightID);
            // if flight not found by id 
            if (neededflight!= null)
            {
                FlightDetails+= $"Flight ID: {neededflight.Id}, Airline: {neededflight.Airline}, Ticket Price: {neededflight.TicketPrice:C}, Cancelled: {flight.IsCancelled}\n"; // cancelled is directly accessed from model class
            }
        }
        return FlightDetails;
    }

    public static string AvailableSeats(LayoutModel flight)
    {
        List<string> availableSeats = flight.AvailableSeats;


        if (availableSeats.Count > 0)
        {
            return string.Join(", ", availableSeats);
        }
        else
        {
            return null;
        }
    }

    public static string RescheduleFlight(string email, int currentId, int newId)
    {
        if (!BookFlightPresentation.allBookedFlights.ContainsKey(email))
        {
            return $"No booked flights for {email}.";
        }


        var userBookings = BookFlightPresentation.allBookedFlights[email];
        var bookedFlight = userBookings.Find(x => x.FlightID == currentId);
        if (bookedFlight == null)
        {
            return $"Flight {currentId} not found in bookings.";
        }

        // Find the current and new flight details
        var currentFlight = BookFlightPresentation.allFlights.FirstOrDefault(x => x.Id == currentId);
        var newFlight = BookFlightPresentation.allFlights.FirstOrDefault(x => x.Id == newId);


        if (newFlight.IsCancelled)
        {
            return "The new flight is cancelled and can't be booked.";
        }

        // Price difference calculation
        double priceDifference = (double)(newFlight.TicketPrice - currentFlight.TicketPrice);
        double fee50 = 50.00;
        double totalFee;

        if (priceDifference > 0)
        {
            totalFee = priceDifference + fee50;
        }
        else
        {
            totalFee = fee50;
        }

        bookedFlight.FlightID = newFlight.Id;  // Update the flight ID to the new one
        bookedFlight.IsCancelled = false;


        WriteJson(fileName, BookFlightPresentation.allFlights);

        return $"Flight successfully rescheduled. Additional fee: {totalFee:C}. " +
               $"New Flight Date: {newFlight.DepartureDate} at {newFlight.FlightTime}.";
    }

    // Show / Review Policy
    public static string Policy()
    {
        return "Cancellation policy: You can cancel your flight at any time before departure for a refund, except for the non-refundable fee.\n" +
               "Rescheduling policy: Reschedule your flight with a €50 fee, plus any price difference for the new flight.";
    }
}
