namespace Microsoft.Owin.Infrastructure
{
    using System;
    using System.CodeDom.Compiler;

    [GeneratedCode("App_Packages", "")]
    internal struct HeaderSegment : IEquatable<HeaderSegment>
    {
        /// <summary>Gets the data.</summary>
        /// <value>The data.</value>
        public StringSegment Data { get; }

        /// <summary>Gets the formatting.</summary>
        /// <value>The formatting.</value>
        public StringSegment Formatting { get; }

        /// <summary>Initializes a new instance of the <see cref="Microsoft.Owin.Infrastructure.HeaderSegment" />
        /// struct.</summary>
        /// <param name="formatting">The formatting.</param>
        /// <param name="data">      The data.</param>
        public HeaderSegment(StringSegment formatting, StringSegment data)
        {
            Formatting = formatting;
            Data = data;
        }

        /// <inheritdoc/>
        public bool Equals(HeaderSegment other)
        {
            if (!Formatting.Equals(other.Formatting))
            {
                return false;
            }
            return Data.Equals(other.Data);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is HeaderSegment))
            {
                return false;
            }
            return Equals((HeaderSegment)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (Formatting.GetHashCode() * 397) ^ Data.GetHashCode();
        }

        /// <summary>Equality operator.</summary>
        /// <param name="left"> The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator ==(HeaderSegment left, HeaderSegment right)
        {
            return left.Equals(right);
        }

        /// <summary>Inequality operator.</summary>
        /// <param name="left"> The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator !=(HeaderSegment left, HeaderSegment right)
        {
            return !left.Equals(right);
        }
    }
}
