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
    public class SafeDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IDictionary<TKey, TValue>
    {
        /// <summary>
        ///
        /// </summary>
        public SafeDictionary()
        {
        }

        protected SafeDictionary( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dictionary"></param>
        public SafeDictionary( SafeDictionary<TKey, TValue> dictionary ) : base(dictionary)
        {
        }

        /// <summary>
        ///
        /// </summary>
        public SafeDictionary<TKey, TValue> CopyForEnum => new SafeDictionary<TKey, TValue>( this );

        /// <summary>
        ///
        /// </summary>
        public new IEqualityComparer<TKey> Comparer
        {
            get
            {
                lock ( this )
                {
                    return base.Comparer;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public new int Count
        {
            get
            {
                lock ( this )
                {
                    return base.Count;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public new Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get
            {
                lock ( this )
                {
                    return base.Keys;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public new Dictionary<TKey, TValue>.ValueCollection Values
        {
            get
            {
                lock ( this )
                {
                    return base.Values;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new TValue this[TKey key]
        {
            get
            {
                lock ( this )
                {
                    TValue ret;
                    TryGetValue( key, out ret );
                    return ret;
                }
            }
            set
            {
                lock ( this )
                {
                    base[key] = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public new void Add( TKey key, TValue value )
        {
            lock ( this )
            {
                base.Add( key, value );
            }
        }

        /// <summary>
        ///
        /// </summary>
        public new void Clear()
        {
            lock ( this )
            {
                base.Clear();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new bool ContainsKey( TKey key )
        {
            lock ( this )
            {
                return base.ContainsKey( key );
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public new bool ContainsValue( TValue value )
        {
            lock ( this )
            {
                return base.ContainsValue( value );
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public new Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            lock ( this )
            {
                return base.GetEnumerator();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new bool Remove( TKey key )
        {
            lock ( this )
            {
                return base.Remove( key );
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public new bool TryGetValue( TKey key, out TValue value )
        {
            lock ( this )
            {
                return base.TryGetValue( key, out value );
            }
        }


    }
}
