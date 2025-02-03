public class PetModel
{
    public string AnimalType { get; set; }
    public string PetName { get; set; }
    public double Fee { get; set; }

    public PetModel(string animalType, string petName)
    {
        AnimalType = animalType;
        PetName = petName;
        Fee = 50;
    }
}
