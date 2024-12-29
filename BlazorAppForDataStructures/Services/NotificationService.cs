
    public class NotificationService
    {
        public event Action<string>? OnMessageAdded;
        public event Action? OnMessageCleared;

        /// <summary>
        /// Adaugă un mesaj nou și notifică subscriitorii.
        /// </summary>
        public void AddMessage(string message)
        {
            OnMessageAdded?.Invoke(message);
        }

        /// <summary>
        /// Golește mesajele și notifică subscriitorii.
        /// </summary>
        public void ClearMessage()
        {
            OnMessageCleared?.Invoke();
        }
    }

