using System.Collections.Generic;

public static class NotificationLogic
{
    public static void GetNotification(string email, List<BookedFlightsModel> bookings, List<PetLogic> newPets, List<string> newSeats, List<BaggageLogic> newBaggageAdded, List<string> seatChanges, List<PetLogic> petChanges = null)
    {
        var account = AccountsLogic.GetByEmail(email);

        if (seatChanges.Count > 0)
        {
            var notification = new Notification(account.Notifications.Count + 1, bookings[0].FlightID, "", seatChanges[0], seatChanges[1]);
            account.Notifications.Add(notification);
        }

        AccountsAccess.UpdateCurrentAccount(account);
    }
}