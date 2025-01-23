﻿using System;
using System.Collections.Generic;
using System.Linq; // Required for LINQ
using System.Text.Json.Serialization;

using System;
using System.Collections.Generic;

public class AccountModel : BaseAccountModel, IDataModel
{
    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("flightPointsDataList")]
    public List<FlightPoint> FlightPointsDataList { get; set; }

    public int TotalFlightPoints { get; set; } // Still dynamically calculated

    [JsonPropertyName("fees")]
    public double Fee { get; set; }

    [JsonPropertyName("notifications")]
    public List<Notification> Notifications { get; set; }

    public AccountModel(int id, string emailAddress, string password, string fullName)
        : base(id, password)
    {
        EmailAddress = emailAddress;
        FullName = fullName;
        FlightPointsDataList = new List<FlightPoint>();
        Fee = 0;
        Notifications = new List<Notification>();
    }
}
