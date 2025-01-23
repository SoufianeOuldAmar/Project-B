public class BaggageLogic
{
    public string Initials { get; set; } // ties baggage to an initial
    public string BaggageType { get; set; } // Carry on or Checked
    public double BaggageWeight { get; set; } // Weight of baggage in kg

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

    public BaggageLogic(string initials, string baggageType, double baggageWeight)
    {
        Initials = initials;
        BaggageType = baggageType;
        BaggageWeight = baggageWeight;
        Fee = CalcFee();
    }
    public double CalcFee()
    {
        double maxWeight = 0;

        if (BaggageType == "Carry-On")
        {
            maxWeight = 10;
            return 15;
        }

        else if (BaggageType == "Checked")
        {
            return BaggageWeight; // fee == baggage weight (20 / 25)
        }
        return 0; // No fee 
    }
}

