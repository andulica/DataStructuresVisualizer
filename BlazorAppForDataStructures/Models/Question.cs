using System.ComponentModel.DataAnnotations;


namespace BlazorAppForDataStructures.Models
{
    public class Question
    {
        public int QuestionId { get; set; } // Primary Key
        public int TopicId { get; set; }    // Foreign Key

        [MinLength(5, ErrorMessage = "Question text must be at least 5 characters long.")]
        [Required(ErrorMessage = "Question text is required.")]
        public string QuestionText { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<Answer> Answers { get; set; } = new List<Answer>(); // Initialized to avoid null references
        public Topic Topic { get; set; } = null!; // Not nullable, as a Question should always belong to a Topic
    }
}
