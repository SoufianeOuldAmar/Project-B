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
            FeedbackAccess.AddFeedback(feedback);
        }

        public static List<FeedbackModel> GetAllFeedbacks()
        {
            return FeedbackAccess.LoadAll();
        }

        public static void CloseFeedback(int id)
        {
            var feedbacks = FeedbackAccess.LoadAll();
            var feedbackToClose = feedbacks.FirstOrDefault(f => f.Id == id);
            if (feedbackToClose != null)
            {
                feedbackToClose.IsClosed = true;
                FeedbackAccess.SaveAll(feedbacks);
            }
        }
        
        public static void DeleteFeedback(int id)
        {
            var feedbacks = FeedbackAccess.LoadAll();
            var feedbackToRemove = feedbacks.FirstOrDefault(f => f.Id == id);
            if (feedbackToRemove != null)
            {
                feedbacks.Remove(feedbackToRemove);
                FeedbackAccess.SaveAll(feedbacks);
            }
        }
    }
}