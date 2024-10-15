// As a user i want a section where i can see my current booked flights
// As a user i want to be able to cancel the tickets i booked
// As a user I want to see which flights are canceled
public class CancelFlight

{

    // fields 
    
    // list to store the booked flights and the cancelled flights the user has 
    public List<FlightModel> Booked = new List<Flightmodel>();
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

    public void CancelFlights(string airline)
    {
        // get flight by looping through booked list
        for (int i=0; i<Booked.Count; i++ )
        {
            if(Booked[i].Airline== airline)
            {
                Booked[i].IsCancelled= true;
                // add to cancelled list 
                Cancelled.Add(Booked[i]);

                Booked.RemoveAt(i);
                return $"Flight:{Booked[i].Airline} Ticket Price: {Booked[i].TicketPrice:C}, Gate: {Booked[i].Gate}, Airport: {Booked[i].Airport}, Cancelled: {Booked[i].IsCancelled} ";
            } 
        }
            

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