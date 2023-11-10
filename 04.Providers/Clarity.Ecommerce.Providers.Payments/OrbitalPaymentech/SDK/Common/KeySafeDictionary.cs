#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace JPMC.MSDK.Common
{
    /// <summary>
    /// SafeDictionary extends the normal Dictionary by returning null if the key does not exist.
    /// </summary>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    [System.Runtime.InteropServices.ComVisible(false)]
    [Serializable]
    public class KeySafeDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IDictionary<TKey, TValue>
    {
        /// <summary>
        ///
        /// </summary>
        public KeySafeDictionary()
        {
        }
        protected KeySafeDictionary( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="dictionary"></param>
        public KeySafeDictionary( IDictionary<TKey, TValue> dictionary ) : base(dictionary)
        {
        }

        /// <summary>
        /// Get will return return null if it is not found.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new TValue this[TKey key]
        {
            get
            {
                TValue ret;
                TryGetValue(key, out ret);
                return ret;
            }
            set => base[key] = value;
        }
    }
}
