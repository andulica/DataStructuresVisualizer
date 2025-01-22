using BlazorAppForDataStructures.Models;

public class QuizService
{
    private readonly HttpClient _httpClient;

    public QuizService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<Question>> GetQuestionsAsync(string topicName)
    {

        var url = $"https://datastructviz-quiz-api-001-hbcza9gdbpb7gzew.canadacentral-01.azurewebsites.net/api/Topics/{topicName}/questions";

        var questions = await _httpClient.GetFromJsonAsync<List<Question>>(url);

        return questions ?? new List<Question>();
    }
}