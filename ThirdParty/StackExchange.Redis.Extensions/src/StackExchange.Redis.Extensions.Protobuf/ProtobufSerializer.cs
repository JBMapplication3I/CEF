namespace StackExchange.Redis.Extensions.Protobuf
{
    using System.IO;
    using System.Threading.Tasks;
    using ProtoBuf;
    using StackExchange.Redis.Extensions.Core;

    /// <summary>A protobuf serializer.</summary>
    /// <seealso cref="ISerializer"/>
    public class ProtobufSerializer : ISerializer
    {
        /// <inheritdoc/>
        public byte[] Serialize(object item)
        {
            using var ms = new MemoryStream();
            Serializer.Serialize(ms, item);
            return ms.ToArray();
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

        /// <inheritdoc/>
        public Task<object> DeserializeAsync(byte[] serializedObject)
        {
            return Task.Factory.StartNew(() => Deserialize(serializedObject));
        }

        /// <inheritdoc/>
        public T Deserialize<T>(byte[] serializedObject)
        {
            using var ms = new MemoryStream(serializedObject);
            return Serializer.Deserialize<T>(ms);
        }

        /// <inheritdoc/>
        public Task<T> DeserializeAsync<T>(byte[] serializedObject)
        {
            return Task.Factory.StartNew(() => Deserialize<T>(serializedObject));
        }
    }
}