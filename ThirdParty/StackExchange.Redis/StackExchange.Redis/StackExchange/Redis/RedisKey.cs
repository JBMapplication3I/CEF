namespace StackExchange.Redis
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents a key that can be stored in redis
    /// </summary>
    public struct RedisKey : IEquatable<RedisKey>
    {
        internal static readonly RedisKey[] EmptyArray = Array.Empty<RedisKey>();

        internal RedisKey(byte[] keyPrefix, object keyValue)
        {
            this.KeyPrefix = (keyPrefix != null && keyPrefix.Length == 0) ? null : keyPrefix;
            this.KeyValue = keyValue;
        }

        internal RedisKey AsPrefix()
        {
            return new RedisKey((byte[])this, null);
        }

        internal bool IsNull => KeyPrefix == null && KeyValue == null;

        internal bool IsEmpty
        {
            get
            {
                if (KeyPrefix != null)
                {
                    return false;
                }

                if (KeyValue == null)
                {
                    return true;
                }

                return KeyValue switch
                {
                    string => ((string)KeyValue).Length == 0,
                    _ => ((byte[])KeyValue).Length == 0
                };
            }
        }

        internal byte[] KeyPrefix { get; private set; }
        internal object KeyValue { get; private set; }

        /// <summary>
        /// Indicate whether two keys are not equal
        /// </summary>
        public static bool operator !=(RedisKey x, RedisKey y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Indicate whether two keys are not equal
        /// </summary>
        public static bool operator !=(string x, RedisKey y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Indicate whether two keys are not equal
        /// </summary>
        public static bool operator !=(byte[] x, RedisKey y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Indicate whether two keys are not equal
        /// </summary>
        public static bool operator !=(RedisKey x, string y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Indicate whether two keys are not equal
        /// </summary>
        public static bool operator !=(RedisKey x, byte[] y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Indicate whether two keys are equal
        /// </summary>
        public static bool operator ==(RedisKey x, RedisKey y)
        {
            return CompositeEquals(x.KeyPrefix, x.KeyValue, y.KeyPrefix, y.KeyValue);
        }

        /// <summary>
        /// Indicate whether two keys are equal
        /// </summary>
        public static bool operator ==(string x, RedisKey y)
        {
            return CompositeEquals(null, x, y.KeyPrefix, y.KeyValue);
        }

        /// <summary>
        /// Indicate whether two keys are equal
        /// </summary>
        public static bool operator ==(byte[] x, RedisKey y)
        {
            return CompositeEquals(null, x, y.KeyPrefix, y.KeyValue);
        }

        /// <summary>
        /// Indicate whether two keys are equal
        /// </summary>
        public static bool operator ==(RedisKey x, string y)
        {
            return CompositeEquals(x.KeyPrefix, x.KeyValue, null, y);
        }

        /// <summary>
        /// Indicate whether two keys are equal
        /// </summary>
        public static bool operator ==(RedisKey x, byte[] y)
        {
            return CompositeEquals(x.KeyPrefix, x.KeyValue, null, y);
        }

        /// <summary>
        /// See Object.Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is RedisKey other)
            {
                return CompositeEquals(this.KeyPrefix, this.KeyValue, other.KeyPrefix, other.KeyValue);
            }
            if (obj is string || obj is byte[])
            {
                return CompositeEquals(this.KeyPrefix, this.KeyValue, null, obj);
            }
            return false;
        }

        /// <summary>
        /// Indicate whether two keys are equal
        /// </summary>
        public bool Equals(RedisKey other)
        {
            return CompositeEquals(this.KeyPrefix, this.KeyValue, other.KeyPrefix, other.KeyValue);
        }

        private static bool CompositeEquals(byte[] keyPrefix0, object keyValue0, byte[] keyPrefix1, object keyValue1)
        {
            if (RedisValue.Equals(keyPrefix0, keyPrefix1))
            {
                if (keyValue0 == keyValue1)
                {
                    return true; // ref equal
                }

                if (keyValue0 == null || keyValue1 == null)
                {
                    return false; // null vs non-null
                }

                if (keyValue0 is string @string && keyValue1 is string string1)
                {
                    return @string == string1;
                }

                if (keyValue0 is byte[] v && keyValue1 is byte[] v1)
                {
                    return RedisValue.Equals(v, v1);
                }
            }

            return RedisValue.Equals(ConcatenateBytes(keyPrefix0, keyValue0, null), ConcatenateBytes(keyPrefix1, keyValue1, null));
        }

        /// <summary>
        /// See Object.GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int chk0 = KeyPrefix == null ? 0 : RedisValue.GetHashCode(this.KeyPrefix),
                chk1 = KeyValue is string ? KeyValue.GetHashCode() : RedisValue.GetHashCode((byte[])KeyValue);

            return unchecked((17 * chk0) + chk1);
        }

        /// <summary>
        /// Obtains a string representation of the key
        /// </summary>
        public override string ToString()
        {
            return ((string)this) ?? "(null)";
        }

        internal RedisValue AsRedisValue()
        {
            return (byte[])this;
        }

        internal void AssertNotNull()
        {
            if (IsNull)
            {
                throw new ArgumentException("A null key is not valid in this context");
            }
        }

        /// <summary>
        /// Create a key from a String
        /// </summary>
        public static implicit operator RedisKey(string key)
        {
            if (key == null)
            {
                return default;
            }

            return new RedisKey(null, key);
        }
        /// <summary>
        /// Create a key from a Byte[]
        /// </summary>
        public static implicit operator RedisKey(byte[] key)
        {
            if (key == null)
            {
                return default;
            }

            return new RedisKey(null, key);
        }
        /// <summary>
        /// Obtain the key as a Byte[]
        /// </summary>
        public static implicit operator byte[](RedisKey key)
        {
            return ConcatenateBytes(key.KeyPrefix, key.KeyValue, null);
        }
        /// <summary>
        /// Obtain the key as a String
        /// </summary>
        public static implicit operator string(RedisKey key)
        {
            byte[] arr;
            if (key.KeyPrefix == null)
            {
                if (key.KeyValue == null)
                {
                    return null;
                }
                if (key.KeyValue is string @string)
                {
                    return @string;
                }
                arr = (byte[])key.KeyValue;
            }
            else
            {
                arr = (byte[])key;
            }
            if (arr == null)
            {
                return null;
            }
            try
            {
                return Encoding.UTF8.GetString(arr);
            }
            catch
            {
                return BitConverter.ToString(arr);
            }

        }

        /// <summary>
        /// Concatenate two keys
        /// </summary>
        [Obsolete("Use Append instead")]
        public static RedisKey operator +(RedisKey x, RedisKey y)
        {
            return new RedisKey(ConcatenateBytes(x.KeyPrefix, x.KeyValue, y.KeyPrefix), y.KeyValue);
        }

        internal static RedisKey WithPrefix(byte[] prefix, RedisKey value)
        {
            if (prefix == null || prefix.Length == 0)
            {
                return value;
            }

            if (value.KeyPrefix == null)
            {
                return new RedisKey(prefix, value.KeyValue);
            }

            if (value.KeyValue == null)
            {
                return new RedisKey(prefix, value.KeyPrefix);
            }

            // two prefixes; darn
            byte[] copy = new byte[prefix.Length + value.KeyPrefix.Length];
            Buffer.BlockCopy(prefix, 0, copy, 0, prefix.Length);
            Buffer.BlockCopy(value.KeyPrefix, 0, copy, prefix.Length, value.KeyPrefix.Length);
            return new RedisKey(copy, value.KeyValue);
        }

        internal static byte[] ConcatenateBytes(byte[] a, object b, byte[] c)
        {
            if ((a == null || a.Length == 0) && (c == null || c.Length == 0))
            {
                if (b == null)
                {
                    return null;
                }
                if (b is string @string)
                {
                    return Encoding.UTF8.GetBytes(@string);
                }
                return (byte[])b;
            }

            int aLen = a?.Length ?? 0;
            int bLen = b == null
                ? 0
                : (b is string @string1
                    ? Encoding.UTF8.GetByteCount(@string1)
                    : ((byte[])b).Length);
            int cLen = c?.Length ?? 0;

            byte[] result = new byte[aLen + bLen + cLen];
            if (aLen != 0)
            {
                Buffer.BlockCopy(a, 0, result, 0, aLen);
            }

            if (bLen != 0)
            {
                if (b is string s)
                {
                    Encoding.UTF8.GetBytes(s, 0, s.Length, result, aLen);
                }
                else
                {
                    Buffer.BlockCopy((byte[])b, 0, result, aLen, bLen);
                }
            }
            if (cLen != 0)
            {
                Buffer.BlockCopy(c, 0, result, aLen + bLen, cLen);
            }

            return result;
        }

        /// <summary>
        /// Prepends p to this RedisKey, returning a new RedisKey.
        ///
        /// Avoids some allocations if possible, repeated Prepend/Appends make
        /// it less possible.
        /// </summary>
        public RedisKey Prepend(RedisKey p)
        {
            return WithPrefix(p, this);
        }

        /// <summary>
        /// Appends p to this RedisKey, returning a new RedisKey.
        ///
        /// Avoids some allocations if possible, repeated Prepend/Appends make
        /// it less possible.
        /// </summary>
        public RedisKey Append(RedisKey p)
        {
            return WithPrefix(this, p);
        }
    }
}
