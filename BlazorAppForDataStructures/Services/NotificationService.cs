public class NotificationService
{
    public event Action<string>? OnMessageAdded;
    public event Action? OnMessageCleared;

    /// <summary>
    /// Adds a new message and notifies subscribers.
    /// </summary>
    public void AddMessage(string message)
    {
        OnMessageAdded?.Invoke(message);
    }

    /// <summary>
    /// Clears the messages and notifies subscribers.
    /// </summary>
    public void ClearMessage()
    {
        OnMessageCleared?.Invoke();
    }
}