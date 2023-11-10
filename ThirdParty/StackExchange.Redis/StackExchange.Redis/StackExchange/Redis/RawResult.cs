namespace StackExchange.Redis
{
    using System;
    using System.Text;

    internal struct RawResult
    {
        public static readonly RawResult EmptyArray = new(Array.Empty<RawResult>());
        public static readonly RawResult Nil = new();
        private static readonly byte[] emptyBlob = Array.Empty<byte>();
        private readonly int offset, count;
        private readonly Array arr;
        public RawResult(ResultType resultType, byte[] buffer, int offset, int count)
        {
            switch (resultType)
            {
                case ResultType.SimpleString:
                case ResultType.Error:
                case ResultType.Integer:
                case ResultType.BulkString:
                break;
                default:
                throw new ArgumentOutOfRangeException(nameof(resultType));
            }
            Type = resultType;
            arr = buffer;
            this.offset = offset;
            this.count = count;
        }

        public RawResult(RawResult[] arr)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr));
            }

            Type = ResultType.MultiBulk;
            offset = 0;
            count = arr.Length;
            this.arr = arr;
        }

        public bool HasValue => Type != ResultType.None;

        public bool IsError => Type == ResultType.Error;

        public ResultType Type { get; }

        internal bool IsNull => arr == null;

        public override string ToString()
        {
            if (arr == null)
            {
                return "(null)";
            }
            return Type switch
            {
                ResultType.SimpleString or ResultType.Integer or ResultType.Error => $"{Type}: {GetString()}",
                ResultType.BulkString => $"{Type}: {count} bytes",
                ResultType.MultiBulk => $"{Type}: {count} items",
                _ => "(unknown)",
            };
        }
        internal RedisChannel AsRedisChannel(byte[] channelPrefix, RedisChannel.PatternMode mode)
        {
            switch (Type)
            {
                case ResultType.SimpleString:
                case ResultType.BulkString:
                if (channelPrefix == null)
                {
                    return new RedisChannel(GetBlob(), mode);
                }
                if (AssertStarts(channelPrefix))
                {
                    var src = (byte[])arr;

                    byte[] copy = new byte[count - channelPrefix.Length];
                    Buffer.BlockCopy(src, offset + channelPrefix.Length, copy, 0, copy.Length);
                    return new RedisChannel(copy, mode);
                }
                return default;
                default:
                throw new InvalidCastException("Cannot convert to RedisChannel: " + Type);
            }
        }

        internal RedisKey AsRedisKey()
        {
            return Type switch
            {
                ResultType.SimpleString or ResultType.BulkString => (RedisKey)GetBlob(),
                _ => throw new InvalidCastException("Cannot convert to RedisKey: " + Type),
            };
        }
        internal RedisValue AsRedisValue()
        {
            switch (Type)
            {
                case ResultType.Integer:
                long i64;
                if (TryGetInt64(out i64))
                {
                    return (RedisValue)i64;
                }

                break;
                case ResultType.SimpleString:
                case ResultType.BulkString:
                return (RedisValue)GetBlob();
            }
            throw new InvalidCastException("Cannot convert to RedisValue: " + Type);
        }

        internal unsafe bool IsEqual(byte[] expected)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (expected.Length != count)
            {
                return false;
            }

            if (arr is not byte[] actual)
            {
                return false;
            }

            int octets = count / 8, spare = count % 8;
            fixed (byte* actual8 = &actual[offset])
            fixed (byte* expected8 = expected)
            {
                long* actual64 = (long*)actual8;
                long* expected64 = (long*)expected8;

                for (int i = 0; i < octets; i++)
                {
                    if (actual64[i] != expected64[i])
                    {
                        return false;
                    }
                }
                int index = count - spare;
                while (spare-- != 0)
                {
                    if (actual8[index] != expected8[index])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        internal bool AssertStarts(byte[] expected)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (expected.Length > count)
            {
                return false;
            }

            if (arr is not byte[] actual)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i] != actual[offset + i])
                {
                    return false;
                }
            }
            return true;
        }
        internal byte[] GetBlob()
        {
            var src = (byte[])arr;
            if (src == null)
            {
                return null;
            }

            if (count == 0)
            {
                return emptyBlob;
            }

            byte[] copy = new byte[count];
            Buffer.BlockCopy(src, offset, copy, 0, count);
            return copy;
        }

        internal bool GetBoolean()
        {
            if (count != 1)
            {
                throw new InvalidCastException();
            }

            if (arr is not byte[] actual)
            {
                throw new InvalidCastException();
            }

            return actual[offset] switch
            {
                (byte)'1' => true,
                (byte)'0' => false,
                _ => throw new InvalidCastException(),
            };
        }

        internal RawResult[] GetItems()
        {
            return (RawResult[])arr;
        }

        internal RedisKey[] GetItemsAsKeys()
        {
            RawResult[] items = GetItems();
            if (items == null)
            {
                return null;
            }
            else if (items.Length == 0)
            {
                return RedisKey.EmptyArray;
            }
            else
            {
                var arr = new RedisKey[items.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = items[i].AsRedisKey();
                }
                return arr;
            }
        }

        internal RedisValue[] GetItemsAsValues()
        {
            RawResult[] items = GetItems();
            if (items == null)
            {
                return null;
            }
            else if (items.Length == 0)
            {
                return RedisValue.EmptyArray;
            }
            else
            {
                var arr = new RedisValue[items.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = items[i].AsRedisValue();
                }
                return arr;
            }
        }
        static readonly string[] NilStrings = Array.Empty<string>();
        internal string[] GetItemsAsStrings()
        {
            RawResult[] items = GetItems();
            if (items == null)
            {
                return null;
            }
            else if (items.Length == 0)
            {
                return NilStrings;
            }
            else
            {
                var arr = new string[items.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = (string)(items[i].AsRedisValue());
                }
                return arr;
            }
        }
        internal GeoPosition? GetItemsAsGeoPosition()
        {
            RawResult[] items = GetItems();
            if (items == null || items.Length == 0)
            {
                return null;
            }

            var coords = items[0].GetArrayOfRawResults();
            if (coords == null)
            {
                return null;
            }
            return new GeoPosition((double)coords[0].AsRedisValue(), (double)coords[1].AsRedisValue());
        }
        internal GeoPosition?[] GetItemsAsGeoPositionArray()
        {
            RawResult[] items = GetItems();
            if (items == null)
            {
                return null;
            }
            else if (items.Length == 0)
            {
                return Array.Empty<GeoPosition?>();
            }
            else
            {
                var arr = new GeoPosition?[items.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    RawResult[] item = items[i].GetArrayOfRawResults();
                    if (item == null)
                    {
                        arr[i] = null;
                    }
                    else
                    {
                        arr[i] = new GeoPosition((double)item[0].AsRedisValue(), (double)item[1].AsRedisValue());
                    }
                }
                return arr;
            }
        }

        internal RawResult[] GetItemsAsRawResults()
        {
            return GetItems();
        }


        // returns an array of RawResults
        internal RawResult[] GetArrayOfRawResults()
        {
            if (arr == null)
            {
                return null;
            }
            else if (arr.Length == 0)
            {
                return Array.Empty<RawResult>();
            }
            else
            {
                var rawResultArray = new RawResult[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    var rawResult = (RawResult)arr.GetValue(i);
                    rawResultArray.SetValue(rawResult, i);
                }
                return rawResultArray;
            }
        }

        internal string GetString()
        {
            if (arr == null)
            {
                return null;
            }

            var blob = (byte[])arr;
            if (blob.Length == 0)
            {
                return "";
            }

            return Encoding.UTF8.GetString(blob, offset, count);
        }

        internal bool TryGetDouble(out double val)
        {
            if (arr == null)
            {
                val = 0;
                return false;
            }
            if (TryGetInt64(out var i64))
            {
                val = i64;
                return true;
            }
            return Format.TryParseDouble(GetString(), out val);
        }

        internal bool TryGetInt64(out long value)
        {
            if (arr == null)
            {
                value = 0;
                return false;
            }
            return RedisValue.TryParseInt64(arr as byte[], offset, count, out value);
        }
    }
}

