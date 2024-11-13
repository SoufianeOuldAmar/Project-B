using System.Text.Json;
using DataModels;
public static class CancelLogic
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
            // Deserialize the JSON content into a list of FlightModel objects
            return JsonSerializer.Deserialize<List<FlightModel>>(json) ?? new List<FlightModel>();
        }
        return new List<FlightModel>();
    }

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
        foreach (var flight in BookFlightPresentation.allBookedFlights[email])
        {
            // find flight by ID in Allflights list of BookFlightPresentation
            var neededflight = BookFlightPresentation.allFlights.Find(x => x.Id == flight.FlightID);

            // if flight not found by id 
            if (neededflight != null)
            {
                FlightDetails += $"Flight ID: {neededflight.Id}, Airline: {neededflight.Airline}, Departure Airport: {BookFlightLogic.SearchFlightByID(neededflight.Id).DepartureAirport}, Arrival Destination: {BookFlightLogic.SearchFlightByID(neededflight.Id).ArrivalDestination}, Ticket Price: {neededflight.TicketPrice:C}, Cancelled: {flight.IsCancelled}\n"; // cancelled is directly accessed from model class
            }

        }
        return FlightDetails;
    }

    public static string CancelFlights(string email, int id)
    {
        if (!BookFlightPresentation.allBookedFlights.ContainsKey(email))
        {
            return "No booked flights found for this user.";
        }

        // get user's booked flights 
        var flightt = BookFlightPresentation.allBookedFlights[email];
        var BookedFlightId = flightt.Find(x => x.FlightID == id);

        if (BookedFlightId == null)
        {
            return $"Flight by id not found";
        }
        if (BookedFlightId.IsCancelled)
        {
            return $"You have already cancelled the flight";
        }

        // set cancelled as true 
        BookedFlightId.IsCancelled = true;

        // load from json 
        List<FlightModel> allFlights = LoadFlights(fileName);

        var flightToCancel = allFlights.Find(x => x.Id == id);

        if (flightToCancel != null)
        {
            // set to true aka cancelled 
            flightToCancel.IsCancelled = true;
            allFlights.Remove(flightToCancel);
        }
        // save changes to json 
        WriteJson(fileName, allFlights);
        return "Flight is cancelled";
    }

    public static string CancelledOverview(string email)
    {
        if (!BookFlightPresentation.allBookedFlights.ContainsKey(email))
        {
            return "No flights found for this user.";
        }
        string cancelled = "";
        foreach (var flight in BookFlightPresentation.allBookedFlights[email])
        {
            if (flight.IsCancelled)
            {
                var flightDetails = BookFlightPresentation.allFlights.FirstOrDefault(x => x.Id == flight.FlightID);
                if (flightDetails != null)
                {
                    cancelled += $"Cancelled Flight: Airline: {flightDetails.Airline}, Ticket Price: {flightDetails.TicketPrice:C}\n";
                }
            }
        }
        return cancelled;
    }
}
