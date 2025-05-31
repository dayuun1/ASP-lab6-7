namespace BlazorBooking1
{
    public class AuthStateService
    {
        public event Action? OnChange;
        public bool IsLoggedIn { get; private set; }

        public void SetLoginState(bool loggedIn)
        {
            IsLoggedIn = loggedIn;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
