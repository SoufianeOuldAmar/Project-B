using System.Text.Json.Serialization;

public abstract class BaseAccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    public BaseAccountModel(int id, string password)
    {
        Id = id;
        Password = password;
    }
}
