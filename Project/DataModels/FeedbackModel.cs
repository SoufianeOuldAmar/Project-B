namespace DataModels
{
    public class FeedbackModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Content { get; set; }
        public bool IsClosed { get; set; } = false;

        public FeedbackModel(int id, string userEmail, string content)
        {
            Id = id;
            UserEmail = userEmail;
            Content = content;
        }
    }
}