using System.ComponentModel.DataAnnotations;

namespace BlazorAppForDataStructures.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        [Required(ErrorMessage = "Topic name is required.")]
        [StringLength(30, ErrorMessage = "Topic name cannot exceed 30 characters.")]
        public string? Name { get; set; }
        public ICollection<Question>? Questions { get; set; }

    }
}
