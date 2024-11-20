public class BaggageLogic 
{
    public string ChosenSeats {get; set;} // chosen seat with initial 
    public string BaggageType { get; set; } // Carry on or Checked
    public double BaggageWeight {get; set; } // Weight of baggage in kg

    public BaggageLogic(string chosenSeats, string baggageType, double baggageWeight)
    {
        ChosenSeats = chosenSeats;
        BaggageType = baggageType;
        BaggageWeight = baggageWeight;
    }
    public double CalcFee()
    {
        double maxWeight = 0;

        if (BaggageType == "Carry-On")
        {
            maxWeight = 10; 
        }

        else if (BaggageType == "Checked")
        {

            maxWeight = 25; 

        }

        if (BaggageWeight > maxWeight)
        {
            // difference * 20 
            return (BaggageWeight - maxWeight) * 20; // Fee of 20 EUR/kg over the limit
        }

        return 0; // No fee if weight is within the limit
    }
}

