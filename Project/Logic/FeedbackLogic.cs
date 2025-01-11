using DataAccess;
using DataModels;

namespace BusinessLogic
{
    public static class FeedbackLogic
    {
        public static void SubmitFeedback(string userEmail, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Feedback cannot be empty.");
            }

            var feedbacks = DataAccessClass.ReadList<FeedbackModel>("DataSources/feedback.json");
            int newId = feedbacks.Count > 0 ? feedbacks.Max(f => f.Id) + 1 : 1;
            var feedback = new FeedbackModel(newId, userEmail, content);
            DataAccessClass.AddFeedback(feedback);
        }

        public static List<FeedbackModel> GetAllFeedbacks()
        {
            return DataAccessClass.ReadList<FeedbackModel>("DataSources/feedback.json");
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
    }
}