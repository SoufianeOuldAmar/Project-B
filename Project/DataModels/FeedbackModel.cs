namespace DataModels
{
    public class FeedbackModel: IDataModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Content { get; set; }
        public bool IsClosed { get; set; } = false;
        public DateTime CreatedAt { get; set; }

        public FeedbackModel(int id, string userEmail, string content)
        {
            Id = id;
            UserEmail = userEmail;
            Content = content;
            CreatedAt = DateTime.Now;
        }
    }
}