using System.Text.Json.Serialization;
public class FinancialAccountModel 
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }


    public FinancialAccountModel(int id, string username, string password)
    {
        Id = id;
        UserName = username;
        Password = password;
    }






}