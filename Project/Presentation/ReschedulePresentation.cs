using System.Net;
using System.Text;
using System.Text.Json;
using DataModels;
using DataAccess;
using System.Security.Cryptography.X509Certificates;
public static class ReschedulePresentation
{
    public static string fileName = "DataSources/flights.json";

    public static List<BookedFlightsModel> BookedFlightsUser(string email)
    {

        if (!BookFlightPresentation.allBookedFlights.ContainsKey(email))
        {
            return new List<BookedFlightsModel>();
        }

        return BookFlightPresentation.allBookedFlights[email];

    }

    public static string RescheduleFlight(string email, int bookedFlightID, int? newFlightInput = null)
    {
        var retrieveBookFlights = BookedFlightsUser(email);

        if (retrieveBookFlights.Count == 0)
        {
            return $"No booked flights for {email}";
        }

        // get from BookflightModel
        var specificFlight = retrieveBookFlights.FirstOrDefault(x => x.FlightID == bookedFlightID);
        if (specificFlight == null)
        {
            return $"No booked flights with this ID: {bookedFlightID}";
        }

        var GetFlightDetails = BookFlightPresentation.allFlights.FirstOrDefault(x => x.Id == specificFlight.FlightID);

        if (GetFlightDetails == null)
        {
            return $"Can't find a flight with the ID {bookedFlightID}.";
        }

        string currentDepartureAirport = GetFlightDetails.DepartureAirport;
        string currentArrivalDestination = GetFlightDetails.ArrivalDestination;


        if (newFlightInput == null)
        {
            // use searchFlights without already booked flights 
            var availableFlights = BookFlightLogic.SearchFlights(currentDepartureAirport, currentArrivalDestination).Where(x => x.Id != bookedFlightID).ToList();

            if (availableFlights.Count == 0)
            {
                return $"No available flights from {currentDepartureAirport} to {currentArrivalDestination}.";
            }

            Console.Clear();

            StringBuilder availableFlightsDetails = new StringBuilder("Available flights to reschedule to:\n");
            foreach (var flight in availableFlights)
            {
                availableFlightsDetails.AppendLine($"Flight ID: {flight.Id}, Date: {flight.DepartureDate}, Time: {flight.FlightTime}, Price: {flight.TicketPrice:C}");
            }
            return availableFlightsDetails.ToString();

        }
        // user cant choose ids outside of list 
        var availableFlightsToReschedule = BookFlightLogic.SearchFlights(currentDepartureAirport, currentArrivalDestination).Where(x => x.Id != bookedFlightID).Select(flight => flight.Id).ToList();
        if (!availableFlightsToReschedule.Contains(newFlightInput.Value))
        {
            return $"Selected flight with ID {newFlightInput.Value} is not in the list of available flights.";
        }


        var newFlightIdGiven = BookFlightPresentation.allFlights.FirstOrDefault(x => x.Id == newFlightInput.Value);
        if (newFlightIdGiven == null)
        {
            return $"Selected flight with ID {newFlightInput.Value} not found.";
        }

        if (newFlightIdGiven.AvailableSeats <= 0)
        {

            return $"Selected flight with ID {newFlightInput.Value} has no available seats.";
        }

        double priceDifference = (double)(newFlightIdGiven.TicketPrice - GetFlightDetails.TicketPrice);
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

        double totalNewPrice = (double)newFlightIdGiven.TicketPrice + totalFee;


        // update 
        specificFlight.FlightID = newFlightIdGiven.Id;
        BookFlightPresentation.allBookedFlights[email] = retrieveBookFlights;
        DataManagerLogic.Save<FlightModel>("DataSources/flights.json", BookFlightPresentation.allFlights);


        totalFee = Math.Round(totalFee, 2);

        // update the fee to users account
        List<UserAccountModel> userAccounts = DataManagerLogic.GetAll<UserAccountModel>("DataSources/useraccounts.json");
        UserAccountModel account = userAccounts.FirstOrDefault(x => x.EmailAddress == email);
        if (account != null)
        {
            account.Fee = totalFee; DataManagerLogic.Save<UserAccountModel>("DataSources/useraccounts.json", userAccounts);

        }
        else
        {
            return $"Account with email {email} not found.";
        }

        DataManagerLogic.WriteAll(email, BookFlightPresentation.allBookedFlights[email]);
        return $"Flight successfully rescheduled. Additional fee: {totalFee:C} and new Price {totalNewPrice:C}. New Flight Date: {newFlightIdGiven.DepartureDate} at {newFlightIdGiven.FlightTime}.";
    }




    // Show / Review Policy
    public static string Policy()
    {
        return "Cancellation policy: Tickets are non-refundable \n" +
               "Rescheduling policy: Reschedule your flight with a €50 fee, plus any price difference for the new flight.";
    }
}
