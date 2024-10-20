// As a user i want a section where i can see my current booked flights
// As a user i want to be able to cancel the tickets i booked
// As a user I want to see which flights are canceled
public class CancelFlight

{
   
    // list to store the booked flights and the cancelled flights the user has 
    public List<FlightModel> Booked = new List<FlightModel>();
    public List<FlightModel> Cancelled = new List<FlightModel>();

    // methods 
    public string BookedFlights()
    {
        if (Booked.Count==0)
        {
            return null;
        }
        else
        {
            string flightsstr= "";
            foreach (var flight in Booked)
            {
                //acessing the properties of flightmodel class 
                flightsstr+= $"Flight:{flight.Airline} Ticket Price: {flight.TicketPrice:C}, Gate: {flight.Gate}, Airport: {flight.Airport} ";
            } 
            return flightsstr;
        }

    }

    public string CancelFlights(int index)
    {
        if (index<0 || index >=Booked.Count)
        {
            return$"Incorrect index";
        }
            // see if i matches with index user input 

        Booked[index].IsCancelled = true;
        Cancelled.Add(Booked[index]);
        string result = $"Cancelled: Flight {Booked[index].Airline}, Ticket Price: {Booked[index].TicketPrice:C}, Gate: {Booked[index].Gate}, Airport: {Booked[index].Airport}, Cancelled: {Booked[index].IsCancelled}";
        Booked.RemoveAt(index);
        return result;
  

    }

    public string CancelledOverview()
    {
        if (Cancelled.Count== 0)
        {
            return null;
        }
        else 
        {
            // loop thru cancelled list 
            string cancelledstr= "";
            foreach(var flights in Cancelled)
            {
                cancelledstr+= $"Airline: {flights.Airline}, Ticket Price: {flights.TicketPrice:C}, Gate: {flights.Gate}, Airport: {flights.Airport}, Cancelled: {flights.IsCancelled}\n";
            }
            return cancelledstr;
        }
    }

}