namespace BlazorAppForDataStructures.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int TopicId { get; set; }
        public string? QuestionText { get; set; }
        public ICollection<Answer>? Answers { get; set; }

        public Topic? Topic { get; set; }
    }
}
