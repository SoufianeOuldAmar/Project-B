using System.Text.Json.Serialization;


public class AdminAccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    public AdminAccountModel(int id, string emailAddress, string password)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
    }

}
