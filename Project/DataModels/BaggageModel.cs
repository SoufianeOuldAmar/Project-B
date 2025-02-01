public class BaggageModel
{
    public string Initials { get; set; } // ties baggage to an initial
    public string BaggageType { get; set; } // Carry on or Checked
    public double BaggageWeight { get; set; } // Weight of baggage in kg

    public double Fee { get; set; }

    public BaggageModel(string initials, string baggageType, double baggageWeight)
    {
        Initials = initials;
        BaggageType = baggageType;
        BaggageWeight = baggageWeight;

        if (BaggageType == "Carry-On")
        {
            Fee = 15;
        }
        else if (BaggageType == "Checked")
        {
            Fee = BaggageWeight;
        }
        else
        {
            Fee = 0;
        }
    }
}