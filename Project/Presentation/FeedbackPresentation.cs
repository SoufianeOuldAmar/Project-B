using BusinessLogic;

public static class FeedbackPresentation
{
    public static void FeedbackMenu(AccountModel accountModel)
    {
        bool isValidChoice = false;

        while (!isValidChoice)
        {
            Console.Clear();
            Console.WriteLine("=== 📋 Feedback Menu ===\n");
            Console.WriteLine("1. 📝 Submit Feedback");
            Console.WriteLine("2. 📂 Manage Feedbacks");
            Console.WriteLine("3. ❌ Quit");

            Console.Write("\nChoose an option: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                SubmitFeedbackMenu(accountModel);
                isValidChoice = true;
            }
            else if (choice == "2")
            {
                ManageUserFeedbacks(accountModel);
                isValidChoice = true;
            }
            else if (choice == "3")
            {
                MenuPresentation.PressAnyKey();
                isValidChoice = true;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter either 1, 2, or 3.");
                MenuPresentation.PressAnyKey();
            }
        }

    }

    public static void SubmitFeedbackMenu(AccountModel accountModel)
    {
        Console.Clear();
        Console.WriteLine("=== 📝 Submit Feedback ===\n");
        Console.Write("Enter your feedback: ");
        string content = Console.ReadLine();

        try
        {
            FeedbackLogic.SubmitFeedback(accountModel.EmailAddress, content);
            Console.WriteLine("Thank you for your feedback! Your feedback has been submitted successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // MenuPresentation.PressAnyKey();
    }

    public static void ManageUserFeedbacks(AccountModel accountModel)
    {
        Console.Clear();
        var feedbacks = FeedbackLogic.GetAllFeedbacks().Where(f => f.UserEmail == accountModel.EmailAddress).ToList();

        if (feedbacks.Count == 0)
        {
            Console.WriteLine("You have no feedback to manage.");
            // MenuPresentation.PressAnyKey();
            return;
        }

        Console.WriteLine("=== 📂 Manage Feedbacks ===\n");
        foreach (var feedback in feedbacks)
        {
            Console.WriteLine($"ID: {feedback.Id}");
            Console.WriteLine($"Content: {feedback.Content}");
            Console.WriteLine($"Status: {(feedback.IsClosed ? "Closed" : "Open")}");
            Console.WriteLine(new string('-', 30));
        }

        Console.Write("\nEnter the ID of the feedback to delete or press Enter to go back: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int id))
        {
            var feedbackToDelete = feedbacks.FirstOrDefault(f => f.Id == id);
            if (feedbackToDelete != null)
            {
                FeedbackLogic.DeleteFeedback(id);
                Console.WriteLine("Feedback deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ID. Returning to feedback menu.");
            }
        }

        // MenuPresentation.PressAnyKey();
    }
    public static void ViewFeedbackMenu()
    {
        Console.Clear();
        var feedbacks = FeedbackLogic.GetAllFeedbacks();

        if (feedbacks.Count == 0)
        {
            Console.WriteLine("No feedback available.");
            MenuPresentation.PressAnyKey();
            return;
        }

        Console.WriteLine("=== 👀 View Feedback ===\n");
        foreach (var feedback in feedbacks)
        {
            Console.WriteLine($"ID: {feedback.Id}");
            Console.WriteLine($"User: {feedback.UserEmail}");
            Console.WriteLine($"Content: {feedback.Content}");
            Console.WriteLine($"Status: {(feedback.IsClosed ? "Closed" : "Open")}");
            Console.WriteLine(new string('-', 30));
        }

        Console.Write("\nEnter the ID of the feedback to manage or press Enter to go back: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int id))
        {
            var feedbackToUpdate = feedbacks.FirstOrDefault(f => f.Id == id);

            if (feedbackToUpdate != null)
            {
                Console.WriteLine("1. Close Feedback");
                Console.WriteLine("2. Delete Feedback");
                Console.Write("\nChoose an action: ");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        FeedbackLogic.CloseFeedback(id);
                        Console.WriteLine("Feedback marked as closed.");
                        break;

                    case "2":
                        FeedbackLogic.DeleteFeedback(id);
                        Console.WriteLine("Feedback deleted successfully.");
                        break;

                    default:
                        Console.WriteLine("Invalid action. Returning to the feedback menu.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid ID. Returning to the feedback menu.");
            }
        }

        // MenuPresentation.PressAnyKey();
    }
}