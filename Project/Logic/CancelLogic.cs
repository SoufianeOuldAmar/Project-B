using System.Text.Json;
using DataModels;
using System.Text;
using DataAccess;

public static class CancelLogic
{
    public static string fileName = "DataSources/flights.json";
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

        Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();

        BookFlightPresentation.allBookedFlights = allBookedFlights;

        string FlightDetails = "";
        foreach (var flight in BookFlightPresentation.allBookedFlights[email])
        {
            // find flight by ID in Allflights list of BookFlightPresentation
            var neededflight = BookFlightPresentation.allFlights.Find(x => x.Id == flight.FlightID);

            // string returnFlightAvailable = neededflight.ReturnFlight != null ? "Yes" : "No";
            double totalPetFee= 0;
            double totalBaggageFee= 0;

            // calculate total pet fee
            if (flight.Pets != null && flight.Pets.Count > 0)
            {
                foreach (var pet in flight.Pets)
                {
                    totalPetFee += pet.Fee;
                }
            }
            if (flight.BaggageInfo != null && flight.BaggageInfo.Count > 0)
            {
                foreach (var baggage in flight.BaggageInfo)
                {
                    totalBaggageFee += baggage.Fee;
                }
            }

            // if flight is found by id 
            if (neededflight != null)
            {
                // FlightDetails += $"Flight ID: {neededflight.Id}, Airline: {neededflight.Airline}, Departure Airport: {BookFlightLogic.SearchFlightByID(neededflight.Id).DepartureAirport}, Arrival Destination: {BookFlightLogic.SearchFlightByID(neededflight.Id).ArrivalDestination}, Ticket Price: {neededflight.TicketPrice:C}, Return Flight: {returnFlightAvailable}, Cancelled: {flight.IsCancelled}\n"; // cancelled is directly accessed from model class
                double totalTicketPrice = neededflight.TicketPrice + totalPetFee + totalBaggageFee;

                FlightDetails += $"Flight ID: {neededflight.Id}, Airline: {neededflight.Airline}, Date: {neededflight.DepartureDate}, Time: {neededflight.FlightTime} , Departure Airport: {BookFlightLogic.SearchFlightByID(neededflight.Id).DepartureAirport}, Arrival Destination: {BookFlightLogic.SearchFlightByID(neededflight.Id).ArrivalDestination}, Ticket Price: {totalTicketPrice:C}, Cancelled: {flight.IsCancelled}\n"; // cancelled is directly accessed from model class
            }
            // pets 
            if (flight.Pets != null && flight.Pets.Count > 0)
            {
                FlightDetails += $"  Pets on this flight:\n";
                foreach (var pet in flight.Pets)
                {
                    FlightDetails += $" Animal: {pet.AnimalType}, Fee: {pet.Fee:C}\n";
                }
            }
            else
            {
                FlightDetails += $"  No pets booked for this flight.\n";
            }
            // baggage 
            if (flight.BaggageInfo != null && flight.BaggageInfo.Count > 0)
            {
                FlightDetails += $"  Baggage Details:\n";
                foreach (var baggage in flight.BaggageInfo)
                {

                    int carryOnCount = 0;
                    int checkedCount = 0;
                    if (baggage.BaggageType == "checked")
                    {
                        checkedCount++;
                    }
                    else if (baggage.BaggageType == "carry-on")
                    {
                        carryOnCount++;
                    }

                    FlightDetails += $" Baggage " + $"Weight: {baggage.BaggageWeight}kg, " + $"Fee: {baggage.Fee:C}\n";
                }
            }
            else
            {
                FlightDetails += $"  No baggage added for this flight.\n";
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
        FlightsAccess.WriteAll(allFlights);
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
