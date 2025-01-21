namespace BlazorAppForDataStructures.Models
{
    public class QuizDto
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
