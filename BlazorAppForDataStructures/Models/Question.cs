namespace BlazorAppForDataStructures.Models
{
    public class Question
    {
        public int QuestionId { get; set; } // Primary Key
        public int TopicId { get; set; }    // Foreign Key

        public string QuestionText { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<Answer> Answers { get; set; } = new List<Answer>(); // Initialized to avoid null references
        public Topic Topic { get; set; } = null!; // Not nullable, as a Question should always belong to a Topic
    }
}
