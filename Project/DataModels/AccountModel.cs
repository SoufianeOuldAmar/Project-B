using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
public class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

   [JsonPropertyName("flightPointsDataList")]
    public List<FlightPoint> FlightPointsDataList { get; set; }

    [JsonPropertyName("fees")]
    public double Fee{get; set;}

    public AccountModel(int id, string emailAddress, string password, string fullName)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        FlightPointsDataList = new List<FlightPoint>();
        Fee = 0;
    }

}