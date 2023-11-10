//Copyright (c) ServiceStack, Inc. All Rights Reserved.
//License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

namespace ServiceStack.Messaging
{
    using System;
    using ServiceStack.Redis;

    public class RedisMessageQueueClientFactory
        : IMessageQueueClientFactory
    {
        private readonly Action onPublishedCallback;
        private readonly IRedisClientsManager clientsManager;

        public RedisMessageQueueClientFactory(
            IRedisClientsManager clientsManager, Action onPublishedCallback)
        {
            this.onPublishedCallback = onPublishedCallback;
            this.clientsManager = clientsManager;
        }

        public IMessageQueueClient CreateMessageQueueClient()
        {
            return new RedisMessageQueueClient(
                clientsManager, onPublishedCallback);
        }

        public void Dispose()
        {
        }
    }
}