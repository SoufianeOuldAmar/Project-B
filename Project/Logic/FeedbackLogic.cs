using System.Collections.Generic;
using DataAccess;
using DataModels;

namespace BusinessLogic
{
    public static class FeedbackLogic
    {
        public static List<FeedbackModel> feedbacks = DataAccessClass.ReadList<FeedbackModel>("DataSources/feedback.json");
        public static void SubmitFeedback(string userEmail, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Feedback cannot be empty.");
            }

            int newId = feedbacks.Count > 0 ? feedbacks.Max(f => f.Id) + 1 : 1;
            var feedback = new FeedbackModel(newId, userEmail, content);
            DataAccessClass.AddFeedback(feedback);
        }

        public static FeedbackModel GetFeedbackByID(int id)
        {
            return feedbacks.FirstOrDefault(f => f.Id == id);
        }

        public static void CloseFeedback(int id)
        {
            var feedbacks = DataAccessClass.ReadList<FeedbackModel>("DataSources/feedback.json");
            var feedbackToClose = feedbacks.FirstOrDefault(f => f.Id == id);
            if (feedbackToClose != null)
            {
                feedbackToClose.IsClosed = true;
                DataAccessClass.WriteList<FeedbackModel>("DataSources/feedback.json", feedbacks);
            }
        }

        public static void DeleteFeedback(int id)
        {
            var feedbacks = DataAccessClass.ReadList<FeedbackModel>("DataSources/feedback.json");
            var feedbackToRemove = feedbacks.FirstOrDefault(f => f.Id == id);
            if (feedbackToRemove != null)
            {
                feedbacks.Remove(feedbackToRemove);
                DataAccessClass.WriteList<FeedbackModel>("DataSources/feedback.json", feedbacks);
            }
        }

        public static List<FeedbackModel> GetFeedbackForUser()
        {
            string userEmail = MenuPresentation.currentAccount.EmailAddress;
            // Load all feedback from the file
            var allFeedback = DataAccessClass.ReadList<FeedbackModel>("DataSources/feedback.json");

            // Filter feedback based on the user's email address
            var userFeedback = allFeedback.Where(f => f.UserEmail == userEmail).ToList();

            return userFeedback;
        }

        public static (bool, List<FeedbackModel>) CheckForFeedback()
        {
            var feedbacks = DataAccessClass.ReadList<FeedbackModel>("DataSources/feedback.json");
            return (feedbacks.Count > 0, feedbacks);
        }

    }
}