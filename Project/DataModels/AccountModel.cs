using System.Text.Json.Serialization;

public abstract class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    public AccountModel(int id, string password)
    {
        Id = id;
        Password = password;
    }
}
