namespace StackExchange.Redis
{
    using System.Collections.Generic;

    internal interface IMultiMessage
    {
        IEnumerable<Message> GetMessages(PhysicalConnection connection);
    }
}
