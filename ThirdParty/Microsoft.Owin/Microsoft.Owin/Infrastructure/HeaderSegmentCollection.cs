// <copyright file="HeaderSegmentCollection.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the header segment collection class</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>Collection of header segments.</summary>
    [GeneratedCode("App_Packages", "")]
    internal struct HeaderSegmentCollection
        : IEnumerable<HeaderSegment>, IEquatable<HeaderSegmentCollection>
    {
        /// <summary>The headers.</summary>
        private readonly string[] headers;

        /// <summary>Initializes a new instance of the <see cref="HeaderSegmentCollection"/> struct.</summary>
        /// <param name="headers">The headers.</param>
        public HeaderSegmentCollection(string[] headers)
        {
            this.headers = headers;
        }

        /// <summary>Equality operator.</summary>
        /// <param name="left"> The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator ==(HeaderSegmentCollection left, HeaderSegmentCollection right)
        {
            return left.Equals(right);
        }

        /// <summary>Inequality operator.</summary>
        /// <param name="left"> The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator !=(HeaderSegmentCollection left, HeaderSegmentCollection right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc/>
        public bool Equals(HeaderSegmentCollection other)
        {
            return Equals(headers, other.headers);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is HeaderSegmentCollection))
            {
                return false;
            }
            return Equals((HeaderSegmentCollection)obj);
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns>The enumerator.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(headers);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            if (headers == null)
            {
                return 0;
            }
            return headers.GetHashCode();
        }

        /// <inheritdoc/>
        IEnumerator<HeaderSegment> IEnumerable<HeaderSegment>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>An enumerator.</summary>
        internal struct Enumerator : IEnumerator<HeaderSegment>
        {
            /// <summary>The headers.</summary>
            private readonly string[] headers;

            /// <summary>The index.</summary>
            private int index;

            /// <summary>The header.</summary>
            private string header;

            /// <summary>Length of the header.</summary>
            private int headerLength;

            /// <summary>The offset.</summary>
            private int offset;

            /// <summary>The leading start.</summary>
            private int leadingStart;

            /// <summary>The leading end.</summary>
            private int leadingEnd;

            /// <summary>The value start.</summary>
            private int valueStart;

            /// <summary>The value end.</summary>
            private int valueEnd;

            /// <summary>The trailing start.</summary>
            private int trailingStart;

            /// <summary>The mode.</summary>
            private Mode mode;

            /// <summary>The no headers.</summary>
            private static readonly string[] NoHeaders;

            /// <inheritdoc/>
            public HeaderSegment Current
                => new HeaderSegment(
                    new StringSegment(header, leadingStart, leadingEnd - leadingStart),
                    new StringSegment(header, valueStart, valueEnd - valueStart));

            /// <inheritdoc/>
            object IEnumerator.Current => Current;

            /// <summary>Initializes static members of the Microsoft.Owin.Infrastructure.HeaderSegmentCollection.Enumerator
            /// struct.</summary>
            static Enumerator()
            {
                NoHeaders = new string[0];
            }

            /// <summary>Initializes a new instance of the
            /// <see cref="Microsoft.Owin.Infrastructure.HeaderSegmentCollection.Enumerator" /> struct.</summary>
            /// <param name="headers">The headers.</param>
            public Enumerator(string[] headers)
            {
                this.headers = headers ?? NoHeaders;
                header = string.Empty;
                headerLength = -1;
                index = -1;
                offset = -1;
                leadingStart = -1;
                leadingEnd = -1;
                valueStart = -1;
                valueEnd = -1;
                trailingStart = -1;
                mode = Mode.Leading;
            }

            /// <inheritdoc/>
            public void Dispose() { }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                Attr attr;
                if (mode == Mode.Produce)
                {
                    leadingStart = trailingStart;
                    leadingEnd = -1;
                    valueStart = -1;
                    valueEnd = -1;
                    trailingStart = -1;
                    if (offset == headerLength && leadingStart != -1 && leadingStart != offset)
                    {
                        leadingEnd = offset;
                        return true;
                    }
                    mode = Mode.Leading;
                }
                if (offset == headerLength)
                {
                    index++;
                    offset = -1;
                    leadingStart = 0;
                    leadingEnd = -1;
                    valueStart = -1;
                    valueEnd = -1;
                    trailingStart = -1;
                    if (index == headers.Length)
                    {
                        return false;
                    }
                    header = headers[index] ?? string.Empty;
                    headerLength = header.Length;
                }
                do
                {
                    offset++;
                    var chr = offset == headerLength ? '\0' : header[offset];
                    if (char.IsWhiteSpace(chr))
                    {
                        attr = Attr.Whitespace;
                    }
                    else if (chr == '\"')
                    {
                        attr = Attr.Quote;
                    }
                    else
                    {
                        attr = chr == ',' || chr == 0 ? Attr.Delimiter : Attr.Value;
                    }
                    var attr1 = attr;
                    switch (mode)
                    {
                        case Mode.Leading:
                        {
                            switch (attr1)
                            {
                                case Attr.Value:
                                {
                                    leadingEnd = offset;
                                    valueStart = offset;
                                    mode = Mode.Value;
                                    continue;
                                }
                                case Attr.Quote:
                                {
                                    leadingEnd = offset;
                                    valueStart = offset;
                                    mode = Mode.ValueQuoted;
                                    continue;
                                }
                                case Attr.Delimiter:
                                {
                                    leadingEnd = offset;
                                    mode = Mode.Produce;
                                    continue;
                                }
                                default:
                                {
                                    continue;
                                }
                            }
                        }
                        case Mode.Value:
                        {
                            switch (attr1)
                            {
                                case Attr.Quote:
                                {
                                    mode = Mode.ValueQuoted;
                                    continue;
                                }
                                case Attr.Delimiter:
                                {
                                    valueEnd = offset;
                                    trailingStart = offset;
                                    mode = Mode.Produce;
                                    continue;
                                }
                                case Attr.Whitespace:
                                {
                                    valueEnd = offset;
                                    trailingStart = offset;
                                    mode = Mode.Trailing;
                                    continue;
                                }
                                default:
                                {
                                    continue;
                                }
                            }
                        }
                        case Mode.ValueQuoted:
                        {
                            switch (attr1)
                            {
                                case Attr.Quote:
                                {
                                    mode = Mode.Value;
                                    continue;
                                }
                                case Attr.Delimiter:
                                {
                                    if (chr != 0)
                                    {
                                        continue;
                                    }
                                    valueEnd = offset;
                                    trailingStart = offset;
                                    mode = Mode.Produce;
                                    continue;
                                }
                                default:
                                {
                                    continue;
                                }
                            }
                        }
                        case Mode.Trailing:
                        {
                            switch (attr1)
                            {
                                case Attr.Value:
                                {
                                    trailingStart = -1;
                                    valueEnd = -1;
                                    mode = Mode.Value;
                                    continue;
                                }
                                case Attr.Quote:
                                {
                                    trailingStart = -1;
                                    valueEnd = -1;
                                    mode = Mode.ValueQuoted;
                                    continue;
                                }
                                case Attr.Delimiter:
                                {
                                    mode = Mode.Produce;
                                    continue;
                                }
                                default:
                                {
                                    continue;
                                }
                            }
                        }
                        default:
                        {
                            continue;
                        }
                    }
                }
                while (mode != Mode.Produce);
                return true;
            }

            /// <inheritdoc/>
            public void Reset()
            {
                index = 0;
                offset = 0;
                leadingStart = 0;
                leadingEnd = 0;
                valueStart = 0;
                valueEnd = 0;
            }

            /// <summary>Values that represent Attributes.</summary>
            private enum Attr
            {
                /// <summary>An enum constant representing the value option.</summary>
                Value,

                /// <summary>An enum constant representing the quote option.</summary>
                Quote,

                /// <summary>An enum constant representing the delimiter option.</summary>
                Delimiter,

                /// <summary>An enum constant representing the whitespace option.</summary>
                Whitespace,
            }

            /// <summary>Values that represent modes.</summary>
            private enum Mode
            {
                /// <summary>An enum constant representing the leading option.</summary>
                Leading,

                /// <summary>An enum constant representing the value option.</summary>
                Value,

                /// <summary>An enum constant representing the value quoted option.</summary>
                ValueQuoted,

                /// <summary>An enum constant representing the trailing option.</summary>
                Trailing,

                /// <summary>An enum constant representing the produce option.</summary>
                Produce,
            }
        }
    }
}
