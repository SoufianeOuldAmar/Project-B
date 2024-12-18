public class Notification
{
    public int Id { get; set; }
    public string FlightID { get; set; }
    public string NewStatus { get; set; }
    public string ActionRequired { get; set; }
    public string OldDetails { get; set; }
    public string NewDetails { get; set; }
    public bool IsRead { get; set; }
}
