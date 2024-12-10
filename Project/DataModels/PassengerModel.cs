namespace DataModels
{
    public class PassengerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string AgeGroup { get; set; } // e.g., "adult", "child", "infant"
        public DateTime? DateOfBirth { get; set; } // Only for infants
    }
}
