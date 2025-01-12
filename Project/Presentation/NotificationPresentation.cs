using System.Collections.Generic;

public static class NotificationPresentation
{

    public static void PrintNotificationPage(AccountModel currentAccount)
    {
        List<Notification> notifications = currentAccount.Notifications;

        if (notifications == null || !notifications.Any())
        {
            Console.Clear();
            Console.WriteLine("========== 🔔 Notifications ==========\n");
            Console.WriteLine("No notifications available.");
            MenuLogic.PopMenu();
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("========== 🔔 Notifications ==========\n");
            for (int i = 0; i < notifications.Count; i++)
            {
                var notification = notifications[i];
                Console.ForegroundColor = notification.IsRead ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"{i + 1}. {(notification.IsRead ? "[Read]" : "[Unread]")} - Flight ID: {notification.FlightID}");
            }

            Console.ResetColor();

            Console.WriteLine();
            Console.Write("Enter the number of a notification to view it, or type 0 to go back: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > notifications.Count)
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            if (choice == 0)
            {
                MenuLogic.PopMenu();
                break; // Exit the notifications page
            }

            var selectedNotification = notifications[choice - 1];
            ShowNotificationDetails(selectedNotification, currentAccount);
        }
    }

    private static void ShowNotificationDetails(Notification notification, AccountModel currentAccount)
    {
        Console.Clear();
        Console.WriteLine("========== 📋 Notification Details ==========\n");
        Console.WriteLine($"Notification ID: {notification.Id}");
        Console.WriteLine($"Flight ID: {notification.FlightID}");
        Console.WriteLine();

        if (string.IsNullOrEmpty(notification.OldDetails))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("New Addition Detected:");
            Console.ResetColor();
            Console.WriteLine($"Details: {notification.NewDetails}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Change Detected:");
            Console.ResetColor();
            Console.WriteLine($"Old Details: {notification.OldDetails}");
            Console.WriteLine($"New Details: {notification.NewDetails}");
        }

        Console.WriteLine();
        Console.WriteLine("========== 📃 Description ==========\n");
        Console.WriteLine(notification.Description);
        Console.WriteLine();

        Console.WriteLine("========== ⌛ Status ==========\n");
        Console.WriteLine($"Is Read: {(notification.IsRead ? "Yes" : "No")}");
        Console.WriteLine();

        // Notify the user if action is required
        if (notification.ActionRequired)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!! Action Required: Please address this notification immediately. !!!");
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("1. Mark as Read");
        Console.WriteLine("2. Go Back");
        Console.Write("\nEnter your choice: ");

        string choice = Console.ReadLine();
        if (choice == "1")
        {
            notification.IsRead = true;
            DataAccessClass.UpdateCurrentAccount(currentAccount);
            Console.WriteLine("\nNotification marked as read.");
        }
        else if (choice == "2")
        {
            Console.WriteLine("\nReturning to the notifications list...\n");
        }
        else
        {
            Console.WriteLine("\nInvalid option. Returning to the notifications list...");
        }

        MenuPresentation.PressAnyKey();
    }

}
