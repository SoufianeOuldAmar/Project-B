public static class NotificationPresentation
{
    public static void PrintNotificationPage()
    {
        var notifications = AccountsLogic.CurrentAccount.Notifications;

        if (notifications == null || !notifications.Any())
        {
            Console.Clear();
            Console.WriteLine("========== Notifications ==========");
            Console.WriteLine("No notifications available.");
            Console.WriteLine("Press any key to return to the previous menu...");
            Console.ReadKey();
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("========== Notifications ==========");
            for (int i = 0; i < notifications.Count; i++)
            {
                var notification = notifications[i];
                Console.WriteLine($"{i + 1}. {(notification.IsRead ? "[Read]" : "[Unread]")} - Flight ID: {notification.FlightID}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter the number of a notification to view it, or type 0 to go back:");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > notifications.Count)
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            if (choice == 0)
            {
                break; // Exit the notifications page
            }

            var selectedNotification = notifications[choice - 1];
            ShowNotificationDetails(selectedNotification);
        }
    }

    private static void ShowNotificationDetails(Notification notification)
    {
        Console.Clear();
        Console.WriteLine("========== Notification Details ==========");
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
        Console.WriteLine("========== Action Required ==========");
        Console.WriteLine(notification.ActionRequired);
        Console.WriteLine();

        Console.WriteLine("========== Status ==========");
        Console.WriteLine($"Is Read: {(notification.IsRead ? "Yes" : "No")}");
        Console.WriteLine();

        Console.WriteLine("Options:");
        Console.WriteLine("1. Mark as Read");
        Console.WriteLine("2. Go Back");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine();
        if (choice == "1")
        {
            notification.IsRead = true;
            Console.WriteLine("\nNotification marked as read.");
        }
        else if (choice == "2")
        {
            Console.WriteLine("\nReturning to the notifications list...");
        }
        else
        {
            Console.WriteLine("\nInvalid option. Returning to the notifications list...");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
