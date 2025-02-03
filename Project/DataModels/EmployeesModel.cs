using System.Text.Json.Serialization;

public class EmployeesModel : IDataModel
{
    [JsonPropertyName("Id")]
    public int Id { get; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Age")]
    public int Age { get; set; }
    [JsonPropertyName("Accepted")]
    public string Accepted { get; set; } = "Not reviewed yet";
    [JsonPropertyName("CvFileName")]
    public string CvFileName { get; }
    public int RegistrationID { get; }
    public EmployeesModel(int id, string name, int age, string cvFileName, int registrationID)
    {
        // int id, 
        Id = id;
        Name = name;
        CvFileName = cvFileName;
        RegistrationID = registrationID;
    }

}