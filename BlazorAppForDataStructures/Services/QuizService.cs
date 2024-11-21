using BlazorAppForDataStructures.Data;
using BlazorAppForDataStructures.Models;
using Microsoft.EntityFrameworkCore;

public class QuizService
{
    private readonly BlazorAppForDataStructuresContext _context;

    public QuizService(BlazorAppForDataStructuresContext context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetQuestionsAsync(string topicName)
    {
        return await _context.Questions
            .Include(q => q.Answers)
            .Where(q => q.Topic.Name == topicName)
            .ToListAsync();
    }

    public Question? GetQuestionByIndex(int index)
    {
        return _context.Questions.Include(q => q.Answers).Skip(index).FirstOrDefault();
    }
}