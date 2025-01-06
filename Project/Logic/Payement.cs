public class Payement 
{
    public string ItemType {get; set;}
    public double Amount{ get; set;}

    public DateTime DatePayement {get; set;}


    public Payement(string itemType, double amount, DateTime datePayement)
    {
        ItemType = itemType;
        Amount = amount; 
        DatePayement = datePayement;


    }


}