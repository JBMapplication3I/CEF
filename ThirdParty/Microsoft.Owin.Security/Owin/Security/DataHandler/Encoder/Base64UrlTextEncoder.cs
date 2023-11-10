// <copyright file="Base64UrlTextEncoder.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base 64 URL text encoder class</summary>
namespace Microsoft.Owin.Security.DataHandler.Encoder
{
    using System;

    /// <summary>A base 64 URL text encoder.</summary>
    /// <seealso cref="ITextEncoder"/>
    /// <seealso cref="ITextEncoder"/>
    public class Base64UrlTextEncoder : ITextEncoder
    {
        /// <summary>Decodes.</summary>
        /// <param name="text">The text.</param>
        /// <returns>A byte[].</returns>
        public byte[] Decode(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            return Convert.FromBase64String(Pad(text.Replace('-', '+').Replace('\u005F', '/')));
        }

        /// <summary>Encodes the given data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>A string.</returns>
        public string Encode(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            return Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '\u005F');
        }

        /// <summary>Pads.</summary>
        /// <param name="text">The text.</param>
        /// <returns>A string.</returns>
        private static string Pad(string text)
        {
            var length = 3 - (text.Length + 3) % 4;
            if (length == 0)
            {
                return text;
            }
            return string.Concat(text, new string('=', length));
        }
    }
}
