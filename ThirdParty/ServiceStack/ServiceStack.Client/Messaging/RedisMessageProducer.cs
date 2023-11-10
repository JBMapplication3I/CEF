//Copyright (c) ServiceStack, Inc. All Rights Reserved.
//License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

namespace ServiceStack.Messaging
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.Redis;

    public class RedisMessageProducer
        : IMessageProducer, IOneWayClient
    {
        private readonly IRedisClientsManager clientsManager;
        private readonly Action onPublishedCallback;

        public RedisMessageProducer(IRedisClientsManager clientsManager)
            : this(clientsManager, null) { }

        public RedisMessageProducer(IRedisClientsManager clientsManager, Action onPublishedCallback)
        {
            this.clientsManager = clientsManager;
            this.onPublishedCallback = onPublishedCallback;
        }

        private IRedisNativeClient readWriteClient;
        public IRedisNativeClient ReadWriteClient
        {
            get
            {
                if (readWriteClient == null)
                {
                    readWriteClient = (IRedisNativeClient)clientsManager.GetClient();
                }
                return readWriteClient;
            }
        }

        public void Publish<T>(T messageBody)
        {
            if (messageBody is IMessage message)
            {
                Publish(message.ToInQueueName(), message);
            }
            else
            {
                Publish(new Message<T>(messageBody));
            }
        }

        public void Publish<T>(IMessage<T> message)
        {
            Publish(message.ToInQueueName(), message);
        }

        public void SendOneWay(object requestDto)
        {
            Publish(MessageFactory.Create(requestDto));
        }

        public void SendOneWay(string queueName, object requestDto)
        {
            Publish(queueName, MessageFactory.Create(requestDto));
        }

        public void SendAllOneWay(IEnumerable<object> requests)
        {
            if (requests == null)
            {
                return;
            }

            foreach (var request in requests)
            {
                SendOneWay(request);
            }
        }

        public void Publish(string queueName, IMessage message)
        {
            var messageBytes = message.ToBytes();
            ReadWriteClient.LPush(queueName, messageBytes);
            ReadWriteClient.Publish(QueueNames.TopicIn, queueName.ToUtf8Bytes());

            onPublishedCallback?.Invoke();
        }

        public void Dispose()
        {
            readWriteClient?.Dispose();
        }
    }
}