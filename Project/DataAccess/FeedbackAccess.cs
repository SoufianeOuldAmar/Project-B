using System.Text.Json;
using DataModels;

namespace DataAccess
{
    public static class FeedbackAccess
    {
        private static string fileName = "DataSources/feedback.json";

        public static List<FeedbackModel> LoadAll()
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<List<FeedbackModel>>(json) ?? new List<FeedbackModel>();
            }
            return new List<FeedbackModel>();
        }

        public static void SaveAll(List<FeedbackModel> feedbacks)
        {
            string json = JsonSerializer.Serialize(feedbacks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
        }

        public static void AddFeedback(FeedbackModel feedback)
        {
            var feedbacks = LoadAll();
            feedbacks.Add(feedback);
            SaveAll(feedbacks);
        }

        public static void UpdateFeedback(FeedbackModel updatedFeedback)
        {
            var feedbacks = LoadAll();
            int index = feedbacks.FindIndex(f => f.Id == updatedFeedback.Id);
            if (index != -1)
            {
                feedbacks[index] = updatedFeedback;
                SaveAll(feedbacks);
            }
        }
    }
}