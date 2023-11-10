namespace StackExchange.Redis.Extensions.Utf8Json
{
    using System.Threading.Tasks;
    using StackExchange.Redis.Extensions.Core;

    /// <summary>An UTF 8 JSON serializer.</summary>
    /// <seealso cref="ISerializer"/>
    public class Utf8JsonSerializer : ISerializer
    {
        /// <summary>Initializes a new instance of the <see cref="Utf8JsonSerializer"/> class.</summary>
        public Utf8JsonSerializer()
        {
        }

        /// <inheritdoc/>
        public byte[] Serialize(object item)
        {
            return global::Utf8Json.JsonSerializer.Serialize(item);
        }

        /// <inheritdoc/>
        public Task<byte[]> SerializeAsync(object item)
        {
            return Task.FromResult(global::Utf8Json.JsonSerializer.Serialize(item));
        }

        /// <inheritdoc/>
        public object Deserialize(byte[] serializedObject)
        {
            return global::Utf8Json.JsonSerializer.Deserialize<object>(serializedObject);
        }

        /// <inheritdoc/>
        public Task<object> DeserializeAsync(byte[] serializedObject)
        {
            return Task.FromResult(global::Utf8Json.JsonSerializer.Deserialize<object>(serializedObject));
        }

        /// <inheritdoc/>
        public T Deserialize<T>(byte[] serializedObject)
        {
            return global::Utf8Json.JsonSerializer.Deserialize<T>(serializedObject);
        }

        /// <inheritdoc/>
        public Task<T> DeserializeAsync<T>(byte[] serializedObject)
        {
            return Task.FromResult(global::Utf8Json.JsonSerializer.Deserialize<T>(serializedObject));
        }
    }
}