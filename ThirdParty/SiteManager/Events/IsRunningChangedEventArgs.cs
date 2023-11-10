namespace SiteManager.Events
{
    using System;

    public class IsRunningChangedEventArgs : EventArgs
    {
        public IsRunningChangedEventArgs(bool isRunning, object? userState = null)
        {
            IsRunning = isRunning;
            UserState = userState;
        }

        public bool IsRunning { get; }

        public object? UserState { get; }
    }
}
