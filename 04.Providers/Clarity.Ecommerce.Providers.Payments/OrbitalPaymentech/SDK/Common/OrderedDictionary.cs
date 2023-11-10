#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>OrderedDictionary extends the SafeDictionary by preserving the order in which keys are returned.</summary>
    /// <typeparam name="TKey">  The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <seealso cref="SafeDictionary{TKey, TValue}"/>
    /// <seealso cref="IDictionary{TKey, TValue}"/>
    [System.Runtime.InteropServices.ComVisible(false), Serializable]
    public class OrderedDictionary<TKey, TValue> : SafeDictionary<TKey, TValue>, IDictionary<TKey, TValue>
    {
        public List<TKey> OrderedKeys { get; private set; }

        public OrderedDictionary()
        {
            OrderedKeys = new List<TKey>();
        }

        protected OrderedDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            OrderedKeys = new List<TKey>();
        }

        public OrderedDictionary(SafeDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
            OrderedKeys = new List<TKey>();
        }

        public new void Add(TKey key, TValue value)
        {
            lock (this)
            {
                OrderedKeys.Add(key);
                base.Add(key, value);
            }
        }

        public TValue Get(int index)
        {
            return this[OrderedKeys[index]];
        }
    }
}
