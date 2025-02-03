public class Payment : IDataModel
{
    public int Id { get; }
    public string ItemType { get; set; }
    public double Amount { get; set; }

    private DateTime _datePayment { get; set; }

    public DateTime DatePayment
    {
        get
        {
            return _datePayment;
        }
        set
        {

            if (DateTime.TryParseExact(value.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                _datePayment = parsedDate; // Set the _datePayment to the parsed date
            }
            else
            {
                throw new ArgumentException("Invalid date format. Please use dd-MM-yyyy.");
            }
        }
    }


    public Payment(int id, string itemType, double amount, DateTime datePayment)
    {   
        Id = id;
        ItemType = itemType;
        Amount = amount;
        _datePayment = datePayment;
    }
}