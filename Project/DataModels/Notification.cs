using System.Globalization;

public class Notification : IDataModel
{
    public int Id { get; set; }
    public int FlightID { get; set; }
    public string NewStatus { get; set; }
    public string Description { get; set; }
    public string OldDetails { get; set; }
    public string NewDetails { get; set; }
    public bool IsRead { get; set; }
    public bool ActionRequired { get; set; }

    public Notification(int id, int flightID, string description, string oldDetails, string newDetails)
    {
        Id = id;
        FlightID = flightID;
        Description = description;
        OldDetails = oldDetails;
        NewDetails = newDetails;
        IsRead = false;
        ActionRequired = false;
    }
}
