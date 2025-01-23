using System.Text.Json.Serialization;
public class FinancialAccountModel : AccountModel, IDataModel
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }


    public FinancialAccountModel(int id, string username, string password) : base(id, password)
    {
        UserName = username;
    }
}