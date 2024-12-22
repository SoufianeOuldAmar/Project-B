public class Notification
{
    public int Id { get; set; }
    public int FlightID { get; set; }
    public string NewStatus { get; set; }
    public string ActionRequired { get; set; }
    public string OldDetails { get; set; }
    public string NewDetails { get; set; }
    public bool IsRead { get; set; }

    public Notification(int id, int flightID, string actionRequired, string oldDetails, string newDetails)
    {
        Id = id;
        FlightID = flightID;
        ActionRequired = actionRequired;
        OldDetails = oldDetails;
        NewDetails = newDetails;
        IsRead = false;
    }
}
