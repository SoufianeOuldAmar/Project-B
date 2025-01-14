using System.Collections.Generic;

public static class NotificationLogic
{
    public static void NotifyBookingModification(
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
                    seatChanges[i],
                    seatChanges[i + 1]
                );
                newNotifications.Add(notification);
            }
        }

        // Handle new pets added
        int petID = 0;
        foreach (var pet in newPets)
        {
            var notification = new Notification(
                notificationId++,
                bookings[0].FlightID,
                "New Pet Added",
                null,
                pet.AnimalType
            );
            newNotifications.Add(notification);
            petID++;
        }

        // Handle new seats added
        foreach (var seat in newSeats)
        {
            var notification = new Notification(
                notificationId++,
                bookings[0].FlightID,
                "New Seat Added",
                null,
                seat
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
                null,
                baggage.BaggageType
            );
            newNotifications.Add(notification);
        }

        // Handle pet changes (if provided)
        if (petChanges != null)
        {
            for (int i = 0; i < petChanges.Count; i += 2) // Assuming pairs of "old seat" and "new seat"
            {
                var notification = new Notification(
                    notificationId++,
                    bookings[0].FlightID,
                    "Pet Change",
                    petChanges[i].AnimalType,
                    petChanges[i + 1].AnimalType
                );
                newNotifications.Add(notification);
            }
        }

        // Add all new notifications to the account
        account.Notifications.AddRange(newNotifications);

        // Update the account in the database
        DataAccessClass.UpdateCurrentAccount(account);
    }

    // TODO: Verander ticketprice, gate en newtime, met olddetails en iscancelled is voor de andere constructor

    public static void NotifyFlightModification(int flightID, List<double> ticketPriceChange, List<string> gateChange, bool isCancelled, List<string> newTimeChange)
    {
        var newNotifications = new List<Notification>();
        var allBookedFlights = BookedFlightsAccess.LoadAll();
        var allEmails = AccountsLogic.GetAllEmails();

        if (ticketPriceChange.Count > 1)
        {
            Notification notification = new Notification(1, flightID, "Ticket price changed", ticketPriceChange[0].ToString(), ticketPriceChange[1].ToString());
            newNotifications.Add(notification);
        }

        if (gateChange.Count > 1)
        {
            Notification notification = new Notification(1, flightID, "Gate changed", gateChange[0].ToString(), gateChange[1].ToString());
            newNotifications.Add(notification);
        }

        if (isCancelled)
        {
            Notification notification = new Notification(
                id: 1,
                flightID: flightID,
                description: "Flight got cancelled. Make sure to book a new flight!",
                oldDetails: string.Empty,
                newDetails: string.Empty
            );
            notification.ActionRequired = true;
            newNotifications.Add(notification);
        }


        if (newTimeChange.Count > 1)
        {
            Notification notification = new Notification(1, flightID, "Flight time changed", newTimeChange[0].ToString(), newTimeChange[1].ToString());
            newNotifications.Add(notification);
        }

        foreach (string email in allEmails)
        {
            foreach (var kvp in allBookedFlights)
            {
                if (kvp.Key == email)
                {
                    foreach (var bookedFlight in kvp.Value)
                    {
                        if (bookedFlight.FlightID == flightID)
                        {
                            var account = AccountsLogic.GetByEmail(email);
                            account.Notifications.AddRange(newNotifications);
                            DataAccessClass.UpdateCurrentAccount(account);
                        }
                    }
                }
            }
        }

    }

    public static bool CheckForNotifications(AccountModel accountModel)
    {
        foreach (Notification notification in accountModel.Notifications)
        {
            if (!notification.IsRead)
            {
                return true;
            }
        }

        return false;
    }

}