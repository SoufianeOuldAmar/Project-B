using System.Collections.Generic;

public static class NotificationLogic
{
    public static void GetNotification(
    string email,
    List<BookedFlightsModel> bookings,
    List<PetLogic> newPets,
    List<string> newSeats,
    List<BaggageLogic> newBaggageAdded,
    List<string> seatChanges,
    List<PetLogic> petChanges)
    {
        // Fetch the account for the provided email
        var account = AccountsLogic.GetByEmail(email);

        // Initialize a list to hold all new notifications
        var newNotifications = new List<Notification>();
        int notificationId = account.Notifications.Count + 1;

        // Handle seat changes
        if (seatChanges.Count > 0)
        {
            for (int i = 0; i < seatChanges.Count; i += 2) // Assuming pairs of "old seat" and "new seat"
            {
                var notification = new Notification(
                    notificationId++,
                    bookings[0].FlightID,
                    "Seat Change",
                    $"Your seat has been changed from {seatChanges[i]} to {seatChanges[i + 1]}."
                );
                newNotifications.Add(notification);
            }
        }

        // Handle new pets added
        foreach (var pet in newPets)
        {
            var notification = new Notification(
                notificationId++,
                bookings[0].FlightID,
                "New Pet Added",
                $"A new pet has been added: {pet.PetName}, {pet.PetType}."
            );
            newNotifications.Add(notification);
        }

        // Handle new seats added
        foreach (var seat in newSeats)
        {
            var notification = new Notification(
                notificationId++,
                bookings[0].FlightID,
                "New Seat Added",
                $"A new seat has been added: {seat}."
            );
            newNotifications.Add(notification);
        }

        // Handle new baggage added
        foreach (var baggage in newBaggageAdded)
        {
            var notification = new Notification(
                notificationId++,
                bookings[0].FlightID,
                "New Baggage Added",
                $"A new baggage has been added: {baggage.Weight} kg, {baggage.Description}."
            );
            newNotifications.Add(notification);
        }

        // Handle pet changes (if provided)
        if (petChanges != null)
        {
            foreach (var pet in petChanges)
            {
                var notification = new Notification(
                    notificationId++,
                    bookings[0].FlightID,
                    "Pet Change",
                    $"Pet details have been updated: {pet.PetName}, {pet.PetType}."
                );
                newNotifications.Add(notification);
            }
        }

        // Add all new notifications to the account
        account.Notifications.AddRange(newNotifications);

        // Update the account in the database
        AccountsAccess.UpdateCurrentAccount(account);
    }

}