public class BaggageLogic 
{
    public string Initials {get; set;} // ties baggage to an initial
    public string BaggageType { get; set; } // Carry on or Checked
    public double BaggageWeight {get; set; } // Weight of baggage in kg

    private double _fee;

    public double Fee 
    {
        get 
        {
            return _fee;
        }
        set
        {
            _fee = value;
        }
    }

    public BaggageLogic( string initials, string baggageType, double baggageWeight)
    {
        Initials= initials;
        BaggageType = baggageType;
        BaggageWeight = baggageWeight;
        Fee= CalcFee();
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

