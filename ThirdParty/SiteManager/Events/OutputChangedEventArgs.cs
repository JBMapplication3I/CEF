namespace SiteManager.Events
{
    using System;

    public class OutputChangedEventArgs : EventArgs
    {
        public OutputChangedEventArgs(string? output, object? userState = null)
        {
            Output = output;
            UserState = userState;
        }

        public string? Output { get; }

        public object? UserState { get; }
    }
}
