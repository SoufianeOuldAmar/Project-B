using BusinessLogic;
using DataModels;
public static class FeedbackPresentation
{
    public static void FeedbackMenu(UserAccountModel userAccountModel)
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
                SubmitFeedbackMenu(userAccountModel);
                isValidChoice = true;
            }
            else if (choice == "2")
            {
                ManageUserFeedbacks(userAccountModel);
                isValidChoice = true;
            }
            else if (choice == "3")
            {
                isValidChoice = true;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter either 1, 2, or 3.");
                MenuPresentation.PressAnyKey();
            }
        }

    }

    public static void SubmitFeedbackMenu(UserAccountModel userAccountModel)
    {
        Console.Clear();
        Console.WriteLine("=== 📝 Submit Feedback ===\n");
        Console.Write("Enter your feedback: ");
        string content = Console.ReadLine();

        try
        {
            FeedbackLogic.SubmitFeedback(userAccountModel.EmailAddress, content);
            MenuPresentation.PrintColored("\nThank you for your feedback! Your feedback has been submitted successfully.", ConsoleColor.Green);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void ManageUserFeedbacks(UserAccountModel userAccountModel)
    {
        Console.Clear();
        var feedbacks = FeedbackLogic.GetFeedbackForUser();

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
            var feedbackToDelete = FeedbackLogic.GetFeedbackByID(id);
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
        var isFeedbackAvailable = FeedbackLogic.CheckForFeedback().Item1;
        var feedbacks = FeedbackLogic.CheckForFeedback().Item2;


        if (!isFeedbackAvailable)
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
            var feedbackToUpdate = FeedbackLogic.GetFeedbackByID(id);

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
                        MenuPresentation.PrintColored("Feedback marked as closed.", ConsoleColor.Yellow);
                        break;

                    case "2":
                        FeedbackLogic.DeleteFeedback(id);
                        MenuPresentation.PrintColored("Feedback deleted successfully.", ConsoleColor.Green);
                        break;

                    default:
                        MenuPresentation.PrintColored("Invalid action. Returning to the feedback menu.", ConsoleColor.Red);
                        break;
                }
            }
            else
            {
                MenuPresentation.PrintColored("Invalid ID. Returning to the feedback menu.", ConsoleColor.Red);
            }
        }

        MenuPresentation.PressAnyKey();
    }
}