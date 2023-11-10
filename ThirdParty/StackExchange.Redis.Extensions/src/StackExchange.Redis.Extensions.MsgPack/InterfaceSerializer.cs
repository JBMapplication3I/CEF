namespace StackExchange.Redis.Extensions.MsgPack
{
    using System.Collections.Generic;
    using System.Linq;
    using global::MsgPack;
    using global::MsgPack.Serialization;

    /// <summary>An interface serializer.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <seealso cref="MessagePackSerializer{T}"/>
    public class InterfaceSerializer<T> : MessagePackSerializer<T>
    {
        private readonly Dictionary<string, MessagePackSerializer> serializers;

        /// <summary>Initializes a new instance of the <see cref="InterfaceSerializer{T}"/> class.</summary>
        public InterfaceSerializer()
            : this(SerializationContext.Default)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="InterfaceSerializer{T}"/> class.</summary>
        /// <param name="context">The context.</param>
        public InterfaceSerializer(SerializationContext context)
            : base(context)
        {
            serializers = new Dictionary<string, MessagePackSerializer>();
            // Get all types that implement T interface
            var implementingTypes = System.Reflection.Assembly
                .GetExecutingAssembly()
                .DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(T)));
            // Create serializer for each type and store it in dictionary
            foreach (var type in implementingTypes)
            {
                var key = type.Name;
                var value = Get(type, context);
                serializers.Add(key, value);
            }
        }

        /// <inheritdoc/>
        protected override void PackToCore(Packer packer, T objectTree)
        {
            string typeName = objectTree.GetType().Name;
            // Find matching serializer
            if (!serializers.TryGetValue(typeName, out var serializer))
            {
                throw SerializationExceptions.NewTypeCannotSerialize(typeof(T));
            }
            packer.PackArrayHeader(2);             // Two-element array:
            packer.PackString(typeName);           //  0: Type name
            serializer.PackTo(packer, objectTree); //  1: Packed object
        }

        /// <inheritdoc/>
        protected override T UnpackFromCore(Unpacker unpacker)
        {

            // Read type name and packed object
            if (!(unpacker.ReadString(out var typeName) && unpacker.Read()))
            {
                throw new System.IO.EndOfStreamException();
            }
            // Find matching serializer
            if (!serializers.TryGetValue(typeName, out var serializer))
            {
                throw SerializationExceptions.NewTypeCannotDeserialize(typeof(T));
            }
            // Unpack and return
            return (T)serializer.UnpackFrom(unpacker);
        }
    }
}
