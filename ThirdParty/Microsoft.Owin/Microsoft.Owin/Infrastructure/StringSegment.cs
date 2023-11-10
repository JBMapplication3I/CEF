namespace Microsoft.Owin.Infrastructure
{
    using System;
    using System.CodeDom.Compiler;

    [GeneratedCode("App_Packages", "")]
    internal struct StringSegment : IEquatable<StringSegment>
    {
        /// <summary>Gets the buffer.</summary>
        /// <value>The buffer.</value>
        public string Buffer { get; }

        /// <summary>Gets the number of.</summary>
        /// <value>The count.</value>
        public int Count { get; }

        /// <summary>Gets a value indicating whether this StringSegment has value.</summary>
        /// <value>True if this StringSegment has value, false if not.</value>
        public bool HasValue
        {
            get
            {
                if (Offset == -1 || Count == 0)
                {
                    return false;
                }
                return Buffer != null;
            }
        }

        /// <summary>Gets the offset.</summary>
        /// <value>The offset.</value>
        public int Offset { get; }

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                if (Offset == -1)
                {
                    return null;
                }
                return Buffer.Substring(Offset, Count);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="Microsoft.Owin.Infrastructure.StringSegment" />
        /// struct.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        public StringSegment(string buffer, int offset, int count)
        {
            Buffer = buffer;
            Offset = offset;
            Count = count;
        }

        /// <summary>Ends with.</summary>
        /// <param name="text">          The text.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool EndsWith(string text, StringComparison comparisonType)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            var length = text.Length;
            if (!HasValue || Count < length)
            {
                return false;
            }
            return string.Compare(Buffer, Offset + Count - length, text, 0, length, comparisonType) == 0;
        }

        /// <inheritdoc/>
        public bool Equals(StringSegment other)
        {
            if (!string.Equals(Buffer, other.Buffer) || Offset != other.Offset)
            {
                return false;
            }
            return Count == other.Count;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is StringSegment))
            {
                return false;
            }
            return Equals((StringSegment)obj);
        }

        /// <summary>Tests if two string objects are considered equal.</summary>
        /// <param name="text">          String to be compared.</param>
        /// <param name="comparisonType">String comparison to be compared.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public bool Equals(string text, StringComparison comparisonType)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            var length = text.Length;
            if (!HasValue || Count != length)
            {
                return false;
            }
            return string.Compare(Buffer, Offset, text, 0, length, comparisonType) == 0;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return ((((Buffer != null ? Buffer.GetHashCode() : 0) * 397) ^ Offset) * 397) ^ Count;
        }

        /// <summary>Equality operator.</summary>
        /// <param name="left"> The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator ==(StringSegment left, StringSegment right)
        {
            return left.Equals(right);
        }

        /// <summary>Inequality operator.</summary>
        /// <param name="left"> The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator !=(StringSegment left, StringSegment right)
        {
            return !left.Equals(right);
        }

        /// <summary>Starts with.</summary>
        /// <param name="text">          The text.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool StartsWith(string text, StringComparison comparisonType)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            var length = text.Length;
            if (!HasValue || Count < length)
            {
                return false;
            }
            return string.Compare(Buffer, Offset, text, 0, length, comparisonType) == 0;
        }

        /// <summary>Subsegments.</summary>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>A StringSegment.</returns>
        public StringSegment Subsegment(int offset, int length)
        {
            return new StringSegment(Buffer, Offset + offset, length);
        }

        /// <summary>Substrings.</summary>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public string Substring(int offset, int length)
        {
            return Buffer.Substring(Offset + offset, length);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value ?? string.Empty;
        }
    }
}
