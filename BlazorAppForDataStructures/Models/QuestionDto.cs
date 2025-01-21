namespace BlazorAppForDataStructures.Models
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public int TopicId { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
