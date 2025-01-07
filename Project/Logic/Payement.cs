public class Payement 
{
    public string ItemType {get; set;}
    public double Amount{ get; set;}

    private DateTime _datePayement {get; set;}

    public DateTime DatePayement
    {
        get
        {
            return _datePayement;
        }
        set 
        {
                     
            if (DateTime.TryParseExact(value.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                _datePayement = parsedDate; // Set the _datePayement to the parsed date
            }
            else
            {
                throw new ArgumentException("Invalid date format. Please use dd-MM-yyyy.");
            }
        }
    }


    public Payement(string itemType, double amount, DateTime datePayement)
    {
        ItemType = itemType;
        Amount = amount; 
        _datePayement = datePayement;


    }


}