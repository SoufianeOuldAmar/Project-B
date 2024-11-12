using System.Text.Json;
public class Reschedule : CancelLogic
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
    public string BookedFlights(string email)
    {
        return base.BookedFlights(email);
    }

    public string AvailableSeats(LayoutModel flight)
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

    public string RescheduleFlight(string email, int currentId, int newId)
    {
        if (!BookFlightPresentation.allBookedFlights.ContainsKey(email))
        {
            return $"No booked flights for {email}.";
        }

        // Assuming the structure is different and you use "BookingModel" or another class
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
    public string Policy()
    {
        return "Cancellation policy: You can cancel your flight at any time before departure for a refund, except for the non-refundable fee.\n" +
               "Rescheduling policy: Reschedule your flight with a €50 fee, plus any price difference for the new flight.";
    }
}
