using System.Text.Json.Serialization;

public class AdminAccountModel : AccountModel, IDataModel
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    public AdminAccountModel(int id, string username, string password)
        : base(id, password) // Pass the parameters to the base class constructor
    {
        UserName = username;
    }
}
