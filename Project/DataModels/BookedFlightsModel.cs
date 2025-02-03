using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using DataModels;

public class BookedFlightsModel
{
    public int FlightID { get; set; }
    public List<string> BookedSeats { get; set; }
    public List<PetModel> Pets { get; set; }
    public List<BaggageModel> BaggageInfo { get; set; }
    public bool IsCancelled { get; set; }
    public double TicketBill { get; set; }
    public FlightPoint FlightPoints { get; set; }
    public Dictionary<string, string> SeatInitials { get; set; }
    public string Email { get; set; }
    public List<FoodAndDrinkItem> FoodAndDrinkItems { get; set; }
    public string DateTicketsBought { get; set; }

    // Add parameterless constructor for JSON deserialization
    public BookedFlightsModel()
    {
        BookedSeats = new List<string>();
        Pets = new List<PetModel>();
        BaggageInfo = new List<BaggageModel>();
        SeatInitials = new Dictionary<string, string>();
        FoodAndDrinkItems = new List<FoodAndDrinkItem>();
    }

    public BookedFlightsModel(int flightID, List<string> bookedSeats, List<BaggageModel> baggageInfo, List<PetModel> pets, bool isCancelled)
    {
        FlightID = flightID;
        BookedSeats = bookedSeats ?? new List<string>();
        Pets = pets ?? new List<PetModel>();
        BaggageInfo = baggageInfo ?? new List<BaggageModel>();
        IsCancelled = isCancelled;
        TicketBill = 0;
        SeatInitials = new Dictionary<string, string>();
        FoodAndDrinkItems = new List<FoodAndDrinkItem>();
    }

}