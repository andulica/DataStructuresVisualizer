namespace BlazorAppForDataStructures.Models
{
    public class QuestionFeedback
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string UserAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public string Feedback { get; set; }
    }
}
