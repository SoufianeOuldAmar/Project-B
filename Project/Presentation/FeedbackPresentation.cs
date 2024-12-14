using BusinessLogic;

namespace PresentationLayer
{
    public static class FeedbackPresentation
    {
        public static void SubmitFeedbackMenu()
        {
            Console.Clear();
            var currentUser = AccountsLogic.CurrentAccount;
            if (currentUser == null)
            {
                Console.WriteLine("You must be logged in to submit feedback.");
                MenuLogic.PopMenu();
                return;
            }

            Console.WriteLine("=== Submit Feedback ===");
            Console.Write("Enter your feedback: ");
            string content = Console.ReadLine();

            try
            {
                FeedbackLogic.SubmitFeedback(currentUser.EmailAddress, content);
                Console.WriteLine("Thank you for your feedback! Your feedback has been submitted successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            MenuPresentation.PressAnyKey();
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

            Console.WriteLine("=== Feedback List ===");
            foreach (var feedback in feedbacks)
            {
                Console.WriteLine($"ID: {feedback.Id}");
                Console.WriteLine($"User: {feedback.UserEmail}");
                Console.WriteLine($"Content: {feedback.Content}");
                Console.WriteLine($"Status: {(feedback.IsClosed ? "Closed" : "Open")}");
                Console.WriteLine(new string('-', 30));
            }

            Console.Write("Enter the ID of the feedback to close or press Enter to go back: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id))
            {
                FeedbackLogic.CloseFeedback(id);
                Console.WriteLine("Feedback has been closed successfully.");
            }

            MenuPresentation.PressAnyKey();
        }
    }
}