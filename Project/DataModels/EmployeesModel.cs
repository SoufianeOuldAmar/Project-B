using System.Text.Json.Serialization;

public class EmployeesModel
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Age")]
    public int Age { get; set; }
    [JsonPropertyName("Accepted")]
    public bool Accepted { get; set; }
    [JsonPropertyName("CvFileName")]
    public string CvFileName { get; set; }
    public EmployeesModel(string name, int age, bool accepted, string cvFileName)
    {
        // int id, 
        // Id = id;
        Name = name;
        Age = age;
        Accepted = accepted;
        CvFileName = cvFileName;
    }

}
// /Users/baselkharbeet/Documents/Sollicitatie-brief.pdf