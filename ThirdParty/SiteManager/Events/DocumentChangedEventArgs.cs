namespace SiteManager.Events
{
    using System;
    using System.Windows.Documents;

    public class DocumentChangedEventArgs : EventArgs
    {
        public DocumentChangedEventArgs(FlowDocument? document, object? userState = null)
        {
            Document = document;
            UserState = userState;
        }

        public FlowDocument? Document { get; }

        public object? UserState { get; }
    }
}
