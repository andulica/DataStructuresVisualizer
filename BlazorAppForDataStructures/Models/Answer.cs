using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorAppForDataStructures.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; } = string.Empty;

        private bool isCorrect;
        public bool IsCorrect
        {
            get => isCorrect;
            set
            {
                isCorrect = value;

                // Ensure only one answer is correct for the question
                if (value && ParentQuestion != null)
                {
                    foreach (var ans in ParentQuestion.Answers)
                    {
                        if (ans != this) ans.isCorrect = false;
                    }
                }
            }
        }

        [JsonIgnore]
        public Question? ParentQuestion { get; set; }
    }
}
