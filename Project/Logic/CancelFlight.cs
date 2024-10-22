// As a user i want a section where i can see my current booked flights
// As a user i want to be able to cancel the tickets i booked
// As a user I want to see which flights are canceled
using Newtonsoft.Json;
public class CancelFlight

{
   
    // list to store the booked flights and the cancelled flights the user has 
    public List<FlightModel> Booked = new List<FlightModel>();
    public List<FlightModel> Cancelled = new List<FlightModel>();
    private string FileName= "flights.json";

    // constructor 
    public CancelFlight()
    {
        Readfromjson();
    }


    // read from json 
    public void Readfromjson()
    {
        if (File.Exists(FileName))
        {
            StreamReader reader = new StreamReader(FileName);
            string File2Json= reader.ReadToEnd();
            Booked = JsonConvert.DeserializeObject<List<FlightModel>>(File2Json);
        }


    }

    // update to json 
    public void UpdateJson()
    {
        string json = JsonConvert.SerializeObject(Booked, Formatting.Indented);
        StreamWriter writer= new StreamWriter(FileName);
        writer.Write(json);
        writer.Close();

    }

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
            // loop thru flights in booked list
            foreach (var flight in Booked)
            {
                // flight details 
                flightsstr+= $"Flight:{flight.Airline} Ticket Price: {flight.TicketPrice:C}, Gate: {flight.Gate}, Airport: {flight.Airport} ";
            } 
            return flightsstr;
        }

    }

    public string CancelFlights(int index)
    {
        // cancels flight based on given index 
        if (index<0 || index >=Booked.Count)
        {
            return$"Incorrect index";
        }
        

        Booked[index].IsCancelled = true;
        // add flight to cancelled list 
        Cancelled.Add(Booked[index]);
        string result = $"Cancelled: Flight {Booked[index].Airline}, Ticket Price: {Booked[index].TicketPrice:C}, Gate: {Booked[index].Gate}, Airport: {Booked[index].Airport}, Cancelled: {Booked[index].IsCancelled}";
        Booked.RemoveAt(index);
        // update the changes to the json file 
        UpdateJson();
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