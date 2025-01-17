public class PetLogic
{
    public string AnimalType { get; set; }
    public string PetName { get; set; }
    public double Fee { get; set; }

    public PetLogic(string animalType, string petName)
    {

        AnimalType = animalType;
        PetName = petName;
        Fee = CalcFee();
    }

    public double CalcFee()
    {
        return 50;
    }


}