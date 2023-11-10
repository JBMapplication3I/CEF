// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.HostString
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System;
    using System.Globalization;

    /// <summary>Represents the host portion of a Uri can be used to construct Uri's properly formatted and encoded
    /// for use in HTTP headers.</summary>
    public struct HostString : IEquatable<HostString>
    {
        /// <summary>Returns the original value from the constructor.</summary>
        /// <value>The value.</value>
        public string Value { get; }

        /// <summary>Creates a new HostString without modification. The value should be Unicode rather than punycode, and
        /// may have a port. IPv4 and IPv6 addresses are also allowed, and also may have ports.</summary>
        /// <param name="value">.</param>
        public HostString(string value)
        {
            Value = value;
        }

        /// <summary>Compares the equality of the Value property, ignoring case.</summary>
        /// <param name="other">.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public bool Equals(HostString other)
        {
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Compares against the given object only if it is a HostString.</summary>
        /// <param name="obj">.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is HostString))
            {
                return false;
            }
            return Equals((HostString)obj);
        }

        /// <summary>Creates a new HostString from the given uri component. Any punycode will be converted to Unicode.</summary>
        /// <param name="uriComponent">.</param>
        /// <returns>A HostString.</returns>
        public static HostString FromUriComponent(string uriComponent)
        {
            if (!string.IsNullOrEmpty(uriComponent) && uriComponent.IndexOf('[') < 0)
            {
                var num = uriComponent.IndexOf(':');
                var num1 = num;
                if ((num < 0 || num1 >= uriComponent.Length - 1 || uriComponent.IndexOf(':', num1 + 1) < 0)
                    && uriComponent.Contains("xn--", StringComparison.Ordinal))
                {
                    if (num1 < 0)
                    {
                        uriComponent = new IdnMapping().GetUnicode(uriComponent);
                    }
                    else
                    {
                        var str = uriComponent[num1..];
                        uriComponent = string.Concat(new IdnMapping().GetUnicode(uriComponent, 0, num1), str);
                    }
                }
            }
            return new HostString(uriComponent);
        }

        /// <summary>Creates a new HostString from the host and port of the give Uri instance. Punycode will be converted
        /// to Unicode.</summary>
        /// <param name="uri">.</param>
        /// <returns>A HostString.</returns>
        public static HostString FromUriComponent(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return new HostString(
                uri.GetComponents(
                    UriComponents.Host
                    | UriComponents.StrongPort
                    | UriComponents.NormalizedHost
                    | UriComponents.HostAndPort,
                    UriFormat.Unescaped));
        }

        /// <summary>Gets a hash code for the value.</summary>
        /// <returns>A hash code for this HostString.</returns>
        public override int GetHashCode()
        {
            if (Value == null)
            {
                return 0;
            }
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        }

        /// <summary>Compares the two instances for equality.</summary>
        /// <param name="left"> .</param>
        /// <param name="right">.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator ==(HostString left, HostString right)
        {
            return left.Equals(right);
        }

        /// <summary>Compares the two instances for inequality.</summary>
        /// <param name="left"> .</param>
        /// <param name="right">.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator !=(HostString left, HostString right)
        {
            return !left.Equals(right);
        }

        /// <summary>Returns the value as normalized by ToUriComponent().</summary>
        /// <returns>A string that represents this HostString.</returns>
        public override string ToString()
        {
            return ToUriComponent();
        }

        /// <summary>Returns the value properly formatted and encoded for use in a URI in a HTTP header. Any Unicode is
        /// converted to punycode. IPv6 addresses will have brackets added if they are missing.</summary>
        /// <returns>This HostString as a string.</returns>
        public string ToUriComponent()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return string.Empty;
            }
            if (Value.Contains('['))
            {
                return Value;
            }
            var num = Value.IndexOf(':');
            var num1 = num;
            if (num >= 0 && num1 < Value.Length - 1 && Value.IndexOf(':', num1 + 1) >= 0)
            {
                return string.Concat("[", Value, "]");
            }
            if (num1 < 0)
            {
                return new IdnMapping().GetAscii(Value);
            }
            var str = Value[num1..];
            return string.Concat(new IdnMapping().GetAscii(Value, 0, num1), str);
        }
    }
}
