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

            var feedbacks = FeedbackAccess.LoadAll();
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
            var feedback = feedbacks.FirstOrDefault(f => f.Id == id);
            if (feedback != null && !feedback.IsClosed)
            {
                feedback.IsClosed = true;
                FeedbackAccess.UpdateFeedback(feedback);
            }
        }
    }
}