namespace StackExchange.Redis.Extensions.Newtonsoft
{
    using System.Text;
    using System.Threading.Tasks;
    using global::Newtonsoft.Json;
    using StackExchange.Redis.Extensions.Core;

    /// <summary>JSon.Net implementation of <see cref="ISerializer"/></summary>
    /// <seealso cref="ISerializer"/>
    public class NewtonsoftSerializer : ISerializer
    {
        // TODO: May make this configurable in the future.

        /// <summary>(Immutable)
        /// Encoding to use to convert string to byte[] and the other way around.</summary>
        private static readonly Encoding encoding = Encoding.UTF8;

        private readonly JsonSerializerSettings settings;

        /// <summary>Initializes a new instance of the <see cref="NewtonsoftSerializer"/> class.</summary>
        public NewtonsoftSerializer() : this(null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="NewtonsoftSerializer"/> class.</summary>
        /// <param name="settings">The settings.</param>
        public NewtonsoftSerializer(JsonSerializerSettings settings)
        {
            this.settings = settings ?? new JsonSerializerSettings();
        }

        /// <inheritdoc/>
        public byte[] Serialize(object item)
        {
            var type = item?.GetType();
            var jsonString = JsonConvert.SerializeObject(item, type, settings);
            return encoding.GetBytes(jsonString);
        }

        /// <inheritdoc/>
        public Task<byte[]> SerializeAsync(object item)
        {
            var type = item?.GetType();
            var jsonString = JsonConvert.SerializeObject(item, type, settings);
            return Task.FromResult(encoding.GetBytes(jsonString));
        }

        /// <inheritdoc/>
        public object Deserialize(byte[] serializedObject)
        {
            var jsonString = encoding.GetString(serializedObject);
            return JsonConvert.DeserializeObject(jsonString, typeof(object));
        }

        /// <inheritdoc/>
        public Task<object> DeserializeAsync(byte[] serializedObject)
        {
            return Task.FromResult(Deserialize(serializedObject));
        }

        /// <inheritdoc/>
        public T Deserialize<T>(byte[] serializedObject)
        {
            var jsonString = encoding.GetString(serializedObject);
            return JsonConvert.DeserializeObject<T>(jsonString, settings);
        }

        /// <inheritdoc/>
        public Task<T> DeserializeAsync<T>(byte[] serializedObject)
        {
            return Task.FromResult(Deserialize<T>(serializedObject));
        }
    }
}
