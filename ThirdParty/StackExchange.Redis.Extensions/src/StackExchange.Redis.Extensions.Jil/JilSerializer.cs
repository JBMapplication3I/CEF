namespace StackExchange.Redis.Extensions.Jil
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using global::Jil;
    using StackExchange.Redis.Extensions.Core;

    /// <summary>Jil implementation of <see cref="ISerializer"/></summary>
    /// <seealso cref="ISerializer"/>
    public class JilSerializer : ISerializer
    {
        // TODO: May make this configurable in the future.

        /// <summary>(Immutable) Encoding to use to convert string to byte[] and the other way around.</summary>
        private static readonly Encoding encoding = Encoding.UTF8;

        /// <summary>Default constructor for Jil serializer.</summary>
        public JilSerializer()
            : this(new Options(
                prettyPrint: true,
                excludeNulls: false,
                jsonp: false,
                dateFormat:
                DateTimeFormat.ISO8601,
                includeInherited: true,
                unspecifiedDateTimeKindBehavior: UnspecifiedDateTimeKindBehavior.IsLocal))
        {
        }

        /// <summary>Constructor for Jil serializer.</summary>
        /// <param name="options">Options for controlling the operation.</param>
        public JilSerializer(Options options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            JSON.SetDefaultOptions(options);
        }

        /// <inheritdoc/>
        public byte[] Serialize(object item)
        {
            var jsonString = JSON.Serialize(item);
            return encoding.GetBytes(jsonString);
        }

        /// <inheritdoc/>
        public Task<byte[]> SerializeAsync(object item)
        {
            return Task.Factory.StartNew(() => Serialize(item));
        }

        /// <inheritdoc/>
        public object Deserialize(byte[] serializedObject)
        {
            var jsonString = encoding.GetString(serializedObject);
            return JSON.Deserialize(jsonString, typeof(object));
        }

        /// <inheritdoc/>
        public Task<object> DeserializeAsync(byte[] serializedObject)
        {
            return Task.Factory.StartNew(() => Deserialize(serializedObject));
        }

        /// <inheritdoc/>
        public T Deserialize<T>(byte[] serializedObject)
        {
            var jsonString = encoding.GetString(serializedObject);
            return JSON.Deserialize<T>(jsonString);
        }

        /// <inheritdoc/>
        public Task<T> DeserializeAsync<T>(byte[] serializedObject)
        {
            return Task.Factory.StartNew(() => Deserialize<T>(serializedObject));
        }
    }
}