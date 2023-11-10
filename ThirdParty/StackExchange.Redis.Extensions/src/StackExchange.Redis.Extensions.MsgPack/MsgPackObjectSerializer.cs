namespace StackExchange.Redis.Extensions.MsgPack
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using global::MsgPack.Serialization;
    using StackExchange.Redis.Extensions.Core;

    /// <summary>A message pack object serializer.</summary>
    /// <seealso cref="ISerializer"/>
    public class MsgPackObjectSerializer : ISerializer
    {
        private readonly System.Text.Encoding encoding;

        /// <summary>Initializes a new instance of the <see cref="MsgPackObjectSerializer"/> class.</summary>
        /// <param name="customSerializerRegistrar">The custom serializer registrar.</param>
        /// <param name="encoding">                 The encoding.</param>
        public MsgPackObjectSerializer(Action<SerializerRepository> customSerializerRegistrar = null, System.Text.Encoding encoding = null)
        {
            customSerializerRegistrar?.Invoke(SerializationContext.Default.Serializers);
            if (encoding == null)
            {
                this.encoding = System.Text.Encoding.UTF8;
            }
        }

        /// <inheritdoc/>
        public Task<object> DeserializeAsync(byte[] serializedObject)
        {
            return Task.Factory.StartNew(() => Deserialize(serializedObject));
        }

        /// <inheritdoc/>
        public T Deserialize<T>(byte[] serializedObject)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(encoding.GetString(serializedObject), typeof(T));
            }
            var serializer = MessagePackSerializer.Get<T>();
            using var byteStream = new MemoryStream(serializedObject);
            return serializer.Unpack(byteStream);
        }

        /// <inheritdoc/>
        public Task<T> DeserializeAsync<T>(byte[] serializedObject)
        {
            return Task.Factory.StartNew(() => Deserialize<T>(serializedObject));
        }

        /// <inheritdoc/>
        public byte[] Serialize(object item)
        {
            if (item is string)
            {
                return encoding.GetBytes(item.ToString());
            }
            var serializer = MessagePackSerializer.Get(item.GetType());
            using var byteStream = new MemoryStream();
            serializer.Pack(byteStream, item);
            return byteStream.ToArray();
        }

        /// <inheritdoc/>
        public Task<byte[]> SerializeAsync(object item)
        {
            return Task.Factory.StartNew(() => Serialize(item));
        }

        /// <inheritdoc/>
        public object Deserialize(byte[] serializedObject)
        {
            return Deserialize<object>(serializedObject);
        }
    }
}
