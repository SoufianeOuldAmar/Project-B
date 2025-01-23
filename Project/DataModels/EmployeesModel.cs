using System.Text.Json.Serialization;

public class EmployeesModel: IDataModel
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Age")]
    public int Age { get; set; }
    [JsonPropertyName("Accepted")]
    // private string _accepted = "Not reviewed yet";
    public string Accepted { get; set; } = "Not reviewed yet";
    [JsonPropertyName("CvFileName")]
    public string CvFileName { get; set; }
    public int RegistrationID { get; set; }
    public EmployeesModel(string name, int age, string cvFileName, int registrationID)
    {
        // int id, 
        // Id = id;
        Name = name;
        Age = age;
        //  string accepted,
        // Accepted = accepted;
        CvFileName = cvFileName;
        RegistrationID = registrationID;
    }

}
// /Users/baselkharbeet/Documents/Sollicitatie-brief.pdf